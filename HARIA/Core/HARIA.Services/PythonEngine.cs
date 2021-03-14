using System;
using System.Collections.Generic;
using System.IO;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace HARIA.Services
{
    public class PythonEngine : IPythonEngine, IDisposable
    {
        private ScriptEngine engine;

        public PythonEngine()
        {
            engine = Python.CreateEngine();
        }

        public void Dispose()
        {
            engine = null;
        }

        public ScriptResult Execute(string script, Dictionary<string, string> states, params (string, string)[] parameters)
        {
            var result = new ScriptResult();
            var outputStream = new MemoryStream();
            var errorStream = new MemoryStream();

            engine.Runtime.IO.SetOutput(outputStream, System.Text.Encoding.UTF8);
            engine.Runtime.IO.SetErrorOutput(errorStream, System.Text.Encoding.UTF8);

            var scope = engine.CreateScope();

            scope.SetVariable("states", states);

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    scope.SetVariable(parameter.Item1, parameter.Item2);
                }
            }

            var source = engine.CreateScriptSourceFromString(script, SourceCodeKind.Statements);
            var compiled = source.Compile();
            compiled.Execute(scope);
            outputStream.Position = 0;
            errorStream.Position = 0;
            StreamReader outputReader = new StreamReader(outputStream);
            StreamReader errorReader = new StreamReader(outputStream);
            result.StdOut = outputReader.ReadToEnd();
            result.StdErr = errorReader.ReadToEnd();
            outputStream.Dispose();
            outputReader.Dispose();
            errorReader.Dispose();
            return result;
        }

        public ScriptResult Execute(string script, Dictionary<string, string> states)
        {
            return Execute(script, states, null);
        }

        public ScriptResult CheckScript(string script, Dictionary<string, string> states, params (string, string)[] parameters)
        {
            try
            {
                var result = Execute(script, states, parameters);
                return result;
            }
            catch (Exception ex)
            {
                return new ScriptResult() { StdErr = ex.Message };
            }
        }

        public ScriptResult CheckScript(string script, Dictionary<string, string> states)
        {
            return CheckScript(script, states, null);
        }
    }
}