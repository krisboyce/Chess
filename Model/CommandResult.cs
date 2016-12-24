namespace Model
{
    public class CommandResult
    {
        public string ResultMessage { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }

        public static CommandResult GetSuccess(string message)
        {
            return new CommandResult()
            {
                Success = true,
                ResultMessage = message
            };
        }

        public static CommandResult GetFail(string message)
        {
            return new CommandResult()
            {
                Success = false,
                ErrorMessage = message
            };
        }
    }
}