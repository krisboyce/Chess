using System.Collections.Generic;
using Model.Constants;

namespace Model.interfaces
{
    public interface ICommand
    {
        IPlayer Player { get; set; }
        CommandType Type { get; set; }
        IEnumerable<string> Arguments { get; set; }
    }
}