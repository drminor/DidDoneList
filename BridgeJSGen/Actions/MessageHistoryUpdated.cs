using System;
using System.Collections.Generic;
using Bridge.React;
using BridgeReactTutorial.API;
using BridgeReactTutorial.ViewModels;

namespace BridgeReactTutorial.Actions
{
    public class MessageHistoryUpdated : IDispatcherAction
    {
        public RequestId RequestId;
        public IEnumerable<Tuple<int, MessageDetails>> Messages;
    }
}