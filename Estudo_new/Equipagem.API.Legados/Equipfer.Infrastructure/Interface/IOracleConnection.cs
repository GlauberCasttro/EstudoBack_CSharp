using Oracle.ManagedDataAccess.Client;
using System;

namespace Equipfer.Infrastructure.Interface
{
    public interface IOracleConnection : IDisposable
    {
        OracleConnection Connection();
    }
}
