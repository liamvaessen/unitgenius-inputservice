using InputService.Model;
using InputService.Service;
using InputService.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InputService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InputController : ControllerBase
    {
        private IInputService InputService { get; set; }
        public InputController(IInputService inputService)
        {
            this.InputService = inputService;
        }
        
        [HttpGet]
        public ActionResult<List<GenerationRequest>> GetAll() =>
            InputService.GetAllGenerationRequests();

        [Route("/add")]
        [HttpPost]
        [Authorize]
        public ActionResult<InputHttpResponseBody> AddInput(InputHttpRequestBody input)
        {
            InputHttpResponseBody response = InputService.AddInput(input);
            return CreatedAtAction(nameof(AddInput), response);
        }
    }
}
