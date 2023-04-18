using AutoFixture.Xunit2;
using DurableFunctionsStudyCase.Application.Pets.Requests;
using DurableFunctionsStudyCase.Application.Pets.Responses;
using DurableFunctionsStudyCase.Domain.Pets;
using DurableFunctionsStudyCase.Domain.Schedules;
using DurableFunctionsStudyCase.Functions.Consts;
using DurableFunctionsStudyCase.Functions.Orcherstrators;
using DurableTask.Core.Exceptions;
using FluentAssertions;
using MapsterMapper;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace DurableFunctionsStudyCaseTests.Funtions.Orcherstrators
{
    public class DogCreateFlowOrcherstatorTests : DefaultTestCase
    {
        private readonly DogCreateFlowOrcherstator _sut;
        private readonly IMapper _mapSubstitute;
        private readonly IDurableOrchestrationContext _contextSubstitute;
        private readonly ILogger _logSubstitute;

        public DogCreateFlowOrcherstatorTests()
        {
            _logSubstitute = Substitute.For<ILogger>();
            _contextSubstitute = Substitute.For<IDurableOrchestrationContext>();
            _mapSubstitute = Substitute.For<IMapper>();
            _sut = new DogCreateFlowOrcherstator(_mapSubstitute);
        }

        [Theory]
        [AutoData]
        public void RunOrchestrator_ShouldReturnPetResponse(
            PetCreateRequest input,
            string instanceId,
            PetCreateResponse response)
        {
            //Arrange
            var dog = getFixtureItemWithoutRecursion<Pet>();
            var checkUp = getFixtureItemWithoutRecursion<CheckUp>();
            var vaccination = getFixtureItemWithoutRecursion<Vaccination>();

            _contextSubstitute.GetInput<PetCreateRequest>().Returns(input);
            _contextSubstitute.InstanceId.Returns(instanceId);

            _contextSubstitute.CallActivityWithRetryAsync<Pet>(ActiviesConsts.Dog.Create, Arg.Any<RetryOptions>(), input)
                .Returns(Task.FromResult(dog));
            _contextSubstitute.CallActivityWithRetryAsync<CheckUp>(ActiviesConsts.VeterinaryClinic.ScheduleCheckUp, Arg.Any<RetryOptions>(), dog)
                .Returns(Task.FromResult(checkUp));
            _contextSubstitute.CallActivityWithRetryAsync<Vaccination>(ActiviesConsts.VeterinaryClinic.ScheduleVaccination, Arg.Any<RetryOptions>(), dog)
                .Returns(Task.FromResult(vaccination));

            _mapSubstitute.Map<PetCreateResponse>(dog).Returns(response);


            //Act
            var actual = _sut.RunOrchestrator(_contextSubstitute, _logSubstitute).GetAwaiter().GetResult();

            //Assert   
            actual.Should().BeEquivalentTo(response);

            _logSubstitute.Received(1).Log(
                Microsoft.Extensions.Logging.LogLevel.Information,
                0,
                Arg.Is<object>(x => $"Started orcherstrator {instanceId}. Payload: {JsonConvert.SerializeObject(input)}".Equals(x.ToString())),
                Arg.Any<Exception>(),
                Arg.Any<Func<object, Exception, string>>());

            _logSubstitute.Received(1).Log(
                Microsoft.Extensions.Logging.LogLevel.Information,
                0,
                Arg.Is<object>(x => $"Finished orcherstrator {instanceId}. Result: {JsonConvert.SerializeObject(dog)}".Equals(x.ToString())),
                Arg.Any<Exception>(),
                Arg.Any<Func<object, Exception, string>>());

            _logSubstitute.DidNotReceive().Log(
                Microsoft.Extensions.Logging.LogLevel.Critical,
                Arg.Any<EventId>(),
                Arg.Any<object>(),
                Arg.Any<Exception>(),
                Arg.Any<Func<object, Exception, string>>());
        }

        [Theory]
        [AutoData]
        public void RunOrchestrator_ShouldThrowException(
            PetCreateRequest input,
            string instanceId,
            TaskFailedException exception)
        {
            //Arrange
            var dog = getFixtureItemWithoutRecursion<Pet>();
            var checkUp = getFixtureItemWithoutRecursion<CheckUp>();
            var vaccination = getFixtureItemWithoutRecursion<Vaccination>();

            _contextSubstitute.GetInput<PetCreateRequest>().Returns(input);
            _contextSubstitute.InstanceId.Returns(instanceId);

            _contextSubstitute.CallActivityWithRetryAsync<Pet>(ActiviesConsts.Dog.Create, Arg.Any<RetryOptions>(), input)
                .ThrowsAsync(exception);

            //Act
            Assert.ThrowsAsync<TaskFailedException>(() => _sut.RunOrchestrator(_contextSubstitute, _logSubstitute)).Wait();

            //Assert   
            _logSubstitute.Received(1).Log(
                Microsoft.Extensions.Logging.LogLevel.Information,
                0,
                Arg.Is<object>(x => $"Started orcherstrator {instanceId}. Payload: {JsonConvert.SerializeObject(input)}".Equals(x.ToString())),
                Arg.Any<Exception>(),
                Arg.Any<Func<object, Exception, string>>());

            _logSubstitute.DidNotReceive().Log(
                Microsoft.Extensions.Logging.LogLevel.Information,
                0,
                Arg.Is<object>(x => x.ToString().StartsWith("Finished orcherstrator")),
                Arg.Any<Exception>(),
                Arg.Any<Func<object, Exception, string>>());

            _logSubstitute.Received().Log(
                Microsoft.Extensions.Logging.LogLevel.Critical,
                0,
                Arg.Is<object>(x => $"Finished orcherstrator {instanceId} with errors. Details: {exception.Message}".Equals(x.ToString())),
                Arg.Any<Exception>(),
                Arg.Any<Func<object, Exception, string>>());
        }
    }
}
