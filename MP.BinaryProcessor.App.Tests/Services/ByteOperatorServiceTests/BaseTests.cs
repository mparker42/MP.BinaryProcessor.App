using MP.BinaryProcessor.App.Services;

namespace MP.BinaryProcessor.App.Tests.Services.ByteOperatorServiceTests
{
    public class BaseTests
    {
        public readonly ByteOperatorService ClassUnderTest;

        public BaseTests()
        {
            ClassUnderTest = new ByteOperatorService();
        }
    }
}
