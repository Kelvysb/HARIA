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

        public async Task<ExecutionResult> Execute(string script)
        {
            var result = new ExecutionResult { ResultCode = Enums.ResultCode.Succsses, Results = new() };

            try
            {
                var normalized = Normalize(script);
                var statements = SplitStatements(normalized);
                foreach (var statement in statements)
                {
                    ExecutingStatement = statement;
                    ExecutingLine = statements.IndexOf(statement) + 1;
                    result.Results.Add(await ExecuteGroup(statement));
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            

            return result;
        }



        private string Normalize(string command)
        {
            throw new NotImplementedException();
        }
        private List<string> SplitStatements(string command)
        {
            throw new NotImplementedException();
        }

        private void SplitAndMerge(string command)
        {

        }

        private Task<string> ExecuteGroup(string command)
        {
            return Task.FromResult("");
        }

    }
}
