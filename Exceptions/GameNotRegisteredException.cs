﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games_ASPNET.Exceptions
{
    public class GameNotRegisteredException : Exception
    {
        public GameNotRegisteredException() : base("This game is not registered") { }
    }
}
