using JuliaHayward.Common.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace JuliaHayward.Common.Logging
{
    public class TrelloLogger : ILogger
    {
        private readonly string _trelloKey;
        private readonly string _trelloAuthKey;

        public TrelloLogger(string trelloKey, string trelloAuthKey)
        {
            _trelloKey = trelloKey;
            _trelloAuthKey = trelloAuthKey;
        }

        public void Error(string appName, Exception exception)
        {
            var nestedStackTrace = exception.StackTrace;
            var nestedMessage = exception.Message;
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                nestedStackTrace = exception.StackTrace + System.Environment.NewLine + "-----"
                    + System.Environment.NewLine + nestedStackTrace;
                nestedMessage = exception.Message;
            }

            Error(appName, nestedMessage, nestedStackTrace);
        }

        public void Error(string appName, string message, string detail)
        {
            if (JuliaEnvironment.CurrentEnvironment == EnvironmentType.Dev) return;

            // https://github.com/dillenmeister/Trello.NET/wiki

            var trello = new TrelloNet.Trello(_trelloKey);
            // Trello AuthKey is returned by logging in and browsing to the url returned from
            // var url = trello.GetAuthorizationUrl("Dawn Chorus", Scope.ReadWrite);
            trello.Authorize(_trelloAuthKey);

            // Get the authenticated member
            var me = trello.Members.Me();
            Console.WriteLine(me.FullName);

            // This is designed for my personal Trello board - if you want to put cards in
            // different places, change as required.
            var boards = trello.Boards.ForMember(me);
            var board = boards.First(x => x.Name.Contains("My software"));
            var cards = trello.Cards.ForBoard(board);
            var card = cards.FirstOrDefault(x => x.Name == message && x.Desc == detail);
            if (card == null)
            {
                var lists = trello.Lists.ForBoard(board);
                var list = lists.First(x => x.Name == "New");
                card = trello.Cards.Add(new TrelloNet.NewCard(message, list));
                card.Desc = detail;
                trello.Cards.Update(card);
            }
            else
            {
                card.Labels.Add(new TrelloNet.Label() { Color = TrelloNet.Color.Red });
                trello.Cards.Update(card);
            }
        }
    }
}
