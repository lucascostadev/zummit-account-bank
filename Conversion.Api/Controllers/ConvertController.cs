using Conversion.Api.ViewModels.Convert;
using Conversion.Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Conversion.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConvertController : ControllerBase
    {
        private readonly IValidator<ConvertRequest> _validator;
        private readonly IEuroService _euroService;

        public ConvertController(IValidator<ConvertRequest> validator, IEuroService euroService)
        {
            _validator = validator;
            _euroService = euroService;
        }

        [HttpPost(Name = "Convert")]
        public async Task<ConvertResponse> Post(ConvertRequest model)
        {
            var validationResult = await _validator.ValidateAsync(model);

            if (validationResult.IsValid)
            {
                return new ConvertResponse(model.From, await _euroService.Convert(model.To, model.From, model.Value));
            }

            return new ConvertResponse(validationResult.Errors);
        }
    }
}
