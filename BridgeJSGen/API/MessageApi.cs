using System;
using System.Collections.Generic;
using Bridge;
using Bridge.Html5;
using Bridge.React;
using BridgeReactTutorial.Actions;
using BridgeReactTutorial.ViewModels;

namespace BridgeReactTutorial.API
{
    public class MessageApi : IReadAndWriteMessages
    {
        private readonly AppDispatcher _dispatcher;
        private readonly List<Tuple<int, MessageDetails>> _messages;
        public MessageApi(AppDispatcher dispatcher)
        {
            if (dispatcher == null)
                throw new ArgumentException("dispatcher");

            _dispatcher = dispatcher;
            _messages = new List<Tuple<int, MessageDetails>>();

            // To further mimic a server-based API (where other people may be recording messages
            // of their own), after a 10s delay a periodic task will be executed to retrieve a
            // new message
            Window.SetTimeout(
              () => Window.SetInterval(GetChuckNorrisFact, 5000),
              10000
            );
        }

        public RequestId SaveMessage(MessageDetails message)
        {
            return SaveMessage(message, optionalSaveCompletedCallback: null);
        }

        private RequestId SaveMessage(
          MessageDetails message,
          Action optionalSaveCompletedCallback)
        {
            if (message == null)
                throw new ArgumentNullException("message");
            if (string.IsNullOrWhiteSpace(message.Title))
                throw new ArgumentException("A title value must be provided");
            if (string.IsNullOrWhiteSpace(message.Content))
                throw new ArgumentException("A content value must be provided");

            var requestId = new RequestId();
            Window.SetTimeout(
              () =>
              {
                  _messages.Add(Tuple.Create(_messages.Count, message));
                  _dispatcher.Dispatch(
              new MessageSaveSucceeded { RequestId = requestId }
            );
                  if (optionalSaveCompletedCallback != null)
                      optionalSaveCompletedCallback();
              },
              1000 // Simulate a roundtrip to the server
            );
            return requestId;
        }

        public RequestId GetMessages()
        {
            // ToArray is used to return a clone of the message set - otherwise, the caller would
            // end up with a list that is updated when the internal reference within this class
            // is updated (which sounds convenient but it's not the behaviour that would be
            // exhibited if this was really persisting messages to a server somewhere)
            var requestId = new RequestId();
            Window.SetTimeout(
              () => _dispatcher.Dispatch(new MessageHistoryUpdated
              {
                  RequestId = requestId,
                  Messages = _messages.ToArray()
              }),
              1000 // Simulate a roundtrip to the server
            );
            return requestId;
        }

        private void GetChuckNorrisFact()
        {
            var request = new XMLHttpRequest();
            request.ResponseType = XMLHttpRequestResponseType.Json;
            request.OnReadyStateChange = () =>
            {
                if (request.ReadyState != AjaxReadyState.Done)
                    return;

                if ((request.Status == 200) || (request.Status == 304))
                {
                    try
                    {
                        var apiResponse = (ChuckNorrisFactApiResponse)request.Response;
                        if ((apiResponse.Type == "success")
                        && (apiResponse.Value != null)
                        && !string.IsNullOrWhiteSpace(apiResponse.Value.Joke))
                        {
                            // The Chuck Norris Facts API (http://www.icndb.com/api/) returns strings
                            // html-encoded, so they need decoding before be wrapped up in a
                            // MessageDetails instance
                            // - Note: After the save has been processed, GetMessages is called so
                            //   that a MessageHistoryUpdate action is dispatched
                            SaveMessage(
                              new MessageDetails
                              {
                                  Title = "Fact",
                                  Content = HtmlDecode(apiResponse.Value.Joke)
                              },
                              () => GetMessages()
                            );
                            return;
                        }
                    }
                    catch
                    {
                        // Ignore any error and drop through to the fallback message-generator below
                    }
                }
                SaveMessage(new MessageDetails
                {
                    Title = "Fact",
                    Content = "API call failed when polling for server content :("
                });
            };
            request.Open("GET", "http://api.icndb.com/jokes/random");
            request.Send();
        }

        private string HtmlDecode(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            var wrapper = Document.CreateElement("div");
            wrapper.InnerHTML = value;
            return wrapper.TextContent;
        }

        [IgnoreCast]
        private class ChuckNorrisFactApiResponse
        {
            public extern string Type { [Template("type")] get; }
            public extern FactDetails Value { [Template("value")] get; }

            [IgnoreCast]
            public class FactDetails
            {
                public extern int Id { [Template("id")] get; }
                public extern string Joke { [Template("joke")]get; }
            }
        }
    }
}