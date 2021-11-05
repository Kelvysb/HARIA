using HARIA.Script.Models;

namespace HARIA.Script.Abstractions
{
    public interface IHSInterpreter
    {
        public List<Variable> Variables { get; }

        public int ExecutingLine { get; }

        public string ExecutingStatement { get; }

        Task<ExecutionResult> Execute(string script);

        Task<ExecutionResult> Evaluate(string script);
    }
}
