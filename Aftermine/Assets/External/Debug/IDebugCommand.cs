
namespace gamedev.utilities.debug
{
    public interface IDebugCommand
    {
        string CommandId { get; }
        string CommandDescription { get; }
        string CommandFormat { get; }

        void Invoke(params object[] arguments);
    }
}