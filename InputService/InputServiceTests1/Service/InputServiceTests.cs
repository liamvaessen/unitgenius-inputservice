using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using InputService.Service;
using InputService.Model;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace InputService.Service.Tests
{
    [TestClass()]
    public class InputServiceTests
    {
        private List<GenerationRequest> _existingGenerationRequests;
        private Mock<MessagePublisherHelper> _mockMessagePublisherHelper;
        private InputService _inputService;

        [TestInitialize]
        public void Setup()
        {
            // Arrange
            _existingGenerationRequests = new List<GenerationRequest>
            {
                new GenerationRequest
                {
                    RequestId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    GenerationType = GenerationType.UnitTest,
                    Status = Status.Completed,
                    Code = "Test Code 1",
                    Parameter = "Test Parameter 1",
                    Result = "Test Result 1"
                },
                new GenerationRequest
                {
                    RequestId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    GenerationType = GenerationType.Documentation,
                    Status = Status.Requested,
                    Code = "Test Code 2",
                    Parameter = "Test Parameter 2",
                    Result = "Test Result 2"
                }
            };
            _mockMessagePublisherHelper = new Mock<MessagePublisherHelper>(["testQueue", "testAddress", "testUsername", "testPassword", 5672, "/"]);
            _mockMessagePublisherHelper.Setup(m => m.PublishMessage(It.IsAny<string>()));
            _inputService = new InputService(_existingGenerationRequests, _mockMessagePublisherHelper.Object);
        }

        [TestMethod()]
        public void GetAllGenerationRequestsTest()
        {
            // Act
            var result = _inputService.GetAllGenerationRequests();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<GenerationRequest>));
            Assert.AreEqual(_existingGenerationRequests.Count, result.Count);
            Assert.AreEqual(_existingGenerationRequests[0].RequestId, result[0].RequestId);
            Assert.AreEqual(_existingGenerationRequests[0].UserId, result[0].UserId);
            Assert.AreEqual(_existingGenerationRequests[0].GenerationType, result[0].GenerationType);
            Assert.AreEqual(_existingGenerationRequests[0].Status, result[0].Status);
            Assert.AreEqual(_existingGenerationRequests[0].Code, result[0].Code);
            Assert.AreEqual(_existingGenerationRequests[0].Parameter, result[0].Parameter);
            Assert.AreEqual(_existingGenerationRequests[0].Result, result[0].Result);
            Assert.AreEqual(_existingGenerationRequests[1].RequestId, result[1].RequestId);
            Assert.AreEqual(_existingGenerationRequests[1].UserId, result[1].UserId);
            Assert.AreEqual(_existingGenerationRequests[1].GenerationType, result[1].GenerationType);
            Assert.AreEqual(_existingGenerationRequests[1].Status, result[1].Status);
            Assert.AreEqual(_existingGenerationRequests[1].Code, result[1].Code);
            Assert.AreEqual(_existingGenerationRequests[1].Parameter, result[1].Parameter);
            Assert.AreEqual(_existingGenerationRequests[1].Result, result[1].Result);
        }

        [TestMethod()]
        public void AddInputTest()
        {
            // Arrange
            var input = new InputHttpRequestBody
            {
                UserId = Guid.NewGuid(),
                GenerationType = 0,
                Code = "Test Code 3",
                Parameter = "Test Parameter 3"
            };

            // Act
            var result = _inputService.AddInput(input);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(InputHttpResponseBody));
            Assert.AreEqual(input.UserId, result.UserId);
            Assert.AreEqual(input.GenerationType, result.GenerationType);
            Assert.AreEqual(0, result.Status);
            Assert.AreEqual(input.Code, result.Code);
            Assert.AreEqual(input.Parameter, result.Parameter);


            // Verify that PublishMessage was called with the correct parameters
            var expectedGenerationRequest = new GenerationRequest
            {
                RequestId = result.RequestId, // Use the RequestId from the result
                UserId = input.UserId,
                GenerationType = (GenerationType)input.GenerationType,
                Status = Status.Requested, // The status is likely set to Requested in the AddInput method
                Code = input.Code,
                Parameter = input.Parameter,
                Result = null // The result is likely null initially
            };
            string expectedPublishedGenerationRequest = JsonConvert.SerializeObject(expectedGenerationRequest);
            _mockMessagePublisherHelper.Verify(m => m.PublishMessage(It.Is<string>(s => s == expectedPublishedGenerationRequest)), Times.Once);
        }
    }
}
