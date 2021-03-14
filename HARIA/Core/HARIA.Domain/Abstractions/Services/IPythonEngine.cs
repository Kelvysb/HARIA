using System.Collections.Generic;
using HARIA.Domain.DTOs;

namespace HARIA.Domain.Abstractions.Services
{
    public interface IPythonEngine
    {
        ScriptResult Execute(string script, Dictionary<string, string> states, params (string, string)[] parameters);

        ScriptResult Execute(string script, Dictionary<string, string> states);

        ScriptResult CheckScript(string script, Dictionary<string, string> states, params (string, string)[] parameters);

        ScriptResult CheckScript(string script, Dictionary<string, string> states);
    }
}