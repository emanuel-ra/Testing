using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUsersServices
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttGetRequest()
        {
            // Arrage
            var expectedResponse = UsersFixture.GetTestUsers();
            var handleMock = MockHttpMessageHandler<User>.SetupBassicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handleMock.Object);

            var endpoint = "http://example.com";
            var config = Options.Create(new UsersApiOptions
            {
                EndPoint = endpoint
            });

            var sut = new UsersService(httpClient, config);
            // Act
            await sut.GetAllUsers();

            // Assert
            handleMock
                .Protected()
                .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                );
        }
        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
        {
            // Arrage
            var handleMock = MockHttpMessageHandler<User>.SetupReturn404();
            var httpClient = new HttpClient(handleMock.Object);
            var endpoint = "http://example.com";
            var config = Options.Create(new UsersApiOptions
            {
                EndPoint = endpoint
            });
            var sut = new UsersService(httpClient, config);
            // Act
            var result = await sut.GetAllUsers();

            // Assert
            result.Count.Should().Be(0);
        }
        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExectedSize()
        {
            // Arrage
            // Arrage
            var expectedResponse = UsersFixture.GetTestUsers();
            var handleMock = MockHttpMessageHandler<User>.SetupBassicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handleMock.Object);
            var endpoint = "http://example.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                EndPoint = endpoint
            });
            var sut = new UsersService(httpClient, config);
            // Act
            var result = await sut.GetAllUsers();

            // Assert
            result.Count.Should().Be(expectedResponse.Count);
        }
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfigureExternalUrl()
        {
            // Arrage
            // Arrage
            var expectedResponse = UsersFixture.GetTestUsers();

            var endpoint = "https://example.com/users";

            var handleMock = MockHttpMessageHandler<User>
                .SetupBassicGetResourceList(expectedResponse, endpoint);

            var httpClient = new HttpClient(handleMock.Object);

            var config = Options.Create(new UsersApiOptions
            {
                EndPoint = endpoint
            });

            var sut = new UsersService(httpClient, config);

            // Act
            var result = await sut.GetAllUsers();

            var uri = new Uri(endpoint);

            // Assert
            handleMock
                .Protected()
                .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(
                    req => req.Method == HttpMethod.Get
                    && req.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>()
                );
        }
    }
}
