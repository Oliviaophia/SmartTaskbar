using SmartTaskbar.Models;

namespace SmartTaskbar.Engines.Interfaces
{
    public interface IAutoModeMethod
    {
        AutoModeType Type { get; }

        void Run();

        void Reset();

        void Ready();
    }
}
