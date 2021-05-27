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
    public class AtividadeRepository : IAtividadeRepository
    {
        private readonly IOracleCommand _oracleCommand;
        public AtividadeRepository(IOracleCommand oracleCommand)
        {
            _oracleCommand = oracleCommand;
        }

        public async Task<List<AtividadeJornada>> AtividadeJornadaAnteriorAsync(Equipagem entidade)
        {
            var atividades = new List<AtividadeJornada>();

            using (var cmd = _oracleCommand.CreateCommand())
            {
                cmd.CommandText = @"SELECT atv.AFBDESRE Descricao
                        , atividade.AFWDTINI Inicio
                        , atividade.AFWDTFIM Fim
                        , atividade.AFWITREM Trem
                        , atividade.AFWNUMOS Os
                        , origem.RIECDLOC origem
                        , destino.RIECDLOC destino
                        , atv.AFBIDTRA is_trabalho
                        , atv.AFBCODAT Codigo
                    FROM AF3CEMPT equipagem
                        JOIN AFETAPRT tarefa ON tarefa.AFEIDENT = equipagem.AF3IDENT
                        JOIN AFWATRET atividade ON atividade.AFWNSEQT = tarefa.AFENUSEQ
                        JOIN AFBATIVT atv ON atv.AFBCODAT = atividade.AFWCODAT
                        JOIN RIEEFERV origem ON origem.RIENUSEQ = atividade.AFWNSEOE
                        JOIN RIEEFERV destino ON destino.RIENUSEQ = atividade.AFWNSEDE
                        , AFWATRET atividade_atual
                    WHERE equipagem.AF3PMATR = :matricula
                        AND tarefa.AFEINPTE = 'S'
                        AND atividade_atual.AFWIDENT = equipagem.AF3IDENT
                        AND atividade_atual.AFWINUAR = 'S'
                        AND tarefa.AFEDTFIM = (SELECT min (atv.AFWDTINI)
                                               FROM AFWATRET atv
                                               WHERE atv.AFWNSEQT = atividade_atual.AFWNSEQT)
                    ORDER BY atividade.AFWDTINI";

                cmd.Parameters.Add(new OracleParameter("matricula", entidade.Matricula));

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var atividade = new AtividadeJornada();
                        atividade.Descricao = reader.GetString(0).TrimEnd();
                        if (!reader.IsDBNull(1))
                            atividade.Inicio = reader.GetDateTime(1);
                        if (!reader.IsDBNull(2))
                            atividade.Fim = reader.GetDateTime(2);
                        atividade.Trem = new Trem();
                        atividade.Trem.Prefixo = reader.IsDBNull(3) ? null : reader.GetString(3);
                        if (!reader.IsDBNull(4))
                            atividade.Trem.Os = reader.GetInt32(4);
                        atividade.Origem = reader.IsDBNull(5) ? null : reader.GetString(5).TrimEnd();
                        atividade.Destino = reader.IsDBNull(6) ? null : reader.GetString(6).TrimEnd();
                        atividade.IsTrabalho = reader.GetString(7) == "S" ? true : false;
                        atividade.Codigo = reader.GetString(8);
                        atividades.Add(atividade);
                    }
                }
            }
            return atividades;
        }

        public async Task<List<AtividadeJornada>> AtividadeJornadaAtualAsync(Equipagem entidade)
        {
            var atividades = new List<AtividadeJornada>();

            using (var cmd = _oracleCommand.CreateCommand())
            {
                cmd.CommandText = @"SELECT atv.AFBDESRE Descricao
                        , atividade.AFWDTINI Inicio
                        , atividade.AFWDTFIM Fim
                        , atividade.AFWITREM Trem
                        , atividade.AFWNUMOS Os
                        , origem.RIECDLOC origem
                        , destino.RIECDLOC destino
                        , atv.AFBIDTRA is_trabalho
                        , atv.AFBCODAT Codigo
                    FROM AFWATRET atividade
	                    JOIN AF3CEMPT equipagem ON equipagem.AF3IDENT = atividade.AFWIDENT
	                    JOIN AFBATIVT atv ON atv.AFBCODAT = atividade.AFWCODAT
                        JOIN RIEEFERV origem ON origem.RIENUSEQ = atividade.AFWNSEOE
                        JOIN RIEEFERV destino ON destino.RIENUSEQ = atividade.AFWNSEDE
                    WHERE equipagem.AF3PMATR = :matricula
                    AND atividade.AFWNSEQT = (SELECT AFWNSEQT
                                                FROM AFWATRET                                
                                                WHERE AFWINUAR = 'S'
                                                    AND AFWIDENT = equipagem.AF3IDENT)
                    ORDER BY atividade.AFWDTINI ";

                cmd.Parameters.Add(new OracleParameter("matricula", entidade.Matricula));

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var atividade = new AtividadeJornada();
                        atividade.Descricao = reader.GetString(0).TrimEnd();
                        if (!reader.IsDBNull(1))
                            atividade.Inicio = reader.GetDateTime(1);
                        if (!reader.IsDBNull(2))
                            atividade.Fim = reader.GetDateTime(2);
                        atividade.Trem = new Trem();
                        atividade.Trem.Prefixo = reader.IsDBNull(3) ? null : reader.GetString(3);
                        if (!reader.IsDBNull(4))
                            atividade.Trem.Os = reader.GetInt32(4);
                        atividade.Origem = reader.IsDBNull(5) ? null : reader.GetString(5).TrimEnd();
                        atividade.Destino = reader.IsDBNull(6) ? null : reader.GetString(6).TrimEnd();
                        atividade.IsTrabalho = reader.GetString(7) == "S" ? true : false;
                        atividade.Codigo = reader.GetString(8);
                        atividades.Add(atividade);
                    }
                }
            }
            return atividades;
        }

        public async Task<Atividade> GetAsync(Atividade entidade)
        {
            var atividade = new Atividade();

            using (var cmd = _oracleCommand.CreateCommand())
            {
                cmd.CommandText = "SELECT AFBCODAT Codigo, AFBDESRE Descricao, AFBIDTRA is_trabalho FROM AFBATIVT WHERE AFBCODAT = :id ";

                cmd.Parameters.Add(new OracleParameter("id", entidade.Codigo));

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        atividade.Codigo = reader.GetString(0);
                        atividade.Nome = reader.GetString(1).TrimEnd();
                        atividade.IsTrabalho = reader.GetString(2) == "S" ? true : false;
                    }
                    else
                    {
                        atividade = null;
                    }
                }
            }
            return atividade;
        }

        public async Task<List<Atividade>> GetAllAsync()
        {
            var atividades = new List<Atividade>();

            using (var cmd = _oracleCommand.CreateCommand())
            {
                cmd.CommandText = "SELECT AFBCODAT Codigo, AFBDESRE Descricao, AFBIDTRA is_trabalho FROM AFBATIVT ";

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        atividades.Add(new Atividade
                        {
                            Codigo = reader.GetString(0),
                            Nome = reader.GetString(1).TrimEnd(),
                            IsTrabalho = reader.GetString(2) == "S" ? true : false
                        });
                    }
                }
            }
            return atividades;
        }

        public async Task<List<AtividadeJornada>> AtividadesQuizenalMensalAsync(Equipagem equipagem)
        {
            var atividades = new List<AtividadeJornada>();

            var now = DateTime.Now;
            var inicio = new DateTime(now.Year, now.Month, 1, 0, 0, 0);

            // Verifica de o dia do mês é menor que 15 dias por precisamos pegar o acumulo de quizenal pois o mensal é apenas do mês atual
            if (DateTime.Now.Day < 15)
                inicio = inicio.AddDays(-(15 - DateTime.Now.Day));

            var fim = DateTime.Now;


            using (var cmd = _oracleCommand.CreateCommand())
            {
                cmd.CommandText = @"SELECT atividades.AFWDTINI Inicio
                        , atividades.AFWDTFIM Fim
                        , atividades.AFWDURHE Hora_Extra
                    FROM AF3CEMPT equipagem
                        JOIN AFETAPRT tarefa ON tarefa.AFEIDENT = equipagem.AF3IDENT
                        JOIN AFWATRET atividades ON equipagem.AF3IDENT = atividades.AFWIDENT
                        AND tarefa.AFENUSEQ = atividades.AFWNSEQT
                        JOIN AFBATIVT atividade ON atividade.AFBCODAT = atividades.AFWCODAT
                    WHERE equipagem.AF3PMATR = :matricula
                        AND tarefa.AFEINPTE = 'S'
                        AND ((atividades.AFWDTINI 
    	                    BETWEEN :inicio AND :fim) 
                        OR (atividades.AFWDTFIM 
    	                    BETWEEN :inicio AND :fim))
                        AND atividade.AFBIDTRA = 'S' ORDER BY Inicio ";

                cmd.Parameters.Add(new OracleParameter("matricula", equipagem.Matricula));
                cmd.Parameters.Add(new OracleParameter("inicio", inicio));
                cmd.Parameters.Add(new OracleParameter("fim", fim));

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var atividade = new AtividadeJornada { Inicio = reader.GetDateTime(0) };
                        if (!reader.IsDBNull(1))
                            atividade.Fim = reader.GetDateTime(1);

                        if (!reader.IsDBNull(2))
                            atividade.HoraExtra = reader.GetInt32(2);

                        atividades.Add(atividade);
                    }
                }
            }

            return atividades;
        }
    }
}
