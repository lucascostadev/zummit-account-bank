using Conversion.Api.ViewModels.Convert;
using Conversion.Services.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Conversion.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConvertController : ControllerBase
    {
        private readonly IValidator<ConvertRequest> _validator;

        public ConvertController(IValidator<ConvertRequest> validator)
        {
            _validator = validator;
        }

        [HttpPost(Name = "Convert")]
        public async Task<ConvertResponse> Post(ConvertRequest model)
        {
            var validationResult = await _validator.ValidateAsync(model);

            if (validationResult.IsValid)
            {
                return new ConvertResponse();
            }

            return new ConvertResponse(validationResult.Errors);
        }
    }
}
