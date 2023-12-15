using AutoMapper;
using Balance.Api.ViewModels.Convert;
using Balance.Domain.Entities;
using Balance.Domain.Interfaces;
using Balance.Domain.ViewModels.AccountBank;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Balance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountBankController : ControllerBase
    {
        private readonly IValidator<AccountBankRequest> _validator;
        private readonly IValidator<DepositRequest> _depositValidator;
        private readonly IValidator<WithdrawRequest> _withdrawValidator;
        private readonly IAccountBankService _accountBankService;
        private readonly IMapper _mapper;

        public AccountBankController(IValidator<AccountBankRequest> validator, IValidator<DepositRequest> depositValidator, IValidator<WithdrawRequest> withdrawValidator, IAccountBankService accountBankService, IMapper mapper)
        {
            _validator = validator;
            _depositValidator = depositValidator;
            _withdrawValidator = withdrawValidator;
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

        [HttpGet("{accountBankId}", Name = "Get")]
        public async Task<AccountBankResponse> Get(int accountBankId)
        {
            return _mapper.Map<AccountBankResponse>(await _accountBankService.GetById(accountBankId));
        }

        [HttpPut("{accountBankId}/deposit", Name = "Deposit")]
        public async Task<AccountBankResponse> Deposit(int accountBankId, DepositRequest model)
        {
            var validationResult = await _depositValidator.ValidateAsync(model);

            if (validationResult.IsValid)
            {
                return _mapper.Map<AccountBankResponse>(await _accountBankService.Deposit(accountBankId, model));
            }

            return new AccountBankResponse(validationResult.Errors);
        }

        [HttpPut("{accountBankId}/withdraw", Name = "Withdraw")]
        public async Task<AccountBankResponse> Withdraw(int accountBankId, WithdrawRequest model)
        {
            var validationResult = await _withdrawValidator.ValidateAsync(model);

            if (validationResult.IsValid)
            {
                return _mapper.Map<AccountBankResponse>(await _accountBankService.Withdraw(accountBankId, model));
            }

            return new AccountBankResponse(validationResult.Errors);
        }
    }
}
