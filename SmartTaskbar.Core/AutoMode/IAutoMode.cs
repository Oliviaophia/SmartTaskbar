namespace SmartTaskbar.Core.AutoMode
{
    public interface IAutoMode
    {
        void Run();

        void Ready();

        void Reset();
    }
}
