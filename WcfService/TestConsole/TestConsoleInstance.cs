﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonHelper;

namespace TestConsole
{
    public class TestConsoleInstance : IHostStartup
    {

        public void Main()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("TestConsoleInstance : " + i);
            }
        }
    }
}