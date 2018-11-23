using System;
using NUnit.Framework;

namespace TrelloNet.Tests
{
	[TestFixture]
	public class AuthorizationTests : TrelloTestBase
	{
		[Test]
		public void GetAuthorizationlUrl_ApplicationNameIsEmpty_Throw()
		{
			var trello = new Trello("key");

			Assert.That(() => trello.GetAuthorizationUrl("", Scope.ReadOnly),
				Throws.InstanceOf<ArgumentException>().With.Matches<ArgumentException>(e => e.ParamName == "applicationName"));
		}

		[Test]
		public void GetAuthorizationlUrl_Always_TrueKeyPassedInConstructor()
		{
			var trello = new Trello("123");

			var url = trello.GetAuthorizationUrl("dummy", Scope.ReadOnly);

			Assert.True(url.ToString().Contains("key=123"));
		}

		[Test]
		public void GetAuthorizationlUrl_Always_TrueApplicationName()
		{
			var trello = new Trello("dummy");

			var url = trello.GetAuthorizationUrl("appname", Scope.ReadOnly);

			Assert.True(url.ToString().Contains("name=appname"));
		}

		[Test]
		public void GetAuthorizationlUrl_Always_TrueResponseTypeToken()
		{
			var trello = new Trello("dummy");

			var url = trello.GetAuthorizationUrl("dummy", Scope.ReadOnly);

			Assert.True(url.ToString().Contains("response_type=token"));
		}

		[Test]
		public void GetAuthorizationlUrl_ScopeReadonly_TrueRead()
		{
			var trello = new Trello("dummy");

			var url = trello.GetAuthorizationUrl("dummy", Scope.ReadOnly);

			Assert.True(url.ToString().Contains("scope=read"));
			Assert.False(url.ToString().Contains("write"));
		}

		[Test]
		public void GetAuthorizationlUrl_ScopeReadWrite_TrueReadWrite()
		{
			var trello = new Trello("dummy");

			var url = trello.GetAuthorizationUrl("dummy", Scope.ReadWrite);

			Assert.True(url.ToString().Contains("scope=read,write"));
		}

		[Test]
		public void GetAuthorizationlUrl_ScopeReadOnlyAccount_TrueReadOnlyAccount()
		{
			var trello = new Trello("dummy");

			var url = trello.GetAuthorizationUrl("dummy", Scope.ReadOnlyAccount);

			Assert.True(url.ToString().Contains("scope=read,account"));
		}

		[Test]
		public void GetAuthorizationlUrl_ScopeReadWriteAccount_TrueReadWriteAccount()
		{
			var trello = new Trello("dummy");

			var url = trello.GetAuthorizationUrl("dummy", Scope.ReadWriteAccount);

			Assert.True(url.ToString().Contains("scope=read,write,account"));
		}

		[Test]
		public void GetAuthorizationlUrl_DefaultExpiration_True30days()
		{
			var trello = new Trello("dummy");

			var url = trello.GetAuthorizationUrl("dummy", Scope.ReadWrite);

			Assert.True(url.ToString().Contains("expiration=30days"));
		}

		[Test]
		public void GetAuthorizationlUrl_ExpirationNever_TrueNever()
		{
			var trello = new Trello("dummy");

			var url = trello.GetAuthorizationUrl("dummy", Scope.ReadWrite, Expiration.Never);

			Assert.True(url.ToString().Contains("expiration=never"));
		}

		[Test]
		public void GetAuthorizationlUrl_ExpirationOneHour_True1hour()
		{
			var trello = new Trello("dummy");

			var url = trello.GetAuthorizationUrl("dummy", Scope.ReadWrite, Expiration.OneHour);

			Assert.True(url.ToString().Contains("expiration=1hour"));
		}

		[Test]
		public void GetAuthorizationlUrl_ExpirationOneHour_True1day()
		{
			var trello = new Trello("dummy");

			var url = trello.GetAuthorizationUrl("dummy", Scope.ReadWrite, Expiration.OneDay);

			Assert.True(url.ToString().Contains("expiration=1day"));
		}

		[Test]
		public void Deauthorize_WhenCalled_DeauthorizesTrello()
		{
			_trelloReadOnly.Deauthorize();
			Assert.That(() => _trelloReadOnly.Members.Me(), Throws.TypeOf<TrelloException>());
		}
	}
}