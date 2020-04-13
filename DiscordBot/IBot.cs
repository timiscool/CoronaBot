using System;
using System.Threading.Tasks;

namespace DiscordBot
{
    public interface IBot
    {
        Task RunAsync();
    }
}
