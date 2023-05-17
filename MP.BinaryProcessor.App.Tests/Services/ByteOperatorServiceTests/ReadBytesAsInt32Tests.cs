namespace MP.BinaryProcessor.App.Tests.Services.ByteOperatorServiceTests
{
    [TestClass]
    public class ReadBytesAsInt32Tests : BaseTests
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new[]
                {
                    new object[] { new byte[] { 0xff, 0xad, 0x00, 0x00 }, 44543 },
                    new object[] { new byte[] { 0xdd, 0xce, 0x00, 0x00 }, 52957 },
                    new object[] { new byte[] { 0xa3, 0x77, 0x69, 0x77 }, 2_003_400_611 }
                };
            }
        }

        [TestMethod]
        [DynamicData(nameof(TestData))]
        public void CanReadBytesAsInt32(byte[] input, int expected)
        {
            var actual = ClassUnderTest.ReadBytesAsInt32(input);

            actual.Should().Be(expected);
        }
    }
}
