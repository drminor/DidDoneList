using System;
using Bridge.React;

namespace BridgeReactTutorial.Components
{
    public class ValidatedTextInput : StatelessComponent<ValidatedTextInput.Props>
    {
        public ValidatedTextInput(Props props) : base(props) { }

        public override ReactElement Render()
        {
            var className = props.ClassName;
            if (!string.IsNullOrWhiteSpace(props.ValidationMessage))
                className = (className + " invalid").Trim();

            return DOM.Span(new Attributes { ClassName = className },
              new TextInput(new TextInput.Props
              {
                  ClassName = props.ClassName,
                  Disabled = props.Disabled,
                  Content = props.Content,
                  OnChange = props.OnChange
              }),
              string.IsNullOrWhiteSpace(props.ValidationMessage)
                ? null
                : DOM.Span(
                  new Attributes { ClassName = "validation-message" },
                  props.ValidationMessage
                )
            );
        }

        public class Props
        {
            public string ClassName;
            public bool Disabled;
            public string Content;
            public Action<string> OnChange;
            public string ValidationMessage;
        }
    }
}