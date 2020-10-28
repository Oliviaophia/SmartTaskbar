using System;
using SmartTaskbar.Engines.Interfaces;
using SmartTaskbar.Models;

namespace SmartTaskbar.Engines.ModeMethods
{
    internal class AllowlistModeMethod
        : IAutoModeMethod
    {
        public AutoModeType Type { get; } = AutoModeType.AllowlistMode;
        public void Run() { throw new NotImplementedException(); }

        public void Reset() { throw new NotImplementedException(); }

        public void Ready() { throw new NotImplementedException(); }
    }
}
