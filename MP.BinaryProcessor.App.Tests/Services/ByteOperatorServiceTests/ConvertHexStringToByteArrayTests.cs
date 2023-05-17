namespace MP.BinaryProcessor.App.Tests.Services.ByteOperatorServiceTests
{
    [TestClass]
    public class ConvertHexStringToByteArrayTests : BaseTests
    {
        [TestMethod]
        public void CanConvertHexStrings()
        {
            string input = "01020FFF2F";
            var expected = new byte[] { 0x01, 0x02, 0x0f, 0xff, 0x2f };

            var actual = ClassUnderTest.ConvertHexStringToByteArray(input);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
