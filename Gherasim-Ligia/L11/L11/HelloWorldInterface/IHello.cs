﻿using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldInterface
{
    public interface IHello:IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);
    }
}
