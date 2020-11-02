using System;
using SmartTaskbar.Engines.Interfaces;
using SmartTaskbar.Models;

namespace SmartTaskbar.Engines.Runners
{
    internal class ForegroundModeRunner
        : IAutoModeMethod
    {
        public AutoModeType Type { get; } = AutoModeType.ForegroundMode;
        public void Run() { throw new NotImplementedException(); }

        public void Reset() { throw new NotImplementedException(); }

        public void Ready() { throw new NotImplementedException(); }
    }
}
