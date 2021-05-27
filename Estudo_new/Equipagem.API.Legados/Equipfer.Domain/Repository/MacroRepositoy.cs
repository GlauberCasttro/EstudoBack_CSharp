using Equipfer.Domain.Entity.MacroAggregate;
using Equipfer.Domain.Interface;
using Equipfer.Infrastructure.Interface;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Equipfer.Domain.Repository
{
    public class MacroRepositoy : IMacroRepository
    {

        private readonly IOracleCommand _oracleCommand;

        public MacroRepositoy(IOracleCommand oracleCommand)
        {
            this._oracleCommand = oracleCommand;
        }


 //---------------------------------------------------------------------------------------------------
    
        public async Task<List<Macro>> GetMacrosPendentesAsync(string equipagem)
        {

            var macros = new List<Macro>();
            using (var cmd = _oracleCommand.CreateCommand())
            {
             cmd.CommandText = @"SELECT ACT.AJ4CDLOI,
	                       ACT.AJ4CDLDI,
	                       ACT.AJ4CDPRT,
	                       ACT.AJ4CODAT,
	                       ACT.AJ4MATEM,
	                       ACT.AJ4DTHIA,
	                       ACT.AJ4TPMAC
	                     FROM AJ4IEACT ACT
	                    JOIN AF3CEMPT EMP ON ACT.AJ4MATEM = EMP.AF3PMATR
                    WHERE EMP.AF3PMATR = '480549'
                    AND ACT.AJ4NUSEQ > 
                    (SELECT max(ACT2.AJ4NUSEQ) FROM AJ4IEACT ACT2
	                    WHERE 1=1
	                    AND ACT2.AJ4INDST = 'S' 
	                    --AND ACT.AJ4DTHCR >= SYSDATE - 3
	                    AND ACT2.AJ4MATEM  = EMP.AF3PMATR)
                    AND ACT.AJ4INDST = 'P'";

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        macros.Add(new Macro
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

        public Task<Macro> GetAsync(Macro entidade)
        {
            throw new NotImplementedException();
        }
        public Task<List<Macro>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

    }
}