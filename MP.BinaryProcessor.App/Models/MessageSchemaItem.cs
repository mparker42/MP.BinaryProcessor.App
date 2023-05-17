using MP.BinaryProcessor.App.Enums;

namespace MP.BinaryProcessor.App.Models
{
    public class MessageSchemaItem
    {
        public string DisplayName { get; set; } = "";
        public int StartPosition { get; set; }
        public int Length { get; set; }
        public bool IsHeader { get; set; }

        public BinaryDataType BinaryDataType { get; set; }
    }
}
