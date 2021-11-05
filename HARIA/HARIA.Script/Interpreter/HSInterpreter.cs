using HARIA.Script.Abstractions;
using HARIA.Script.Models;

namespace HARIA.Script.Interpreter
{
    public class HSInterpreter : IHSInterpreter
    {
        private readonly IExecutor executor;

        public HSInterpreter(IExecutor executor)
        {
            this.executor = executor;
        }

        public List<Variable> Variables { get; private set; }

        public int ExecutingLine { get; private set; }

        public string ExecutingStatement { get; private set; }

        public Task<ExecutionResult> Evaluate(string script)
        {
            throw new NotImplementedException();
        }

        public Task<ExecutionResult> Execute(string script)
        {
            throw new NotImplementedException();
        }
    }
}
