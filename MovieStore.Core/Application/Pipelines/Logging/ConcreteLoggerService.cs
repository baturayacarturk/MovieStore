using Core.CrossCuttingConcerns.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Logging
{
    public class ConcreteLoggerService : LoggerServiceBase
    {
        public ConcreteLoggerService(ILogger<ConcreteLoggerService> logger) : base(logger)
        {
        }
    }
}
