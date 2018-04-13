using Bridge.Html5;
using Bridge.React;
using System;

namespace BridgeReactTutorial.Components
{
    public class TextInput : StatelessComponent<TextInput.Props>
    {
        public TextInput(Props props) : base(props) { }

        public override ReactElement Render()
        {
            return DOM.Input(new InputAttributes
            {
                Type = InputType.Text,
                ClassName = props.ClassName,
                Value = props.Content,
                OnChange = e => props.OnChange(e.CurrentTarget.Value)
            });
        }

        public class Props
        {
            public string ClassName;
            public string Content;
            public Action<string> OnChange;
        }
    }
}
