using System;
using System.Collections.Generic;
using Bridge.React;
using BridgeReactTutorial.Actions;
using BridgeReactTutorial.ViewModels;
using BridgeReactTutorial.Stores;

namespace BridgeReactTutorial.Components
{
    public class AppContainer : Component<AppContainer.Props, AppContainer.State>
    {
        public AppContainer(AppContainer.Props props) : base(props) { }

        protected override void ComponentDidMount()
        {
            props.Store.Change += StoreChanged;
        }
        protected override void ComponentWillUnmount()
        {
            props.Store.Change -= StoreChanged;
        }
        private void StoreChanged()
        {
            SetState(new State
            {
                Message = props.Store.Message,
                MessageHistory = props.Store.MessageHistory
            });
        }

        public override ReactElement Render()
        {
            if (state == null)
                return null;

            return DOM.Div(null,
              new MessageEditor(new MessageEditor.Props
              {
                  ClassName = "message",
                  Message = state.Message,
                  OnChange = newState => props.Dispatcher.Dispatch(
              new MessageEditStateChanged { NewState = newState }
            ),
                  OnSave = () => props.Dispatcher.Dispatch(
              new MessageSaveRequested
                  {
                      Message = new MessageDetails
                      {
                          Title = state.Message.Title.Text,
                          Content = state.Message.Content.Text
                      }
                  }
            )
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
            public AppDispatcher Dispatcher;
            public MessageWriterStore Store;
        }

        public class State
        {
            public MessageEditState Message;
            public IEnumerable<Tuple<int, MessageDetails>> MessageHistory;
        }
    }
}