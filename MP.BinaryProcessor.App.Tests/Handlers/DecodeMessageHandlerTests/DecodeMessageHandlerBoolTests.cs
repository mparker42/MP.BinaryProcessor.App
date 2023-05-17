using MP.BinaryProcessor.App.Models;

namespace MP.BinaryProcessor.App.Tests.Handlers.DecodeMessageHandlerTests
{
    [TestClass]
    public class DecodeMessageHandlerBoolTests : BaseTests
    {
        private const string _fakeSchemaDisplayName = "fake schema item";

        private readonly byte[] _allBytes = new byte[] { 0x32, 0xf2, 0x3a, 0x65, 0x23, 0x12, 0xaa };

        private const bool response = true;

        public override MessageSchemaItem[] GetMessageSchemaItems()
        {
            return new[] { new MessageSchemaItem { DisplayName = _fakeSchemaDisplayName, BinaryDataType = Enums.BinaryDataType.BooleanByte, Length = 4, StartPosition = 2 } };
        }

        private bool ValidateRead(IEnumerable<byte> actual)
        {
            actual.Should().BeEquivalentTo(new[] { 0x3a, 0x65, 0x23, 0x12 });

            return true;
        }

        [TestMethod]
        public void HandlerReadsBool()
        {
            ByteOperatorServiceMock.Setup(m => m.ConvertHexStringToByteArray(It.IsAny<string>())).Returns(() => _allBytes);

            ByteOperatorServiceMock.Setup(m => m.ReadBytesAsBool(It.IsAny<IEnumerable<byte>>())).Returns(() => response);

            var input = "input string";

            var decodeResult = ClassUnderTest.DecodeMessage(input);

            decodeResult.Should().Contain($"{_fakeSchemaDisplayName}: True");

            ByteOperatorServiceMock.Verify(x => x.ConvertHexStringToByteArray(input), Times.Once);

            ByteOperatorServiceMock.Verify(m => m.ReadBytesAsBool(It.Is<IEnumerable<byte>>(b => ValidateRead(b))));
        }
    }
}
