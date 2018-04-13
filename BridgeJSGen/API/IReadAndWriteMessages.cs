using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BridgeReactTutorial.ViewModels;

namespace BridgeReactTutorial.API
{
    public interface IReadAndWriteMessages
    {
        Task SaveMessage(MessageDetails message);
        Task<IEnumerable<Tuple<int, MessageDetails>>> GetMessages();
    }
}