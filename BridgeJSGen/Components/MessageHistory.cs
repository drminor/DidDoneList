using System;
using System.Collections.Generic;
using System.Linq;
using Bridge.React;
using BridgeReactTutorial.ViewModels;

namespace BridgeReactTutorial.Components
{
    public class MessageHistory : StatelessComponent<MessageHistory.Props>
    {
        public MessageHistory(Props props) : base(props) { }

        public override ReactElement Render()
        {
            var className = props.ClassName;
            if (!props.Messages.Any())
                className = (className + " zero-messages").Trim();

            // Any time a set of child components is dynamically-created (meaning that the
            // numbers of items may vary from one render to another), each must have a unique
            // "Key" property set (this may be a int or a string). Here, this is simple as
            // each message tuple is a unique id and the contents of that message (and the
            // unique id is ideal for use as a unique "Key" property).
            var messageElements = props.Messages
              .Select(idAndMessage => DOM.Div(new Attributes { Key = idAndMessage.Item1 },
                DOM.Span(new Attributes { ClassName = "title" }, idAndMessage.Item2.Title),
                DOM.Span(new Attributes { ClassName = "content" }, idAndMessage.Item2.Content)
              ));

            return DOM.FieldSet(new FieldSetAttributes { ClassName = className },
              DOM.Legend(null, "Message History"),
              DOM.Div(null, messageElements)
            );
        }

        public class Props
        {
            public string ClassName;
            public IEnumerable<Tuple<int, MessageDetails>> Messages;
        }
    }
}