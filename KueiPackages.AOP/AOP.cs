using System;
using System.Linq;
using System.Reflection;
using KueiPackages.AOP.Models;
using KueiPackages.Extensions;
using KueiPackages.System.Text.Json;
using Microsoft.Extensions.Logging;

namespace KueiPackages.AOP
{
    public class AOP<T> : DispatchProxy
    {
        private T          _decorated;
        private ILogger<T> _logger;

        protected override object? Invoke(MethodInfo targetMethod, object[] args)
        {
            var aopAttribute = targetMethod.GetCustomAttribute<AOPAttribute>();

            try
            {
                if (!string.IsNullOrEmpty(aopAttribute?.Before))
                {
                    var beforeMessage = aopAttribute.Before;

                    if (aopAttribute.IsLogParameter)
                    {
                        beforeMessage += "\n" + args.Select((a, i) => $"{i+1}th parameter " + a.ToJson()).Join("\n");
                    }

                    _logger.LogInformation(beforeMessage);
                }

                // Perform the actual operation
                var result = targetMethod.Invoke(_decorated, args);

                if (!string.IsNullOrEmpty(aopAttribute?.After)) _logger.LogInformation($"{aopAttribute.After}");

                return result;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(aopAttribute?.Exception)) _logger.LogError($"{aopAttribute.Exception}: {ex.Message}");
                throw;
            }
        }

        public static T Create(T decorated, ILogger<T> logger)
        {
            object proxy = Create<T, AOP<T>>();
            ((AOP<T>)proxy)._decorated = decorated;
            ((AOP<T>)proxy)._logger    = logger;
            return (T)proxy;
        }
    }
}