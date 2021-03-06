﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MonoDragons
{
    public static class Logger
    {
        private static readonly List<Action<string>> _sinks = new List<Action<string>> { x => Debug.WriteLine(x) };

        public static void AddSink(Action<string> sink) => _sinks.Add(sink);

        public static void WriteLine(string text) => _sinks.ForEach(write => write(text));
    }
}
