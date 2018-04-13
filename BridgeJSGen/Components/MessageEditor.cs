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
            var formIsInvalid =
              string.IsNullOrWhiteSpace(props.Title) ||
              string.IsNullOrWhiteSpace(props.Content);

            return DOM.FieldSet(new FieldSetAttributes { ClassName = props.ClassName },
              DOM.Legend(null, string.IsNullOrWhiteSpace(props.Title) ? "Untitled" : props.Title),
              DOM.Span(new Attributes { ClassName = "label" }, "Title"),
              new ValidatedTextInput(new ValidatedTextInput.Props
              {
                  ClassName = "title",
                  Disabled = props.Disabled,
                  Content = props.Title,
                  OnChange = newTitle => props.OnChange(new MessageDetails
                  {
                      Title = newTitle,
                      Content = props.Content
                  }),
                  ValidationMessage = string.IsNullOrWhiteSpace(props.Title)
                  ? "Must enter a title"
                  : null
              }),
              DOM.Span(new Attributes { ClassName = "label" }, "Content"),
              new ValidatedTextInput(new ValidatedTextInput.Props
              {
                  ClassName = "content",
                  Disabled = props.Disabled,
                  Content = props.Content,
                  OnChange = newContent => props.OnChange(new MessageDetails
                  {
                      Title = props.Title,
                      Content = newContent
                  }),
                  ValidationMessage = string.IsNullOrWhiteSpace(props.Content)
                  ? "Must enter message content"
                  : null
              }),
              DOM.Button(
                new ButtonAttributes
                {
                    Disabled = props.Disabled || formIsInvalid,
                    OnClick = e => props.OnSave()
                },
                "Save"
              )
            );
        }

        public class Props
        {
            public string ClassName;
            public string Title;
            public string Content;
            public Action<MessageDetails> OnChange;
            public Action OnSave;
            public bool Disabled;
        }
    }
}