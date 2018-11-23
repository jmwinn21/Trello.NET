using System.Configuration;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace TrelloNet.Tests
{
	[TestFixture]
	public class UnauthorizedTests : TrelloTestBase
    {
        [Test]
		public void InvalidToken_ShouldThrowUnauthorizedException()
		{
            //var trello = new Trello(ConfigurationManager.AppSettings["ApplicationKey"]);
            var appKey = _config.GetSection("Values").GetChildren().First(x => x.Key == "ApplicationKey").Value;
            var trello = new Trello(appKey);
            trello.Authorize("invalid token");
			Assert.That(trello.Members.Me, Throws.TypeOf<TrelloUnauthorizedException>());
		}

		[Test]
		public void NoToken_ShouldThrowUnauthorizedException()
		{
            //var trello = new Trello(ConfigurationManager.AppSettings["ApplicationKey"]);
            var appKey = _config.GetSection("Values").GetChildren().First(x => x.Key == "ApplicationKey").Value;
            var trello = new Trello(appKey);
            Assert.That(() => trello.Boards.WithId(Constants.WelcomeBoardId), Throws.TypeOf<TrelloUnauthorizedException>());
		}
	}
}