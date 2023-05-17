using MP.BinaryProcessor.App.Models;

namespace MP.BinaryProcessor.App.Tests.Handlers.DecodeMessageHandlerTests
{
    [TestClass]
    public class DecodeMessageHandlerInt16Tests : BaseTests
    {
        private const string _fakeSchemaDisplayName = "fake schema item";

        private readonly byte[] _allBytes = new byte[] { 0x16, 0xf2, 0x3a, 0x65, 0x23, 0x12, 0xaa };

        private const int response = 2316;

        public override MessageSchemaItem[] GetMessageSchemaItems()
        {
            return new[] { new MessageSchemaItem { DisplayName = _fakeSchemaDisplayName, BinaryDataType = Enums.BinaryDataType.LittleEndianInt16, Length = 4, StartPosition = 2 } };
        }

        private bool ValidateRead(IEnumerable<byte> actual)
        {
            actual.Should().BeEquivalentTo(new[] { 0x3a, 0x65, 0x23, 0x12 });

            return true;
        }

        [TestMethod]
        public void HandlerReadsInt16()
        {
            ByteOperatorServiceMock.Setup(m => m.ConvertHexStringToByteArray(It.IsAny<string>())).Returns(() => _allBytes);

            ByteOperatorServiceMock.Setup(m => m.ReadBytesAsInt16(It.IsAny<IEnumerable<byte>>())).Returns(() => response);

            var input = "input string";

            var decodeResult = ClassUnderTest.DecodeMessage(input);

            decodeResult.Should().Contain($"{_fakeSchemaDisplayName}: {response}");

            ByteOperatorServiceMock.Verify(x => x.ConvertHexStringToByteArray(input), Times.Once);

            ByteOperatorServiceMock.Verify(m => m.ReadBytesAsInt16(It.Is<IEnumerable<byte>>(b => ValidateRead(b))));
        }
    }
}
