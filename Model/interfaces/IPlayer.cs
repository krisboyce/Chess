using Model.Constants;

namespace Model.interfaces
{
    public interface IPlayer
    {
        Side Side { get; set; }
        string Name { get; set; }
    }
}