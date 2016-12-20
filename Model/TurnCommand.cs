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
        public Player player { get; set; }
        public TurnType Type { get; set; }
        public Dictionary<string, object> Arguments = new Dictionary<string, object>();
    }
}
