using MP.BinaryProcessor.App.Handlers;
using MP.BinaryProcessor.App.Interfaces.Services;
using MP.BinaryProcessor.App.Models;

namespace MP.BinaryProcessor.App.Tests.Handlers.DecodeMessageHandlerTests
{
    public class BaseTests
    {
        public DecodeMessageHandler ClassUnderTest;

        public readonly Mock<IByteOperatorService> ByteOperatorServiceMock;

        private readonly MockRepository _mockRepository;

        public BaseTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            ByteOperatorServiceMock = _mockRepository.Create<IByteOperatorService>();

            var messageSchemaItems = GetMessageSchemaItems();

            ClassUnderTest = new DecodeMessageHandler(ByteOperatorServiceMock.Object, messageSchemaItems);
        }

        public virtual MessageSchemaItem[] GetMessageSchemaItems()
        {
            return new MessageSchemaItem[0];
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockRepository.VerifyNoOtherCalls();
        }
    }
}
