namespace Codeturion.Scripts.Data.Nodes
{
    // Make debug logging easy for now
// Not for using inside the code
    public interface INodeDebug
    {
        string KeyAsString { get; }
        INodeDebug? Next { get; }
        INodeDebug? Previous { get; }
    }
    
    // Possible to add interface for int,string etc. if needed.
}