using System.Linq;
using Bridge.Html5;
using Bridge.React;
using BridgeReactTutorial.Actions;
using BridgeReactTutorial.API;
using BridgeReactTutorial.Components;
using BridgeReactTutorial.Stores;

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

            var dispatcher = new AppDispatcher();
            var messageApi = new MessageApi(dispatcher);
            var store = new MessageWriterStore(messageApi, dispatcher);
            React.Render(
              new AppContainer(new AppContainer.Props
              {
                  Dispatcher = dispatcher,
                  Store = store
              }),
              container
            );
            dispatcher.Dispatch(new StoreInitialised { Store = store });
        }
    }
}