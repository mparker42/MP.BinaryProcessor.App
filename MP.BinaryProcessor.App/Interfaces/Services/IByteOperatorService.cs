namespace MP.BinaryProcessor.App.Interfaces.Services
{
    public interface IByteOperatorService
    {
        bool ValidateStringIsHexString(string input);

        Byte[] ConvertHexStringToByteArray(string input);


        int ReadBytesAsInt32(IEnumerable<byte> input);

        int ReadBytesAsInt16(IEnumerable<byte> input);

        bool ReadBytesAsBool(IEnumerable<byte> input);

        string ReadBytesAsAsciiString(IEnumerable<byte> input);
    }
}
