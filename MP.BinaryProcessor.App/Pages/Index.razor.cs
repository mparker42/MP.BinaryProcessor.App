using Microsoft.AspNetCore.Components;
using MP.BinaryProcessor.App.Interfaces.Handlers;

namespace MP.BinaryProcessor.App.Pages
{
    public partial class Index
    {
        [Inject]
        private IDecodeMessageHandler? Handler { get; set; }

        private string _message = "";

        private string _handlerResponse = "";

        private void SubmitData()
        {
            var validationResponse = Handler!.ValidateMessage(_message);

            if(validationResponse.Any())
            {
                _handlerResponse = $@"Message processing failed with errors:
{string.Join(Environment.NewLine, validationResponse)}";

                return;
            }

            _handlerResponse = Handler.DecodeMessage(_message);
        }
    }
}
