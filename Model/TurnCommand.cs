using Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TurnCommand
    {
        public Player Player { get; set; }
        public TurnType Type { get; set; }
        public List<string> Arguments;

        public TurnCommand()
        {
            Arguments = new List<string>();
        }
    }
}
