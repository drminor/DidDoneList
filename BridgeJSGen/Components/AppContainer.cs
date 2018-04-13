using System;
using System.Collections.Generic;
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
                IsSaveInProgress = false,
                MessageHistory = new Tuple<int, MessageDetails>[0]
            };
        }

        public override ReactElement Render()
        {
            return DOM.Div(null,
              new MessageEditor(new MessageEditor.Props
              {
                  ClassName = "message",
                  Title = state.Message.Title,
                  Content = state.Message.Content,
                  OnChange = newMessage => SetState(new State
                  {
                      Message = newMessage,
                      IsSaveInProgress = state.IsSaveInProgress,
                      MessageHistory = state.MessageHistory
                  }),
                  OnSave = async () =>
                  {
                // Set SaveInProgress to true while the save operation is requested
                SetState(new State
                      {
                          Message = state.Message,
                          IsSaveInProgress = true,
                          MessageHistory = state.MessageHistory
                      });
                      await props.MessageApi.SaveMessage(state.Message);

                // After the save has completed, clear the message entry form and reset
                // SaveInProgress to false
                SetState(new State
                      {
                          Message = new MessageDetails { Title = "", Content = "" },
                          IsSaveInProgress = false,
                          MessageHistory = state.MessageHistory
                      });

                // Then re-load the message history state and re-render when that data arrives
                var allMessages = await props.MessageApi.GetMessages();
                      SetState(new State
                      {
                          Message = state.Message,
                          IsSaveInProgress = state.IsSaveInProgress,
                          MessageHistory = allMessages
                      });
                  },
                  Disabled = state.IsSaveInProgress
              }),
              new MessageHistory(new MessageHistory.Props
              {
                  ClassName = "history",
                  Messages = state.MessageHistory
              })
            );
        }

        public class Props
        {
            public IReadAndWriteMessages MessageApi;
        }

        public class State
        {
            public MessageDetails Message;
            public bool IsSaveInProgress;
            public IEnumerable<Tuple<int, MessageDetails>> MessageHistory;
        }
    }
}