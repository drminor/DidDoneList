using Bridge.React;
using BridgeReactTutorial.ViewModels;

namespace BridgeReactTutorial.Actions
{
    public class MessageSaveRequested : IDispatcherAction
    {
        public MessageDetails Message;
    }
}
