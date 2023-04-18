using DurableFunctionsStudyCase.Application.Dogs.Validators;
using DurableFunctionsStudyCase.Application.Pets.Requests;
using FluentAssertions;

namespace DurableFunctionsStudyCaseTests.Application.Pets.Validators
{
    public class PetFormValidatorTests
    {
        private readonly PetFormValidator _sut;

        public PetFormValidatorTests()
        {
            _sut = new PetFormValidator();
        }

        [Fact]
        public void Validate_ShouldBeValid()
        {
            //Arrange
            var request = new PetCreateRequest()
            {
                Name = "Pet0001"
            };

            //Act
            var actual = _sut.Validate(request);

            //Assert
            actual.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Validate_ShouldNotBeValid(string value)
        {
            //Arrange
            var request = new PetCreateRequest()
            {
                Name = value
            };

            //Act
            var actual = _sut.Validate(request);

            //Assert
            actual.IsValid.Should().BeFalse();
        }

    }
}
