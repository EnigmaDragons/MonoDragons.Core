using System;
using MonoDragons.Core.Engine;

#if WINDOWS || LINUX

namespace MonoDragons.Core
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
        }
    }
#endif
}
