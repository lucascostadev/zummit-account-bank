using AutoMapper;
using Balance.Api.ViewModels.Convert;
using Balance.Domain.Entities;
using Balance.Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Balance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountBankController : ControllerBase
    {
        private readonly IValidator<AccountBankRequest> _validator;
        private readonly IAccountBankService _accountBankService;
        private readonly IMapper _mapper;

        public AccountBankController(IValidator<AccountBankRequest> validator, IAccountBankService accountBankService, IMapper mapper)
        {
            _validator = validator;
            _accountBankService = accountBankService;
            _mapper = mapper;
        }

        [HttpPost(Name = "Create")]

        public async Task<AccountBankResponse> Post(AccountBankRequest model)
        {
            var validationResult = await _validator.ValidateAsync(model);

            if (validationResult.IsValid)
            {
                return _mapper.Map<AccountBankResponse>(await _accountBankService.Add(_mapper.Map<AccountBank>(model)));
            }

            return new AccountBankResponse(validationResult.Errors);
        }

        [HttpPut(Name = "Update")]

        public async Task<AccountBankResponse> Put(AccountBankRequest model)
        {
            var validationResult = await _validator.ValidateAsync(model);

            if (validationResult.IsValid)
            {
                return _mapper.Map<AccountBankResponse>(await _accountBankService.Update(_mapper.Map<AccountBank>(model)));
            }

            return new AccountBankResponse(validationResult.Errors);
        }

        [HttpGet(Name = "List")]
        public async Task<IEnumerable<AccountBankResponse>> List()
        {
            return _mapper.Map<IEnumerable<AccountBankResponse>>(await _accountBankService.Get());
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<AccountBankResponse> Get(int id)
        {
            return _mapper.Map<AccountBankResponse>(await _accountBankService.GetById(id));
        }
    }
}
