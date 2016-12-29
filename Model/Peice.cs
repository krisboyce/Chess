using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Constants;

namespace Model
{
    public class Peice
    {
        public PeiceType Type { get; set; }
        public Side Side { get; set; }
        public bool HasMoved { get; set; }
        public bool IsChecked { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Peice() {}

        public Peice(Peice peice)
        {
            this.X = peice.X;
            this.Y = peice.Y;
            this.HasMoved = peice.HasMoved;
            this.IsChecked = peice.IsChecked;
            this.Side = peice.Side;
            this.Type = peice.Type;
        }
    }
}
