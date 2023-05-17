using MP.BinaryProcessor.App.Models;

namespace MP.BinaryProcessor.App.Tests.Handlers.DecodeMessageHandlerTests
{
    [TestClass]
    public class DecodeMessageHandlerValidationTests : BaseTests
    {
        private const string _fakeSchemaDisplayName = "fake schema item";

        public override MessageSchemaItem[] GetMessageSchemaItems()
        {
            return new[] { new MessageSchemaItem { DisplayName = _fakeSchemaDisplayName } };
        }

        [TestMethod]
        public void ValidatorChecksValidMessageLength()
        {
            // We're not testing the validation behaviour here
            ByteOperatorServiceMock
                .Setup(x => x.ValidateStringIsHexString(It.IsAny<string>()))
                .Returns(() => true);

            var validationResult = ClassUnderTest.ValidateMessage("FF0006A6C92B3939393939316500DDCE0000D91100000100BC9EE00116C0EEFF");

            validationResult.Should().BeEmpty();

            ByteOperatorServiceMock.Verify(x => x.ValidateStringIsHexString(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [DataRow("A")]
        [DataRow("FF0006A6C92B3939393939316500DDCE0000D91100000100BC9EE00116C0EEFFASFFSDS")]
        [DataRow("FF0006A6C92B3939393939316500DDCE0000D")]
        public void ValidatorChecksInvalidMessageLength(string input)
        {
            // We're not testing the validation behaviour here
            ByteOperatorServiceMock
                .Setup(x => x.ValidateStringIsHexString(It.IsAny<string>()))
                .Returns(() => true);

            var validationResult = ClassUnderTest.ValidateMessage(input);

            validationResult
                .Should()
                .HaveCount(1)
                .And
                .AllSatisfy(i => i.Contains("characters long but the provided message"));

            ByteOperatorServiceMock.Verify(x => x.ValidateStringIsHexString(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void ValidatorChecksReportsInvalidHexStrings()
        {
            ByteOperatorServiceMock
                .Setup(x => x.ValidateStringIsHexString(It.IsAny<string>()))
                .Returns(() => false);

            var validationResult = ClassUnderTest.ValidateMessage("FF0006A6C92B3939393939316500DDCE0000D91100000100BC9EE00116C0EEFF");

            validationResult.Should().HaveCount(2);

            validationResult.ElementAt(0).Should().Contain("The provided message is not a valid hex string");

            validationResult.ElementAt(1).Should().Contain($"The {_fakeSchemaDisplayName} is not a valid hex string");

            ByteOperatorServiceMock.Verify(x => x.ValidateStringIsHexString(It.IsAny<string>()), Times.Exactly(2));
        }
    }
}
