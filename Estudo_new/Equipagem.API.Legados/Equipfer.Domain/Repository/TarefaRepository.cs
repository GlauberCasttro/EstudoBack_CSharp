using Equipfer.Domain.Entity;
using Equipfer.Domain.Entity.EquipagemAggregate;
using Equipfer.Domain.Interface;
using Equipfer.Infrastructure.Interface;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equipfer.Domain.Repository
{
    public class TarefaRepository : ITarefaRepository
    {
        private IOracleCommand _oracleCommand;
        public TarefaRepository(IOracleCommand oracleCommand)
        {
            _oracleCommand = oracleCommand;
        }
        public async Task<List<TarefaEscalaProgramada>> EscalaMensalAsync(Equipagem equipagem)
        {
            var tarefas = new List<TarefaEscalaProgramada>();

            using (var cmd = _oracleCommand.CreateCommand())
            {
                #region[SQL]
                cmd.CommandText = @"SELECT tarefas.AFECODTA Codigo
	                                , tarefas.AFEDTINI Data_Inicio
	                                , tarefas.AFEDTFIM Data_Fim
	                                ,tarefas.AFEHOPRO Data_Programada   	
	                                ,origem.RIECDLOC Origem
	                                ,destino.RIECDLOC Destino    
                                FROM AFETAPRT tarefas
                                    JOIN AF3CEMPT equipagem ON equipagem.AF3IDENT = tarefas.AFEIDENT
                                    JOIN RIELOFET origem ON origem.RIENUSEQ = tarefas.AFENSEQO
                                    AND origem.RIECDFEO = tarefas.AFECDFER
                                    JOIN RIELOFET destino ON destino.RIENUSEQ = tarefas.AFENSEQO
                                    AND destino.RIECDFEO = tarefas.AFECDFER
                                WHERE tarefas.AFEINPTE = 'S'
                                    AND tarefas.AFEHOPRO >= to_date ('01/' || to_char (SYSDATE, 'mm/yyyy') || ' 00:00:00', 'DD/MM/YYYY HH24:MI:SS')
                                    AND tarefas.AFEHOPRO <= to_date (to_char (last_day (sysdate), 'dd/mm/yyyy') || ' 23:59:59', 'DD/MM/YYYY HH24:MI:SS')
                                    AND equipagem.AF3PMATR = :matricula
                                ORDER BY tarefas.AFEHOPRO ";
                #endregion

                cmd.Parameters.Add(new OracleParameter("matricula", equipagem.Matricula));

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var tarefa = new TarefaEscalaProgramada
                        {
                            Codigo = reader.GetString(0),
                            DataProgramada = reader.GetDateTime(3),
                            Origem = reader.GetString(4).TrimEnd(),
                            Destino = reader.GetString(5).TrimEnd()
                        };

                        if (!reader.IsDBNull(1))
                            tarefa.DataInicio = reader.GetDateTime(1);

                        if (!reader.IsDBNull(2))
                            tarefa.DataFim = reader.GetDateTime(2);

                        tarefas.Add(tarefa);
                    }
                }
            }
            return tarefas;
        }
        public async Task<Tarefa> GetAsync(Tarefa entidade)
        {
            var tarefa = new Tarefa();

            using (var cmd = _oracleCommand.CreateCommand())
            {
                cmd.CommandText = @"SELECT AFNCDFER Ferrovia
                                    , AFNCODTA Codigo
                                    , AFNDESRT Descricao
                                FROM AFNTARET
                                WHERE AFNCDFER = :codigoFerroviario 
                                AND AFNCODTA = :codigo ";

                cmd.Parameters.Add(new OracleParameter("codigoFerroviario", entidade.CodigoFerrovia));
                cmd.Parameters.Add(new OracleParameter("codigo", entidade.Codigo));

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        tarefa.CodigoFerrovia = reader.GetString(0);
                        tarefa.Codigo = reader.GetString(1);
                        tarefa.Descricao = reader.GetString(2).TrimEnd();
                    }
                    else
                        tarefa = null;
                }
            }
            return tarefa;
        }
        public async Task<List<Tarefa>> GetAllAsync()
        {
            var tarefas = new List<Tarefa>();

            using (var cmd = _oracleCommand.CreateCommand())
            {
                cmd.CommandText = @"SELECT AFNCDFER Ferrovia
                                    , AFNCODTA Codigo
                                    , AFNDESRT Descricao
                                FROM AFNTARET
                                WHERE AFNCDFER IN ('06', '12') ";

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        tarefas.Add(new Tarefa
                        {
                            CodigoFerrovia = reader.GetString(0),
                            Codigo = reader.GetString(1),
                            Descricao = reader.GetString(2).TrimEnd()
                        });
                    }
                }
            }
            return tarefas;
        }
    }
}
