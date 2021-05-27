using KissLog.Apis.v1.Listeners;
using KissLog.AspNetCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAutho.Extensions
{
    public class LoggerConfig
    {

        //CLasse de configuração do kissLogger para monitoramento 
        public static void ConfigureKissLog(IOptionsBuilder options, IConfiguration configuration)
        {
            // register KissLog.net cloud listener
            options.Listeners.Add(new KissLogApiListener(new KissLog.Apis.v1.Auth.Application(
                configuration["KissLog.OrganizationId"],    //  "fdb40078-3078-4598-9151-fdc712e4367c"
                configuration["KissLog.ApplicationId"])     //  "df7f2d00-e4a1-4e24-9a0b-073f06bca98d"
            )
            {
                ApiUrl = configuration["KissLog.ApiUrl"]    //  "https://api.kisslog.net"
            });

            // optional KissLog configuration
            options.Options
                .AppendExceptionDetails((Exception ex) =>
                {
                    StringBuilder sb = new StringBuilder();

                    if (ex is System.NullReferenceException nullRefException)
                    {
                        sb.AppendLine("Important: check for null references");
                    }

                    return sb.ToString();
                });

            // KissLog internal logs
            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };
        }
    }
}
