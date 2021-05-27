using Equipfer.Domain.Entity;
using Equipfer.Domain.Interface;
using Equipfer.Infrastructure.Interface;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equipfer.Domain.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IOracleCommand _oracleCommand;
        public CategoriaRepository(IOracleCommand oracleCommand)
        {
            _oracleCommand = oracleCommand;
        }

        public async Task<Categoria> GetAsync(Categoria entidade)
        {
            var categoria = new Categoria();

            using (var cmd = _oracleCommand.CreateCommand())
            {
                cmd.CommandText = "SELECT AFMCDCAT Codigo, AFMDESRE Destricao, AFMCDFER Codigo_Ferrovia FROM AFMCATET WHERE AFMCDFER = :codigoFerrovia AND AFMCDCAT = :codigo ";

                cmd.Parameters.Add(new OracleParameter("codigo", entidade.Codigo));
                cmd.Parameters.Add(new OracleParameter("codigoFerrovia", entidade.CodigoFerrovia));

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        categoria.Codigo = reader.GetString(0);
                        categoria.Nome = reader.GetString(1).TrimEnd();
                        categoria.CodigoFerrovia = reader.GetString(2);
                    }
                    else
                        categoria = null;
                }
            }

            return categoria;
        }

        public async Task<List<Categoria>> GetAllAsync()
        {
            var categorias = new List<Categoria>();

            using (var cmd = _oracleCommand.CreateCommand())
            {
                cmd.CommandText = @"SELECT AFMCDCAT Codigo, AFMDESRE Destricao, AFMCDFER Codigo_Ferrovia
                    FROM AFMCATET WHERE AFMCDFER IN ('06','12') AND
                    AFMCDCAT IN ('0001','0004', '0005', '0006', '0008', '0009', '0011', '0017', 'INPT', 'INSP', 'SINS', 'SUBS', 'SUPE', 'VICA')";

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        categorias.Add(new Categoria
                        {
                            Codigo = reader.GetString(0),
                            Nome = reader.GetString(1).TrimEnd(),
                            CodigoFerrovia = reader.GetString(2)
                        });
                    }
                }
            }
            return categorias;
        }
    }
}
