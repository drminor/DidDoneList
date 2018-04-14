using System;
using System.Collections.Generic;
using Bridge.React;
using BridgeReactTutorial.Actions;
using BridgeReactTutorial.API;
using BridgeReactTutorial.ViewModels;

namespace BridgeReactTutorial.Stores
{
    public class MessageWriterStore
    {
        private RequestId _saveActionRequestId, _lastDataUpdatedRequestId;
        public MessageWriterStore(IReadAndWriteMessages messageApi, AppDispatcher dispatcher)
        {
            if (messageApi == null)
                throw new ArgumentNullException("messageApi");
            if (dispatcher == null)
                throw new ArgumentNullException("dispatcher");

            Message = GetInitialMessageEditState();
            MessageHistory = new Tuple<int, MessageDetails>[0];

            dispatcher.Receive(action =>
            {
                if (action is StoreInitialised)
                {
                    var storeInitialised = (StoreInitialised)action;
                    if (storeInitialised.Store == this)
                        OnChange();
                }
                else if (action is MessageEditStateChanged)
                {
                    var messageEditStateChanged = (MessageEditStateChanged)action;
                    Message = messageEditStateChanged.NewState;
                    ValidateMessage(Message);
                    OnChange();
                }
                else if (action is MessageSaveRequested)
                {
                    var messageSaveRequested = (MessageSaveRequested)action;
                    _saveActionRequestId = messageApi.SaveMessage(messageSaveRequested.Message);
                    Message.IsSaveInProgress = true;
                    OnChange();
                }
                else if (action is MessageSaveSucceeded)
                {
                    var messageSaveSucceeded = (MessageSaveSucceeded)action;
                    if (messageSaveSucceeded.RequestId == _saveActionRequestId)
                    {
                        _saveActionRequestId = null;
                        Message = GetInitialMessageEditState();
                        OnChange();
                        _lastDataUpdatedRequestId = messageApi.GetMessages();
                    }
                }
                else if (action is MessageHistoryUpdated)
                {
                    var messageHistoryUpdated = (MessageHistoryUpdated)action;
                    if ((_lastDataUpdatedRequestId == null)
                    || (_lastDataUpdatedRequestId == messageHistoryUpdated.RequestId)
                    || messageHistoryUpdated.RequestId.ComesAfter(_lastDataUpdatedRequestId))
                    {
                        _lastDataUpdatedRequestId = messageHistoryUpdated.RequestId;
                        MessageHistory = messageHistoryUpdated.Messages;
                        OnChange();
                    }
                }
            });
        }

        public event Action Change;
        public MessageEditState Message;
        public IEnumerable<Tuple<int, MessageDetails>> MessageHistory;

        private MessageEditState GetInitialMessageEditState()
        {
            // Note: By using the ValidateMessage here, we don't need to duplicate the "Untitled"
            // string that should be used for the Caption value when the UI is first rendered
            // or when the user has entered some Title content but then deleted it again.
            // Similarly, we avoid having to repeat the validation messages that should be
            // displayed when the form is empty, since they will be set by ValidateMessage.
            var blankMessage = new MessageEditState
            {
                Caption = "",
                Title = new TextEditState { Text = "" },
                Content = new TextEditState { Text = "" },
                IsSaveInProgress = false
            };
            ValidateMessage(blankMessage);
            return blankMessage;
        }

        private void ValidateMessage(MessageEditState message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            message.Caption = string.IsNullOrWhiteSpace(message.Title.Text)
              ? "Untitled"
              : message.Title.Text.Trim();
            message.Title.ValidationError = string.IsNullOrWhiteSpace(message.Title.Text)
              ? "Must enter a title"
              : null;
            message.Content.ValidationError = string.IsNullOrWhiteSpace(message.Content.Text)
              ? "Must enter message content"
              : null;
        }

        private void OnChange()
        {
            if (Change != null)
                Change();
        }
    }
}