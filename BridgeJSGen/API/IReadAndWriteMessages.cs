using System.Threading.Tasks;
using BridgeReactTutorial.ViewModels;

namespace BridgeReactTutorial.API
{
    public interface IReadAndWriteMessages
    {
        Task SaveMessage(MessageDetails message);
    }
}