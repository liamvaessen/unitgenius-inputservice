using InputService.Model;

namespace InputService.Service.Abstraction
{
    /// <summary>
    /// Represents the interface for the input service.
    /// </summary>
    public interface IInputService
    {
        /// <summary>
        /// Retrieves all existing input generation requests.
        /// </summary>
        /// <returns>A list of GenerationRequest objects.</returns>
        List<GenerationRequest> GetAllGenerationRequests();

        /// <summary>
        /// Adds a GenerationRequest to the service and message bus.
        /// </summary>
        /// <param name="input">The input to be added.</param>
        /// <returns>The response body of the input request.</returns>
        InputHttpResponseBody AddInput(InputHttpRequestBody input);
    }
}
