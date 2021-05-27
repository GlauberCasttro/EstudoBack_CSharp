using Equipfer.Infrastructure.Interface;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System;

namespace Equipfer.Infrastructure.Services
{
    public class OracleConnectionFactory : IOracleConnection
    {
        private OracleConnection _oracleConnection;
        private readonly ILogger _logger;
        public OracleConnectionFactory(ILogger<OracleConnectionFactory> logger)
        {
            _logger = logger;
            CreateConnection();
        }
        private void CreateConnection()
        {
            if (_oracleConnection == null)
            {
                try
                {
                    string user = Environment.GetEnvironmentVariable("ORACLE_USER");
                    string password = Environment.GetEnvironmentVariable("ORACLE_PASSWORD");
                    string connection = Environment.GetEnvironmentVariable("ORACLE_CONNECTION");

                    if (user == null)
                        throw new ArgumentNullException(user, "Variável de ambiente de não configurada: ORACLE_USER ");

                    if (password == null)
                        throw new ArgumentNullException(password, "Variável de ambiente de não configurada: ORACLE_PASSWORD ");

                    if (connection == null)
                        throw new ArgumentNullException(connection, "Variável de ambiente de não configurada: ORACLE_CONNECTION ");

                    string connectionString = $"User Id={user};Password={password};Data Source={connection}";
                    OracleConfiguration.BindByName = true;
                    OracleConfiguration.CommandTimeout = 60;
                    _oracleConnection = new OracleConnection(connectionString);
                    _oracleConnection.Open();

                    _logger.LogInformation("CreateConnection");
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Ocorreu um erro ao abrir conexão com o Banco");
                    throw new Exception("Ocorreu um erro ao abrir conexão com o Banco");
                }
            }
        }
        public void Dispose()
        {
            _oracleConnection.Close();
            _oracleConnection.Dispose();
            _oracleConnection = null;
            _logger.LogInformation("Dispose");
        }
        public OracleConnection Connection()
        {
            _logger.LogInformation("Connection");

            if (_oracleConnection == null)
                CreateConnection();

            return _oracleConnection;
        }
    }
}
