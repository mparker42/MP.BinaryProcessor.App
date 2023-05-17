namespace MP.BinaryProcessor.App.Interfaces.Handlers
{
    public interface IDecodeMessageHandler
    {
        IEnumerable<string> ValidateMessage(string message);

        string DecodeMessage(string message);
    }
}
