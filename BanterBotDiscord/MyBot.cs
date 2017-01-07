using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanterBotDiscord
{
    class MyBot
    {
        DiscordClient Discord;
        CommandService Commands;
        Random Rand;

        string[,] Users;
        string CheckEm = "Images/CheckEm.jpg";
        string[] RandomImage; 

        public MyBot()
        {
            Rand = new Random();
            RandomImage = new string[]
            {
                "Images/Image1.jpg",
                "Images/Image2.jpg",
                "Images/Image3.jpg"
            };
            //Users = new string[,];

            Discord = new DiscordClient(X =>
            {
                X.LogLevel = LogSeverity.Info;
                X.LogHandler = Log;
            });
            Discord.UsingCommands(X =>
            {
                X.PrefixChar = '!';
                X.AllowMentionPrefix = true;

            });
            

            Commands = Discord.GetService<CommandService>();

            Commands.CreateCommand("CanYouHearMe?").Do(async (e) => { e.Channel.SendMessage("Hell Yes");});
            Commands.CreateCommand("Double Number?").Do(async (e) => {
                int RandomDubs = Rand.Next(10,100);
                string RandomDubsString = RandomDubs.ToString();
                e.Channel.SendMessage(RandomDubsString);
                e.Channel.SendFile(CheckEm);
            });
            RegisterCommand();

            Discord.ExecuteAndWait(async () =>
            {
                await Discord.Connect("", TokenType.Bot);
            });
        }

        public void RegisterCommand()
        {
            Commands.CreateCommand("TestCommand").Do(async (e) =>
            {
                int RandomNumber = Rand.Next(RandomImage.Length);
                string ImageToPost = RandomImage[RandomNumber];
                await e.Channel.SendFile(ImageToPost);
            });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
