namespace MP.BinaryProcessor.App.Tests.Services.ByteOperatorServiceTests
{
    [TestClass]
    public class ReadBytesAsInt16Tests : BaseTests
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new[]
                {
                    new object[] { new byte[] { 0xff, 0x0d }, 3583 },
                    new object[] { new byte[] { 0x03, 0x01 }, 259 }
                };
            }
        }

        [TestMethod]
        [DynamicData(nameof(TestData))]
        public void CanReadBytesAsInt16(byte[] input, int expected)
        {
            var actual = ClassUnderTest.ReadBytesAsInt16(input);

            actual.Should().Be(expected);
        }
    }
}
