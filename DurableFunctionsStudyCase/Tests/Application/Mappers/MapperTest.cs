using DurableFunctionsStudyCase.Application.Pets.Responses;
using DurableFunctionsStudyCase.Domain.Pets;
using FluentAssertions;
using MapsterMapper;

namespace DurableFunctionsStudyCaseTests.Application.Mappers
{
    public class MapperTest : DefaultTestCase
    {
        private readonly IMapper _sut;

        public MapperTest()
        {
            _sut = new Mapper();
        }

        [Fact]
        public void Map_ShouldMapPetToPetCreateResponse()
        {
            //Arrange
            Pet pet = getFixtureItemWithoutRecursion<Pet>();

            //Act
            var actual = _sut.Map<PetCreateResponse>(pet);

            //Assert
            actual.Should().BeEquivalentTo(pet, opts =>
            {
                opts.For(x => x.CheckUps).Exclude(x => x.Pet);
                opts.For(x => x.Vaccinations).Exclude(x => x.Pet);
                return opts;
            });
        }
    }
}
