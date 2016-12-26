using Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.interfaces;

namespace Model
{
    public class Player : IPlayer
    {
        public Side Side { get; set; }
        public string Name { get; set; }
    }
}
