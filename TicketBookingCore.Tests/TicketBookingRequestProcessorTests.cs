using TicketBookingCore;
using Moq;

namespace TicketBookingCore.Tests
{
    public class TicketBookingRequestProcessorTests
    {
        private readonly TicketBookingRequest _request;
        private readonly Mock<ITicketBookingRepository> _ticketBookingRepositoryMock;
        private readonly TicketBookingRequestProcessor _processor;
        public TicketBookingRequestProcessorTests()
        {
            _request = new TicketBookingRequest
            {
                FirstName = "Jamie",
                LastName = "Ziegler",
                Email = "fake@mail.com"
            };
            _ticketBookingRepositoryMock = new Mock<ITicketBookingRepository>();
            _processor = new TicketBookingRequestProcessor(_ticketBookingRepositoryMock.Object);
        }

        [Fact]
        public void ShouldReturnTicketBookingResultWithRequestValues()
        {
            // Arrange
            var request = new TicketBookingRequest
            {
                FirstName = "Jamie",
                LastName = "Ziegler",
                Email = "fake@mail.com"
            };

            // Act
            TicketBookingResponse response = _processor.Book(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(request.FirstName, response.FirstName);
            Assert.Equal(request.LastName, response.LastName);
            Assert.Equal(request.Email, response.Email);
        }

        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.Book(null));
            // Assert
            Assert.Equal("request", exception.ParamName);
        }


        [Fact]
        public void ShouldSaveToDatabase()
        {
            // Arrange
            TicketBooking savedTicketBooking = null;

            // Setup save method to capture saved ticket
            _ticketBookingRepositoryMock.Setup(x => x.Save(It.IsAny<TicketBooking>()))
            .Callback<TicketBooking>((ticketBooking) =>
            {
                savedTicketBooking = ticketBooking;
            });

            // Act
            _processor.Book(_request);

            // Assert
            _ticketBookingRepositoryMock.Verify(x => x.Save(It.IsAny<TicketBooking>()), Times.Once);

            // Assert
            Assert.NotNull(savedTicketBooking);
            Assert.Equal(_request.FirstName, savedTicketBooking.FirstName);
            Assert.Equal(_request.LastName, savedTicketBooking.LastName);
            Assert.Equal(_request.Email, savedTicketBooking.Email);


        }
    }
}