using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging
{
    public abstract class LoggerServiceBase
    {
        protected LoggerServiceBase(ILogger logger)
        {
            Logger = logger;
        }

        protected ILogger Logger { get; set; }


        public void Verbose(string message) => Logger.LogTrace(message);
        public void Fatal(string message) => Logger.LogError(message); 
        public void Info(string message) => Logger.LogInformation(message);
        public void Warn(string message) => Logger.LogWarning(message);
        public void Debug(string message) => Logger.LogDebug(message);
        public void Error(string message) => Logger.LogError(message);
    }

}
