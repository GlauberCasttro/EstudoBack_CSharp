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
    public class EquipagemRepository : IEquipagemRepository
    {
        private IOracleCommand _oracleCommand;
        public EquipagemRepository(IOracleCommand oracleCommand)
        {
            _oracleCommand = oracleCommand;
        }
        public Task<List<Equipagem>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<List<string>> GetAllDisponiveisAsync(string codigo)
        {
            var matriculas = new List<string>();

            using (var cmd = _oracleCommand.CreateCommand())
            {
                #region[SQL]
                cmd.CommandText = $@"SELECT DISTINCT matricula
                    FROM (SELECT equipagem.AF3PMATR matricula
                                  , sede.RIECDLOC destacamento
                              FROM AF3CEMPT equipagem
                                  JOIN PPAEFERV empregado ON empregado.PPAPMATR = equipagem.AF3PMATR AND empregado.PPAPEMPR = equipagem.AF3PEMPR
                                  JOIN AFHALEQT alocacao ON alocacao.AFHIDENT = equipagem.AF3IDENT
                                  JOIN RIEEFERV sede ON sede.RIENUSEQ = alocacao.AFHNUSEQ
                                  JOIN AFMCATET categoria ON categoria.AFMCDCAT = alocacao.AFHCDCAT
                                  AND categoria.AFMCDFER = alocacao.AFHCDFER
                                  JOIN AFETAPRT tarefas ON tarefas.AFEIDENT = equipagem.AF3IDENT
                              WHERE equipagem.AF3DTTHA > SYSDATE
                                  AND categoria.AFMCDCAT IN ('0001', '0004', '0005', '0006', '0008', '0009', '0011', '0017', 'INPT', 'INSP', 'SINS', 'SUBS', 'SUPE', 'VICA')
                                  AND alocacao.AFHDTFVA IS NULL
                                  AND tarefas.AFEHOPRO BETWEEN SYSDATE AND (SYSDATE + 12 / 24)
                                  AND tarefas.AFEINPTE = 'S'
                                  AND empregado.PPAPSICA = 3
                                  AND tarefas.AFECODTA IN ('ADEF', 'VIAG', 'MANB', 'TREM') 
                        UNION
                        SELECT equipagem.AF3PMATR Matricula
                            , sede.RIECDLOC destacamento
                            FROM AF3CEMPT equipagem
                                JOIN PPAEFERV empregado ON empregado.PPAPMATR = equipagem.AF3PMATR AND empregado.PPAPEMPR = equipagem.AF3PEMPR
                                JOIN AFHALEQT alocacao ON alocacao.AFHIDENT = equipagem.AF3IDENT
                                JOIN RIEEFERV sede ON sede.RIENUSEQ = alocacao.AFHNUSEQ
                                JOIN AFMCATET categoria ON categoria.AFMCDCAT = alocacao.AFHCDCAT
                                AND categoria.AFMCDFER = alocacao.AFHCDFER
                                JOIN AFWATRET atividades ON atividades.AFWIDENT = equipagem.AF3IDENT
                                JOIN AFBATIVT atividade ON atividade.AFBCODAT = atividades.AFWCODAT
                            WHERE equipagem.AF3DTTHA > SYSDATE
                                AND categoria.AFMCDCAT IN ('0001', '0004', '0005', '0006', '0008', '0009', '0011', '0017', 'INPT', 'INSP', 'SINS', 'SUBS', 'SUPE', 'VICA')
                                AND alocacao.AFHDTFVA IS NULL
                                AND atividades.AFWINUAR = 'S'
                                AND empregado.PPAPSICA = 3
                                AND atividade.AFBIDTRA = 'S' ) WHERE destacamento = :codigo ";
                #endregion
                OracleParameter parameter = new OracleParameter("codigo", OracleDbType.Char, 5);
                parameter.Value = codigo;

                cmd.Parameters.Add(parameter);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        matriculas.Add(reader.GetString(0));
                    }
                }
            }
            return matriculas;
        }
        public async Task<Equipagem> GetAsync(Equipagem entidade)
        {
            var equipagem = new Equipagem();

            using (var cmd = _oracleCommand.CreateCommand())
            {
                #region[SQL]
                cmd.CommandText = @"SELECT equipagem.AF3PMATR Matricula
                    , empregado.PPAPNOME Nome
                    , categoria.AFMCDCAT Codigo_Categoria
                    , sede.riecdloc Sede
                    , concat (empregado.PPADDDTT, empregado.PPANTELS) telefone
                    , ferrovia.RHACDFER ferrovia
                    , categoria.AFMJORNA Jornada_Diaria
                    , atividade_indis.AFBCODAT Codigo_Indisponibilidade
                    , atividade_indis.AFBDESCR Descricao_Indisponibilidade
                    , atividades_indis.AFWDTINI DataInicio_Indisponibilidade
                    , CASE WHEN atividades_indis.AFWDTFIM IS NULL THEN atividades_indis.AFWDTINI + (1 / 24 / 60) * atividade_destacamento.AFRTMDMI ELSE atividades_indis.AFWDTFIM END DataFim_Indisponibilidade
                    , local_indis.RIECDLOC Local_Indisponibilidade
                    , proxima_tarefa.AFEHOPRO proxima_tarefa
                    , local_tarefa.RIECDLOC Local_Tarefa
                    , atividade.AFBCODAT Codigo_Atividade
                    , atividade.AFBDESCR Descricao_Atividade
                    , origem_atividade.RIECDLOC Origem_Atividade
                    , destino_atividade.RIECDLOC Destino_Atividade
                    , atividades.AFWNUMOS OS_Atividade
                    , atividades.AFWITREM PrefixoTrem_Atividade
                    , CASE WHEN atividade.AFBIDTRA = 'S' THEN 1 ELSE 0 END Is_Trabalho
                    , (SELECT min (AFEHOPRO)
                       FROM AFETAPRT
                       WHERE AFEHOPRO > SYSDATE
                           AND AFEIDENT = equipagem.AF3IDENT
                           AND AFECDFER = alocacao.AFHCDFER
                           AND AFECODTA = 'FOLG') Proxima_Folga
                    , CAST ((24 * (nvl (ultimo_descanso.AFWDTFIM, sysdate) -ultimo_descanso.AFWDTINI)) AS DECIMAL (5, 0)) duracao_UltimoDescanso
                    , (SELECT min (temp1.AFWDTINI)
                       FROM AFWATRET temp1
                           JOIN AFWATRET temp2 ON temp2.AFWNSEQT = temp1.AFWNSEQT
                           JOIN AFBATIVT temp3 ON temp3.AFBCODAT = temp1.AFWCODAT
                       WHERE temp2.AFWIDENT = equipagem.AF3IDENT
                           AND temp2.AFWINUAR = 'S'
                           AND temp3.AFBIDTRA = 'S') abertura_jornada_efetiva
                FROM AF3CEMPT equipagem
                    JOIN PPAEFERV empregado ON equipagem.AF3PMATR = empregado.PPAPMATR
                    AND equipagem.AF3PEMPR = empregado.PPAPEMPR
                    JOIN AFHALEQT alocacao ON alocacao.AFHIDENT = equipagem.AF3IDENT
                    JOIN RHAFERRT ferrovia ON ferrovia.RHACDFER = alocacao.AFHCDFER
                    JOIN RIEEFERV sede ON sede.RIENUSEQ = alocacao.AFHNUSEQ
                    JOIN AFMCATET categoria ON categoria.AFMCDCAT = alocacao.AFHCDCAT
                    AND categoria.AFMCDFER = alocacao.AFHCDFER
                    JOIN AFWATRET atividades_indis ON atividades_indis.AFWIDENT = equipagem.AF3IDENT
                    JOIN AFBATIVT atividade_indis ON atividade_indis.AFBCODAT = atividades_indis.AFWCODAT
                    JOIN RIEEFERV local_indis ON local_indis.RIENUSEQ = atividades_indis.AFWNSEDE
                    JOIN AFRATDET atividade_destacamento ON atividade_destacamento.AFRCODAT = atividade_indis.AFBCODAT
                    AND atividade_destacamento.AFRNUSEQ = local_indis.RIENUSEQ
                    JOIN AFETAPRT proxima_tarefa ON proxima_tarefa.AFEIDENT = equipagem.AF3IDENT
                    JOIN RIEEFERV local_tarefa ON local_tarefa.RIENUSEQ = proxima_tarefa.AFENSEQD
                    AND local_tarefa.RIECDFEO = proxima_tarefa.AFECDFER
                    JOIN AFWATRET atividades ON atividades.AFWIDENT = equipagem.AF3IDENT
                    JOIN AFBATIVT atividade ON atividade.AFBCODAT = atividades.AFWCODAT
                    JOIN RIEEFERV origem_atividade ON origem_atividade.RIENUSEQ = atividades.AFWNSEOE
                    JOIN RIEEFERV destino_atividade ON destino_atividade.RIENUSEQ = atividades.AFWNSEDE
                    JOIN AFWATRET ultimo_descanso ON ultimo_descanso.AFWIDENT = equipagem.AF3IDENT
                WHERE alocacao.AFHDTFVA IS NULL
                    AND atividades_indis.AFWDTINI = (SELECT max (AFWATRET.AFWDTINI)
                                                     FROM AFWATRET
                                                         JOIN AFBATIVT ON AFWATRET.AFWCODAT = AFBATIVT.AFBCODAT
                                                     WHERE AFWATRET.AFWIDENT = equipagem.AF3IDENT
                                                         AND AFWATRET.AFWDTINI BETWEEN SYSDATE - 7 AND sysdate
                                                         AND AFBATIVT.AFBIDEXT IN ('F', 'D', 'C', 'I'))
                    AND proxima_tarefa.AFEHOPRO = (SELECT AFETAPRT.AFEHOPRO
                                                   FROM AFETAPRT
                                                   WHERE AFETAPRT.AFEINPTE = 'S'
                                                       AND AFETAPRT.AFEINPTP = 'S'
                                                       AND AFEIDENT = equipagem.AF3IDENT)
                    AND atividades.AFWINUAR = 'S'
                    AND ultimo_descanso.AFWDTINI = (SELECT max (AFWATRET.AFWDTINI) AFWDTINI
                                                    FROM AFWATRET
                                                        JOIN AFBATIVT ON AFWATRET.AFWCODAT = AFBATIVT.AFBCODAT
                                                    WHERE AFWATRET.AFWIDENT = equipagem.AF3IDENT
                                                        AND AFWATRET.AFWDTINI BETWEEN SYSDATE - 7 AND sysdate
                                                        AND AFBATIVT.AFBIDEXT IN ('D', 'C', 'I'))
                    AND equipagem.AF3PMATR = :matricula";
                #endregion

                cmd.Parameters.Add(new OracleParameter("matricula", entidade.Matricula));

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        #region[Mapper]
                        equipagem.Matricula = reader.GetString(0);
                        equipagem.Nome = reader.GetString(1).TrimEnd();
                        equipagem.Categoria = reader.GetString(2);
                        equipagem.Sede = reader.GetString(3).TrimEnd();
                        equipagem.Telefone = reader.GetString(4).TrimStart().TrimEnd();
                        equipagem.CodigoFerrovia = reader.GetString(5).TrimEnd();
                        equipagem.JornadaDiaria = reader.GetInt32(6);

                        equipagem.Indisponibilidade = new Indisponibilidade { };
                        equipagem.Indisponibilidade.Codigo = reader.GetString(7);
                        equipagem.Indisponibilidade.Atividade = reader.GetString(8).TrimEnd();
                        equipagem.Indisponibilidade.DataInicio = reader.GetDateTime(9);
                        equipagem.Indisponibilidade.DataTermino = reader.GetDateTime(10);
                        equipagem.Indisponibilidade.LocalTermino = reader.GetString(11).TrimEnd();

                        equipagem.EscalaProgramada = new EscalaProgramada
                        {
                            DataHora = reader.GetDateTime(12),
                            Local = reader.GetString(13).TrimEnd()
                        };

                        equipagem.AtividadeAtual = new AtividadeAtual();
                        equipagem.AtividadeAtual.Codigo = reader.GetString(14);
                        equipagem.AtividadeAtual.Atividade = reader.GetString(15).TrimEnd();
                        equipagem.AtividadeAtual.Origem = reader.GetString(16).TrimEnd();
                        equipagem.AtividadeAtual.Destino = reader.GetString(17).TrimEnd();

                        equipagem.AtividadeAtual.Trem = new Trem();

                        if (!reader.IsDBNull(18))
                            equipagem.AtividadeAtual.Trem.Os = reader.GetInt32(18);
                        if (!reader.IsDBNull(19))
                            equipagem.AtividadeAtual.Trem.Prefixo = reader.GetString(19);
                        equipagem.AtividadeAtual.IsTrabalalho = reader.GetInt32(20) == 1;

                        if (!reader.IsDBNull(21))
                            equipagem.ProximaFolga = reader.GetDateTime(21);

                        equipagem.DuracaoUltimoDescanso = reader.GetInt32(22);
                        equipagem.AberturaJornadaEfetiva = reader.GetDateTime(23);
                        #endregion
                    }
                    else
                        equipagem = null;
                }
            }

            return equipagem;
        }
    }
}
