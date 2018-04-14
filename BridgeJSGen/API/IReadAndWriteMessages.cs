using BridgeReactTutorial.ViewModels;

namespace BridgeReactTutorial.API
{
    public interface IReadAndWriteMessages
    {
        RequestId SaveMessage(MessageDetails message);
        RequestId GetMessages();
    }
}