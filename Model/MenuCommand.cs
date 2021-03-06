﻿using Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.interfaces;

namespace Model
{
    public class MenuCommand : ICommand
    {
        public IPlayer Player { get; set; }
        public CommandType Type { get; set; }
        public IEnumerable<string> Arguments { get; set; }

        public MenuCommand()
        {
            Arguments = new List<string>();
        }
    }
}
