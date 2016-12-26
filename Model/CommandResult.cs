using Model.interfaces;

namespace Model
{
    public class CommandResult : IResult
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public static CommandResult GetSuccess(string message)
        {
            return new CommandResult()
            {
                Success = true,
                Message = message
            };
        }

        public static CommandResult GetFail(string message)
        {
            return new CommandResult()
            {
                Success = false,
                Message = message
            };
        }
    }
}