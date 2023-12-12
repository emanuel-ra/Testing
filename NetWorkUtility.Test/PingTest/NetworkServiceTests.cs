using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.Ping;
using System.Net.NetworkInformation;

namespace NetWorkUtility.Test.PingTest
{

    public class NetworkServiceTests
    {
        private readonly NetworkService _networkService;
        public NetworkServiceTests()
        {
            _networkService = new NetworkService();
        }
        [Fact]
        public void NetworkService_SendPing_ReturnString()
        {
            // Arrange - Variables, Classes, Mocks

            // Act
            var result = _networkService.SendPing();

            // Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Success: Ping Sent!");
            result.Should().Contain("Success", Exactly.Once());
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 4)]
        public void NetworkService_PingTimeout_ReturnInt(int a, int b, int expected)
        {
            // Arrage

            // Act
            var result = _networkService.PingTimeout(a, b);

            // Assert
            result.Should().Be(expected);
            result.Should().BeGreaterThanOrEqualTo(2);
            result.Should().NotBeInRange(-10000, 0);
        }
        [Fact]
        public void NetworkService_LastPingDate_ReturnDate()
        {
            // Arrage

            // Act
            var result = _networkService.LasPingDate();

            // Assert
            result.Should().BeAfter(1.January(2010));
            result.Should().BeBefore(1.January(2030));

        }
        [Fact]
        public void NetworkService_GetPingOptions_ReturnsObject()
        {
            // Arrage
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };
            // Act
            var result = _networkService.GetPingOptions();

            // Assert: WARNING: "Be" careful
            result.Should().BeOfType<PingOptions>();
            result.Should().BeEquivalentTo(expected);
            result.Ttl.Should().Be(1);
        }
        [Fact]
        public void NetworkService_MostRecentPings_ReturnsObject()
        {
            // Arrage
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };
            // Act
            var result = _networkService.MostRecentPings();

            // Assert: WARNING: "Be" careful
            //result.Should().BeOfType<IEnumerable<PingOptions>>();
            result.Should().ContainEquivalentOf(expected);
            result.Should().Contain(x => x.DontFragment == true);
        }
    }
}
