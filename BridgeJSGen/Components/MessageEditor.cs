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
              !string.IsNullOrWhiteSpace(props.Message.Title.ValidationError) ||
              !string.IsNullOrWhiteSpace(props.Message.Content.ValidationError);

            return DOM.FieldSet(new FieldSetAttributes { ClassName = props.ClassName },
              DOM.Legend(null, props.Message.Caption),
              DOM.Span(new Attributes { ClassName = "label" }, "Title"),
              new ValidatedTextInput(new ValidatedTextInput.Props
              {
                  ClassName = "title",
                  Disabled = props.Message.IsSaveInProgress,
                  Content = props.Message.Title.Text,
                  OnChange = newTitle => props.OnChange(new MessageEditState
                  {
                      Title = new TextEditState { Text = newTitle },
                      Content = props.Message.Content
                  }),
                  ValidationMessage = props.Message.Title.ValidationError
              }),
              DOM.Span(new Attributes { ClassName = "label" }, "Content"),
              new ValidatedTextInput(new ValidatedTextInput.Props
              {
                  ClassName = "content",
                  Disabled = props.Message.IsSaveInProgress,
                  Content = props.Message.Content.Text,
                  OnChange = newContent => props.OnChange(new MessageEditState
                  {
                      Title = props.Message.Title,
                      Content = new TextEditState { Text = newContent },
                  }),
                  ValidationMessage = props.Message.Content.ValidationError
              }),
              DOM.Button(
                new ButtonAttributes
                {
                    Disabled = formIsInvalid || props.Message.IsSaveInProgress,
                    OnClick = e => props.OnSave()
                },
                "Save"
              )
            );
        }

        public class Props
        {
            public string ClassName;
            public MessageEditState Message;
            public Action<MessageEditState> OnChange;
            public Action OnSave;
        }
    }
}