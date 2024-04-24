using InputService.Model;
using InputService.Service.Abstraction;
using Newtonsoft.Json;

namespace InputService.Service
{
    /// <summary>
    /// Represents the input service that handles generation requests.
    /// </summary>
    public class InputService : IInputService
    {
        private List<GenerationRequest> _inputGenerationRequests;
        private MessagePublisherHelper _messagePublisherHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputService"/> class.
        /// </summary>
        public InputService(IConfiguration configuration)
        {
            // Initialize the list of generation requests.
            _inputGenerationRequests = new List<GenerationRequest>();

            string rabbitmqHost = configuration["RABBITMQ_HOST"] ?? throw new ArgumentNullException("RABBITMQ_HOST");
            int rabbitmqPort = int.TryParse(configuration["RABBITMQ_PORT"], out int port) ? port : 5672; // default RabbitMQ port
            string rabbitmqUser = configuration["RABBITMQ_USERNAME"] ?? throw new ArgumentNullException("RABBITMQ_USERNAME");
            string rabbitmqPass = configuration["RABBITMQ_PASSWORD"] ?? throw new ArgumentNullException("RABBITMQ_PASSWORD");
            string rabbitmqVhost = configuration["RABBITMQ_VHOST"] ?? "/"; // default RabbitMQ virtual host

            // Initialize the message publisher helper. Interacts with the generationRequests_Requested queue.
            _messagePublisherHelper = new MessagePublisherHelper(
                queueName: "generationRequests_Requested",
                hostName: rabbitmqHost,
                hostPort: rabbitmqPort,
                hostUsername: rabbitmqUser,
                hostPassword: rabbitmqPass,
                virtualHost: rabbitmqVhost);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputService"/> class.
        /// </summary>
        public InputService(List<GenerationRequest> inputGenerationRequests, MessagePublisherHelper messagePublisherHelper)
        {
            // Initialize the list of generation requests.
            _inputGenerationRequests = inputGenerationRequests;

            // Initialize the message publisher helper. Interacts with the generationRequests_Requested queue.
            _messagePublisherHelper = messagePublisherHelper;
        }

        /// <summary>
        /// Gets all the existing input generation requests.
        /// </summary>
        /// <returns>A list of <see cref="GenerationRequest"/>.</returns>
        public List<GenerationRequest> GetAllGenerationRequests() => _inputGenerationRequests;

        /// <summary>
        /// Adds a new input GenerationRequest and publishes a message for generation.
        /// </summary>
        /// <param name="input">The input to be added.</param>
        /// <returns>The response body containing the details of the added input.</returns>
        public InputHttpResponseBody AddInput(InputHttpRequestBody input)
        {
            // Create a new GenerationRequest object.
            GenerationRequest newGenerationRequest = new GenerationRequest
            {
                RequestId = Guid.NewGuid(),
                UserId = input.UserId,
                GenerationType = (GenerationType)input.GenerationType,
                Status = (Status)0,
                Code = input.Code,
                Parameter = input.Parameter,
                Result = null
            };

            // Add the new GenerationRequest to the list of requests.
            _inputGenerationRequests.Add(newGenerationRequest);

            // Publish a message for the new GenerationRequest.
            _messagePublisherHelper.PublishMessage(JsonConvert.SerializeObject(newGenerationRequest));

            // Return the response body of new GenerationRequest.
            return new InputHttpResponseBody
            {
                RequestId = newGenerationRequest.RequestId,
                UserId = newGenerationRequest.UserId,
                GenerationType = (int)newGenerationRequest.GenerationType,
                Status = (int)newGenerationRequest.Status,
                Code = newGenerationRequest.Code,
                Parameter = newGenerationRequest.Parameter
            };
        }
    }
}
