using System.Linq;
using Bridge.Html5;
using Bridge.React;
using BridgeReactTutorial.API;
using BridgeReactTutorial.Components;

namespace BridgeReactTutorial
{
    public class App
    {
        [Ready]
        public static void Go()
        {
            var container = Document.GetElementById("main");
            container.ClassName = string.Join(
              " ",
              container.ClassName.Split().Where(c => c != "loading")
            );
            React.Render(
              new AppContainer(new AppContainer.Props { MessageApi = new MessageApi() }),
              container
            );
        }
    }
}