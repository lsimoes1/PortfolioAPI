using Bogus;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Portfolio.API.Business;
using Portfolio.API.Model.Response;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Portfolio.API.xUnitTest
{
    public class GitHubTest
    {

        IConfigurationRoot configuration;
        private readonly List<ResponseGit> listGit;

        public GitHubTest()
        {
            listGit = this.CreateFakeObj();
            configuration = this.CreateConfiguration();
        }

        [Fact]
        public void Teste_GitHub_Not_Null()
        {

            var handlerMock = this.CreateMockHandler(JsonConvert.SerializeObject(listGit), HttpStatusCode.OK);

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object);

            var testGit = new GitHub(configuration);

            // ACT
            var result = testGit.GetAllRepository(httpClient);

            // ASSERT
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void Test_GitHub_NotFound_FromGit()
        {
            var handlerMock = this.CreateMockHandler("NotFound", HttpStatusCode.NotFound);

            var httpClient = new HttpClient(handlerMock.Object);

            var testGit = new GitHub(configuration);

            var result = testGit.GetAllRepository(httpClient);

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public void Test_GitHub_BadRequest_FromGit()
        {
            var handlerMock = this.CreateMockHandler("BadRequest", HttpStatusCode.BadRequest);

            var httpClient = new HttpClient(handlerMock.Object);

            var testGit = new GitHub(configuration);

            var result = testGit.GetAllRepository(httpClient);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public void Test_GitHub_InternalServerError_Configuration_Null()
        {
            var handlerMock = this.CreateMockHandler("BadRequest", HttpStatusCode.BadRequest);

            var httpClient = new HttpClient(handlerMock.Object);

            var testGit = new GitHub(null);

            ResponseHttp result;

            result = testGit.GetAllRepository(httpClient);

            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.Equal("Object reference not set to an instance of an object.", result.Body);
        }

        private Mock<HttpMessageHandler> CreateMockHandler(string listgit, HttpStatusCode statusCode)
        {
            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = statusCode,
                   Content = new StringContent(listgit),
               }).Verifiable();

            return handlerMock;
        }

        private List<ResponseGit> CreateFakeObj()
        {
            List<ResponseGit> listGit = new List<ResponseGit>();
            ResponseGit objGit = new Faker<ResponseGit>()
                .RuleFor(p => p.Description, p => p.Random.Word())
            .RuleFor(p => p.NameProject, p => p.Company.CompanyName())
            .RuleFor(p => p.HomePage, p => p.Internet.Url())
            .RuleFor(p => p.Language, p => p.Random.Word())
            .RuleFor(p => p.LastUpdate, p => p.Date.Past())
            .RuleFor(p => p.UrlRepository, p => p.Internet.Url());

            listGit.Add(objGit);

            return listGit;
        }

        private IConfigurationRoot CreateConfiguration()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"GitRepository", "https://unitteste.local/"},
            };

            configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            return configuration;
        }
    }
}
