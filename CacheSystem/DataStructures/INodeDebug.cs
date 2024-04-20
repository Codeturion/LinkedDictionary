namespace Codeturion.DataStructures
{
    // Make debug logging easy for now
// Not for using inside the code
    public interface INodeDebug
    {
        string KeyAsString { get; }
        INodeDebug? Next { get; }
        INodeDebug? Previous { get; }
    }
}