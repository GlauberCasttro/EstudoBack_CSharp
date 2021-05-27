using Oracle.ManagedDataAccess.Client;
using System;

namespace Equipfer.Infrastructure.Interface
{
    public interface IOracleCommand
    {
        OracleCommand CreateCommand();
    }
}
