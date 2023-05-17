using MP.BinaryProcessor.App.Interfaces.Services;
using System.Text;

namespace MP.BinaryProcessor.App.Services
{
    public class ByteOperatorService : IByteOperatorService
    {
        public bool ValidateStringIsHexString(string input)
        {
            const string validCharacters = "0123456789ABCDEF";

            return input.ToUpperInvariant().All(validCharacters.Contains);
        }

        public byte[] ConvertHexStringToByteArray(string input)
        {
            return Convert.FromHexString(input);
        }

        private ReadOnlySpan<byte> GetByteSpanForBitConverter(IEnumerable<byte> input)
        {
            // The BitConverter relies on endianness. The hex string input for this solution accepts little endian.
            // If the system is big endian then we need to reverse te array before sending to the BitConverter.
            if (!BitConverter.IsLittleEndian)
            {
                input = input.Reverse().ToArray();
            }

            return new ReadOnlySpan<byte>(input.ToArray());
        }

        public int ReadBytesAsInt32(IEnumerable<byte> input)
        {
            var readOnlyInput = GetByteSpanForBitConverter(input);

            return BitConverter.ToInt32(readOnlyInput);
        }

        public int ReadBytesAsInt16(IEnumerable<byte> input)
        {
            var readOnlyInput = GetByteSpanForBitConverter(input);

            return BitConverter.ToInt16(readOnlyInput);
        }

        public bool ReadBytesAsBool(IEnumerable<byte> input)
        {
            var readOnlyInput = GetByteSpanForBitConverter(input);

            return BitConverter.ToBoolean(readOnlyInput);
        }

        public string ReadBytesAsAsciiString(IEnumerable<byte> input)
        {
            var readOnlyInput = new ReadOnlySpan<byte>(input.ToArray());

            return Encoding.ASCII.GetString(readOnlyInput);
        }
    }
}
