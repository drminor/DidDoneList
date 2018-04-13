using Bridge.Html5;
using Bridge.React;
using BridgeReactTutorial.ViewModels;

namespace BridgeReactTutorial.Components
{
    public class AppContainer : Component<object, AppContainer.State>
    {
        public AppContainer() : base(null) { }

        protected override State GetInitialState()
        {
            return new State
            {
                Message = new MessageDetails { Title = "", Content = "" }
            };
        }

        public override ReactElement Render()
        {
            return new MessageEditor(new MessageEditor.Props
            {
                ClassName = "message",
                Title = state.Message.Title,
                Content = state.Message.Content,
                OnChange = newMessage => SetState(new State { Message = newMessage })
            });
        }

        public class State
        {
            public MessageDetails Message;
        }
    }
}
