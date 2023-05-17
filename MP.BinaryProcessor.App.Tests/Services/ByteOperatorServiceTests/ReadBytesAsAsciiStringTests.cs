namespace MP.BinaryProcessor.App.Tests.Services.ByteOperatorServiceTests
{
    [TestClass]
    public class ReadBytesAsAsciiStringTests : BaseTests
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new[]
                {
                    new object[] { new byte[] { 0x73, 0x6f, 0x6d, 0x65, 0x20, 0x74, 0x65, 0x78, 0x74, 0x20, 0x66, 0x72, 0x6f, 0x6d, 0x20, 0x62, 0x79, 0x74, 0x65, 0x73 }, "some text from bytes" },
                    new object[] { new byte[] { 0x61 }, "a" }
                };
            }
        }

        [TestMethod]
        [DynamicData(nameof(TestData))]
        public void CanReadBytesAsAsciiString(byte[] input, string expected)
        {
            var actual = ClassUnderTest.ReadBytesAsAsciiString(input);

            actual.Should().Be(expected);
        }
    }
}
