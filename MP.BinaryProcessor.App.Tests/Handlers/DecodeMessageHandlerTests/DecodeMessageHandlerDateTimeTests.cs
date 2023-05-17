using MP.BinaryProcessor.App.Models;

namespace MP.BinaryProcessor.App.Tests.Handlers.DecodeMessageHandlerTests
{
    [TestClass]
    public class DecodeMessageHandlerDateTimeTests : BaseTests
    {
        private const string _fakeSchemaDisplayName = "fake schema item";

        private readonly byte[] _allBytes = new byte[] { 0x32, 0xf2, 0x3a, 0x65, 0x23, 0x12, 0xaa };

        private const int response = 2_592_000;

        public override MessageSchemaItem[] GetMessageSchemaItems()
        {
            return new[] { new MessageSchemaItem { DisplayName = _fakeSchemaDisplayName, BinaryDataType = Enums.BinaryDataType.DateTimeInElapsedSeconds, Length = 4, StartPosition = 2 } };
        }

        private bool ValidateRead(IEnumerable<byte> actual)
        {
            actual.Should().BeEquivalentTo(new[] { 0x3a, 0x65, 0x23, 0x12 });

            return true;
        }

        [TestMethod]
        public void HandlerReadsDateTime()
        {
            ByteOperatorServiceMock.Setup(m => m.ConvertHexStringToByteArray(It.IsAny<string>())).Returns(() => _allBytes);

            ByteOperatorServiceMock.Setup(m => m.ReadBytesAsInt32(It.IsAny<IEnumerable<byte>>())).Returns(() => response);

            var input = "input string";

            var decodeResult = ClassUnderTest.DecodeMessage(input);

            decodeResult.Should().Contain($"{_fakeSchemaDisplayName}: 31/01/2000 00:00:00");

            ByteOperatorServiceMock.Verify(x => x.ConvertHexStringToByteArray(input), Times.Once);

            ByteOperatorServiceMock.Verify(m => m.ReadBytesAsInt32(It.Is<IEnumerable<byte>>(b => ValidateRead(b))));
        }
    }
}
