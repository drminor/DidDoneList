using Bridge.React;
using BridgeReactTutorial.API;
using BridgeReactTutorial.ViewModels;

namespace BridgeReactTutorial.Components
{
    public class AppContainer : Component<AppContainer.Props, AppContainer.State>
    {
        public AppContainer(AppContainer.Props props) : base(props) { }

        protected override State GetInitialState()
        {
            return new State
            {
                Message = new MessageDetails { Title = "", Content = "" },
                IsSaveInProgress = false
            };
        }

        public override ReactElement Render()
        {
            return new MessageEditor(new MessageEditor.Props
            {
                ClassName = "message",
                Title = state.Message.Title,
                Content = state.Message.Content,
                OnChange = newMessage => SetState(new State
                {
                    Message = newMessage,
                    IsSaveInProgress = state.IsSaveInProgress
                }),
                OnSave = async () =>
                {
                    SetState(new State { Message = state.Message, IsSaveInProgress = true });
                    await props.MessageApi.SaveMessage(state.Message);
                    SetState(new State
                    {
                        Message = new MessageDetails { Title = "", Content = "" },
                        IsSaveInProgress = false
                    });
                },
                Disabled = state.IsSaveInProgress
            });
        }

        public class Props
        {
            public IReadAndWriteMessages MessageApi;
        }

        public class State
        {
            public MessageDetails Message;
            public bool IsSaveInProgress;
        }
    }
}