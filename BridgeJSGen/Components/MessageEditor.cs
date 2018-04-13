using System;
using Bridge.React;
using BridgeReactTutorial.ViewModels;

namespace BridgeReactTutorial.Components
{
    public class MessageEditor : StatelessComponent<MessageEditor.Props>
    {
        public MessageEditor(Props props) : base(props) { }

        public override ReactElement Render()
        {
            return DOM.FieldSet(new FieldSetAttributes { ClassName = props.ClassName },
              DOM.Legend(null, string.IsNullOrWhiteSpace(props.Title) ? "Untitled" : props.Title),
              DOM.Span(new Attributes { ClassName = "label" }, "Title"),
              new TextInput(new TextInput.Props
              {
                  ClassName = "title",
                  Content = props.Title,
                  OnChange = newTitle => props.OnChange(new MessageDetails
                  {
                      Title = newTitle,
                      Content = props.Content
                  })
              }),
              DOM.Span(new Attributes { ClassName = "label" }, "Content"),
              new TextInput(new TextInput.Props
              {
                  ClassName = "content",
                  Content = props.Content,
                  OnChange = newContent => props.OnChange(new MessageDetails
                  {
                      Title = props.Title,
                      Content = newContent
                  })
              })
            );
        }

        public class Props
        {
            public string ClassName;
            public string Title;
            public string Content;
            public Action<MessageDetails> OnChange;
        }
    }
}
