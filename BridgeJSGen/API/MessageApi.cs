using System;
using System.Threading.Tasks;
using Bridge.Html5;
using BridgeReactTutorial.ViewModels;

namespace BridgeReactTutorial.API
{
    public class MessageApi : IReadAndWriteMessages
    {
        public Task SaveMessage(MessageDetails message)
        {
            if (message == null)
                throw new ArgumentNullException("message");
            if (string.IsNullOrWhiteSpace(message.Title))
                throw new ArgumentException("A title value must be provided");
            if (string.IsNullOrWhiteSpace(message.Content))
                throw new ArgumentException("A content value must be provided");

            var task = new Task<object>(null);
            Window.SetTimeout(
              () => task.Complete(),
              1000 // Simulate a roundtrip to the server
            );
            return task;
        }
    }
}