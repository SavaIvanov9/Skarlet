﻿namespace WindowsFormsApplication1.Exceptions
{
    using System;

    public class NotExistingFileException : Exception
    {
        public NotExistingFileException(string message)
            : base(message)
        {
        }
    }
}


