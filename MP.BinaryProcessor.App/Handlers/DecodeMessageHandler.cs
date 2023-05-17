using MP.BinaryProcessor.App.Enums;
using MP.BinaryProcessor.App.Interfaces.Handlers;
using MP.BinaryProcessor.App.Interfaces.Services;
using MP.BinaryProcessor.App.Models;
using System.Text;

namespace MP.BinaryProcessor.App.Handlers
{
    public class DecodeMessageHandler : IDecodeMessageHandler
    {
        private readonly IByteOperatorService _byteOperatorService;
        private readonly MessageSchemaItem[] _messageSchemaItems;

        public DecodeMessageHandler(IByteOperatorService byteOperatorService, MessageSchemaItem[] messageSchemaItems)
        {
            _byteOperatorService = byteOperatorService;

            // Injected from MessageSchema.MessageSchemaItems to make unit testing simpler.
            _messageSchemaItems = messageSchemaItems;
        }

        private IEnumerable<byte> GetMessageItemBytes(MessageSchemaItem schemaItem, byte[] messageBytes)
        {
            return messageBytes.Skip(schemaItem.StartPosition).Take(schemaItem.Length);
        }

        public string DecodeMessage(string message)
        {
            var messageBytes = _byteOperatorService.ConvertHexStringToByteArray(message);

            var headerItems = new List<string>();
            var bodyItems = new List<string>();

            foreach (var item in _messageSchemaItems)
            {
                var dataAsString = "";
                var schemaItemBytes = GetMessageItemBytes(item, messageBytes);

                switch (item.BinaryDataType)
                {
                    case BinaryDataType.LittleEndianInt16:
                        dataAsString = _byteOperatorService.ReadBytesAsInt16(schemaItemBytes).ToString();
                        break;
                    case BinaryDataType.LittleEndianInt32:
                        dataAsString = _byteOperatorService.ReadBytesAsInt32(schemaItemBytes).ToString();
                        break;
                    case BinaryDataType.AsciiString:
                        dataAsString = _byteOperatorService.ReadBytesAsAsciiString(schemaItemBytes).ToString();
                        break;
                    case BinaryDataType.BooleanByte:
                        dataAsString = _byteOperatorService.ReadBytesAsBool(schemaItemBytes) ? "True" : "False";
                        break;
                    case BinaryDataType.DateTimeInElapsedSeconds:
                        var dateTimeElapsedSeconds = _byteOperatorService.ReadBytesAsInt32(schemaItemBytes);

                        var dateTime = DateTime.Parse("2000-01-01T00:00:00Z").AddSeconds(dateTimeElapsedSeconds);

                        dataAsString = dateTime.ToString("dd/MM/yyyy HH:mm:ss");
                        break;
                    case BinaryDataType.DecimalFromInt32:
                        var decimalBeforeTransformation = _byteOperatorService.ReadBytesAsInt32(schemaItemBytes);

                        var decimalValue = decimalBeforeTransformation / 600000.0m;

                        dataAsString = decimalValue.ToString();
                        break;
                    case BinaryDataType.MessageType:
                        dataAsString = _byteOperatorService.ReadBytesAsInt16(schemaItemBytes) == 255 ? "Developer Test Message" : "Unknown";
                        break;
                }

                var printedLine = $"{item.DisplayName}: {dataAsString}";

                if (item.IsHeader)
                {
                    headerItems.Add(printedLine);
                }
                else
                {
                    bodyItems.Add(printedLine);
                }
            }

            var stringBuider = new StringBuilder();

            stringBuider.AppendLine($@"[{DateTime.UtcNow:HH:mm:ss} INF] =========== beginning developer test message ======================
[{DateTime.UtcNow:HH:mm:ss} INF] =========== Header Start =======================================");

            foreach (var headerItem in headerItems)
            {
                stringBuider.AppendLine($"[{DateTime.UtcNow:HH:mm:ss} INF] {headerItem}");
            }

            stringBuider.AppendLine($@"[{DateTime.UtcNow:HH:mm:ss} INF] =========== Header End =======================================
[{DateTime.UtcNow:HH:mm:ss} INF] =========== Message data start =================================");

            foreach (var bodyItem in bodyItems)
            {
                stringBuider.AppendLine($"[{DateTime.UtcNow:HH:mm:ss} INF] {bodyItem}");
            }

            stringBuider.AppendLine($@"[{DateTime.UtcNow:HH:mm:ss} INF] =========== Message data end =================================
[{DateTime.UtcNow:HH:mm:ss} INF] =========== End Developer Test Message =======================");


            return stringBuider.ToString();
        }

        public IEnumerable<string> ValidateMessage(string message)
        {
            var errors = new List<string>();

            if (!_byteOperatorService.ValidateStringIsHexString(message))
            {
                errors.Add("The provided message is not a valid hex string");

                void ValidateMessagePart(int startPosition, int length, string displayName)
                {
                    if (message.Length >= (startPosition + length))
                    {
                        var messagePart = message.Substring(startPosition, length);

                        if (!_byteOperatorService.ValidateStringIsHexString(messagePart))
                        {
                            errors.Add($"The {displayName} is not a valid hex string (provided value {messagePart})");
                        }
                    }
                }

                foreach (var messageSchemaItem in _messageSchemaItems)
                {
                    ValidateMessagePart(messageSchemaItem.StartPosition, messageSchemaItem.Length, messageSchemaItem.DisplayName);
                }
            }

            if (message.Length != 64)
            {
                errors.Add($"Messages must be exacyly 32 characters long but the provided message is {message.Length} characters long");
            }

            return errors;
        }
    }
}
