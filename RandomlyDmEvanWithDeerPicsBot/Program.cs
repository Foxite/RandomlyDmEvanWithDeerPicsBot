using System.Net.Http.Headers;
using DSharpPlus;
using DSharpPlus.Entities;

string botToken = Environment.GetEnvironmentVariable("BOT_TOKEN")!;
ulong targetId = ulong.Parse(Environment.GetEnvironmentVariable("TARGET_ID")!);

var random = new Random();
var http = new HttpClient() {
	DefaultRequestHeaders = {
		UserAgent = {
			new ProductInfoHeaderValue("RandomlyDmEvanWithDeerPicsBot", "0.1"),
			new ProductInfoHeaderValue("(https://github.com/Foxite/RandomlyDmEvanWithDeerPicsBot)")
		}
	}
};
var discord = new DiscordClient(new DiscordConfiguration() {
	Token = botToken,
	Intents = DiscordIntents.GuildMembers
});

await discord.ConnectAsync();

DiscordChannel? dmChannel = null;

while (true) {
	await Task.Delay(TimeSpan.FromHours(random.NextDouble() * 22 + 1));

	try {
		using HttpResponseMessage image = await http.GetAsync($"https://api.tinyfox.dev/img?animal=bleat");
		await using Stream download = await image.Content.ReadAsStreamAsync();
		if (dmChannel == null) {
			DiscordGuild guild = await discord.GetGuildAsync(discord.Guilds.First().Key);
			DiscordMember member = await guild.GetMemberAsync(targetId);
			dmChannel = await member.CreateDmChannelAsync();
		}
		await dmChannel.SendMessageAsync(dmb => dmb.WithFile(Path.GetFileName(image.Content.Headers.ContentDisposition!.FileName!.Replace("\"", "")), download));
	} catch (Exception e) {
		Console.WriteLine(e);
	}
}
