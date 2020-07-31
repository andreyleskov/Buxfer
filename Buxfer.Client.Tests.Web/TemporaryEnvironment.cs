using System;
using System.Collections.Generic;
using System.Linq;

namespace Buxfer.Client.Tests.Web
{
    public class TemporaryEnvironment : IDisposable
    {
        private readonly IDictionary<string, string> _cleanUpScope;

        public TemporaryEnvironment(IDictionary<string, string> variables)
        {
            _cleanUpScope = variables.ToDictionary(pair => pair.Key, pair =>
            {
                SetVariable(pair.Key, pair.Value, out var oldValue);
                return oldValue;
            });
        }

        public void Dispose()
        {
            foreach (var varToClean in _cleanUpScope)
            {
                if (varToClean.Value != null)
                    Console.WriteLine($"Restoring env variable {varToClean.Key} to {varToClean.Value}");
                else
                    Console.WriteLine($"Deleting env variable {varToClean.Key}");

                Environment.SetEnvironmentVariable(varToClean.Key, varToClean.Value);
            }
        }

        private static bool SetVariable(string variable, string variableValue, out string oldValue)
        {
            oldValue = Environment.GetEnvironmentVariable(variable);
            Environment.SetEnvironmentVariable(variable, variableValue);
            return oldValue != null;
        }
    }
}