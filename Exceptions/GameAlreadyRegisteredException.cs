﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games_ASPNET.Exceptions
{
    public class GameAlreadyRegisteredException : Exception
    {
        public GameAlreadyRegisteredException() : base("This game is already registered") { }
    }
}
