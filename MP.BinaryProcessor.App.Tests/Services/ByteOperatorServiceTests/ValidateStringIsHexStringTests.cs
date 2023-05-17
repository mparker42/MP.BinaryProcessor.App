namespace MP.BinaryProcessor.App.Tests.Services.ByteOperatorServiceTests
{
    [TestClass]
    public class ValidateStringIsHexStringTests : BaseTests
    {
        [TestMethod]
        [DataRow("Not a hex string")]
        [DataRow("-1")]
        [DataRow("GG")]
        [DataRow("T")]
        public void InvalidStringsAreNotValid(string invalidString)
        {
            var response = ClassUnderTest.ValidateStringIsHexString(invalidString);

            response.Should().BeFalse();
        }

        [TestMethod]
        [DataRow("0000FFAA22")]
        [DataRow("123456AB")]
        public void ValidStringsAreValid(string invalidString)
        {
            var response = ClassUnderTest.ValidateStringIsHexString(invalidString);

            response.Should().BeTrue();
        }
    }
}
