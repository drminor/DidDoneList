using Bridge.React;
using BridgeReactTutorial.ViewModels;

namespace BridgeReactTutorial.Actions
{
    public class MessageEditStateChanged : IDispatcherAction
    {
        public MessageEditState NewState;
    }
}