using System.Linq;
using Bridge.Html5;
using Bridge.React;

namespace BridgeReactTutorial
{
    public class Class1
    {
        [Ready]
        public static void Main()
        {
            var container = Document.GetElementById("main");
            container.ClassName = string.Join(
              " ",
              container.ClassName.Split().Where(c => c != "loading")
            );
            React.Render(
              DOM.Div(new Attributes { ClassName = "welcome" }, "Hi!"),
              container
            );
        }
    }
}