using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace TrelloNet.Tests
{
    public class TrelloTestBase
	{
		protected ITrello _trelloReadOnly;
		protected ITrello _trelloReadWrite;
        protected IConfigurationRoot _config;

		[SetUp]
		public void Setup()
		{
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("app.config.json", optional: true, reloadOnChange: true)
                .Build();

            var appKey = _config.GetSection("Values").GetChildren().First(x => x.Key == "ApplicationKey").Value;
            var token = _config.GetSection("Values").GetChildren().First(x => x.Key == "MemberReadToken").Value;
			_trelloReadOnly = new Trello(appKey);
			_trelloReadOnly.Authorize(token);

			_trelloReadWrite = new Trello(appKey);
			_trelloReadWrite.Authorize(token);	
		}
	}
}