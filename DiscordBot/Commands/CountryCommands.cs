using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
    using System.Globalization;
using DSharpPlus.Entities;

namespace DiscordBot.Commands
{
    public class FirstCommand : BaseCommandModule
    {
        private readonly CoronaMetrics _metrics = new CoronaMetrics();



        [Command("!d"), Aliases("Deaths"), Description("Returns a live count of deaths for provided country")]
        public async Task Deaths(CommandContext ctx, string country)
        {
			var myembded = new DiscordEmbedBuilder()
			{
				Title = "Death Stats",
			    Color = DiscordColor.Blue,
                

			};

			// await to be sequencial
			//await ctx.Channel.SendMessageAsync(embed: myembded, content: _metrics.GetDeathsByCountry(country)).ConfigureAwait(false);
		    await ctx.Channel.SendMessageAsync(_metrics.GetDeathsByCountry(country)).ConfigureAwait(false);
		}



		[Command("!s"), Aliases("Summary"), Description("Returns a live summary of Corona Virus metrics for provided country")]
		public async Task Summaries(CommandContext ctx, string country)
		{
			// await to be sequencial
			await ctx.Channel.SendMessageAsync(_metrics.GetSummaryByCountry(country)).ConfigureAwait(false);
		}





	}
}
