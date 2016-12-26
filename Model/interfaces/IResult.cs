namespace Model.interfaces
{
    public interface IResult
    {
        bool Success { get; set; }
        string Message { get; set; }
    }
}