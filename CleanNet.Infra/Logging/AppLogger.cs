using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanNet.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace CleanNet.Infra.Logging
{
    public class AppLogger<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;

        public AppLogger(ILogger<T> logger)
        {
            _logger = logger;
        }


        public void LogDebug(string message, params object[] args) => _logger.LogDebug(message, args);

        public void LogError(Exception exception, string message, params object[] args) => _logger.LogError(exception, message, args);

        public void LogInformation(string message, params object[] args) => _logger.LogInformation(message, args);

        public void LogTrace(string message, params object[] args) => _logger.LogTrace(message, args);

        public void LogWarning(string message, params object[] args) => _logger.LogWarning(message, args);
    }
}