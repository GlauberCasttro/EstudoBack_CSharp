using Equipfer.Infrastructure.Interface;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;

namespace Equipfer.Infrastructure.Services
{
    public class OracleCommandFactory : IOracleCommand
    {
        private readonly IOracleConnection _oracleConnection;
        private readonly ILogger _logger;
        public OracleCommandFactory(IOracleConnection oracleConnection,ILogger<OracleCommandFactory> logger)
        {
            _oracleConnection = oracleConnection;
            _logger = logger;
        }
        public OracleCommand CreateCommand()
        {
            _logger.LogInformation("CreateCommand");
            return _oracleConnection.Connection().CreateCommand();
        }
    }
}
