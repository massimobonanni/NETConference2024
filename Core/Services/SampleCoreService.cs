using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class SampleCoreService : ICoreService
    {
        public string DoSomething()
        {
            return this.GetType().ToString();
        }
    }
}
