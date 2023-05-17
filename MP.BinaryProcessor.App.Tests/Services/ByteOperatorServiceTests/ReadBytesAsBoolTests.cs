namespace MP.BinaryProcessor.App.Tests.Services.ByteOperatorServiceTests
{
    [TestClass]
    public class ReadBytesAsBoolTests : BaseTests
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new[]
                {
                    new object[] { new byte[] { 0x1 }, true },
                    new object[] { new byte[] { 0x00 }, false }
                };
            }
        }

        [TestMethod]
        [DynamicData(nameof(TestData))]
        public void CanReadBytesAsBool(byte[] input, bool expected)
        {
            var actual = ClassUnderTest.ReadBytesAsBool(input);

            actual.Should().Be(expected);
        }
    }
}
