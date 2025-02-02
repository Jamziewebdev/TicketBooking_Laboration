using TicketBookingCore;

namespace TicketBookingCore.Tests
{
    public class TicketBookingRequestProcessorTests
    {
        private readonly TicketBookingRequestProcessor _processor;
        public TicketBookingRequestProcessorTests()
        {
            _processor = new TicketBookingRequestProcessor();
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
            // Arrange
            var processor = new TicketBookingRequestProcessor();
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => processor.Book(null));
            // Assert
            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void ShouldSaveToDatabase()
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

        }
    }
}