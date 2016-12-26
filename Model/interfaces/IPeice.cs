using Model.Constants;

namespace Model.interfaces
{
    public interface IPeice
    {
        PeiceType Type { get; set; }
        Side Side { get; set; }
        bool HasMoved { get; set; }
        bool IsChecked { get; set; }
        int X { get; set; }
        int Y { get; set; }
    }
}