using MP.BinaryProcessor.App.Enums;
using MP.BinaryProcessor.App.Models;

namespace MP.BinaryProcessor.App.Schemas
{
    public static class MessageSchema
    {
        public static readonly MessageSchemaItem[] MessageSchemaItems = new[]
                {
                    new MessageSchemaItem
                    {
                        DisplayName = "Message type",
                        StartPosition = 0, Length = 2,
                        IsHeader = true,
                        BinaryDataType = BinaryDataType.MessageType
                    },
                    new MessageSchemaItem
                    {
                        DisplayName = "Event time",
                        StartPosition = 2,
                        Length = 4,
                        IsHeader = true,
                        BinaryDataType = BinaryDataType.DateTimeInElapsedSeconds
                    },
                    new MessageSchemaItem
                    {
                        DisplayName = "Device id",
                        StartPosition = 6,
                        Length = 6,
                        IsHeader = true,
                        BinaryDataType = BinaryDataType.AsciiString
                    },
                    new MessageSchemaItem
                    {
                        DisplayName = "Current Speed",
                        StartPosition = 12,
                        Length = 2,
                        BinaryDataType = BinaryDataType.LittleEndianInt16
                    },
                    new MessageSchemaItem
                    {
                        DisplayName = "Oedometer",
                        StartPosition = 14,
                        Length = 4,
                        BinaryDataType = BinaryDataType.LittleEndianInt32
                    },
                    new MessageSchemaItem
                    {
                        DisplayName = "Trip id",
                        StartPosition = 18,
                        Length = 4,
                        BinaryDataType = BinaryDataType.LittleEndianInt32
                    },
                    new MessageSchemaItem
                    {
                        DisplayName = "Trip start",
                        StartPosition = 22,
                        Length = 1,
                        BinaryDataType = BinaryDataType.BooleanByte
                    },
                    new MessageSchemaItem
                    {
                        DisplayName = "Trip end",
                        StartPosition = 23,
                        Length = 1,
                        BinaryDataType = BinaryDataType.BooleanByte
                    },
                    new MessageSchemaItem
                    {
                        DisplayName = "Latitude",
                        StartPosition = 24,
                        Length = 4,
                        BinaryDataType = BinaryDataType.DecimalFromInt32
                    },
                    new MessageSchemaItem
                    {
                        DisplayName = "Longitude",
                        StartPosition = 28,
                        Length = 4,
                        BinaryDataType = BinaryDataType.DecimalFromInt32
                    },
                };
    }
}
