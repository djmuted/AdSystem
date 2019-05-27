using System;
using System.Collections.Generic;
using System.Text;

namespace AdSystem
{
    public interface IAppConfiguration
    {
        Logging Logging { get; }
    }
}
