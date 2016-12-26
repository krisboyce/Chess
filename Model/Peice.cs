using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Constants;
using Model.interfaces;

namespace Model
{
    public class Peice : IPeice
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
            if (peice == null) return;
            X = peice.X;
            Y = peice.Y;
            HasMoved = peice.HasMoved;
            IsChecked = peice.IsChecked;
            Side = peice.Side;
            Type = peice.Type;
        }
    }
}
