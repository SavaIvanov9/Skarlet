﻿namespace RPG_ConsoleGame.Exceptions
{
    using System;

    public class IncorrectRaceException : Exception
    {
        public IncorrectRaceException(string message)
            : base(message)
        {
        }
    }
}

