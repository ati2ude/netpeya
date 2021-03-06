﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.APIResponse.Wallet;
using Core.Application.StatusCodes;
using Core.Application.Wallet.Currencies.Commands.DeleteCurrency;
using Core.Application.Wallet.Currencies.Commands.Models;
using Core.Application.Wallet.Currencies.Queries.GetAllCurrenciesQuery;
using Core.Application.Wallet.Currencies.Queries.GetSingleCurrency;
using Core.Domain.Wallet.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.Wallet.CurrenciesController
{
    public class CurrenciesController : WalletBaseController
    {
        private readonly IStringLocalizer<CurrenciesController> _localizer;
        private readonly IStringLocalizer<SharedLocaleController.SharedLocaleController> _baseLocalizer;

        public CurrenciesController(
            IStringLocalizer<SharedLocaleController.SharedLocaleController> baseLocalizer,
            IStringLocalizer<CurrenciesController> localizer)
        {
            _localizer = localizer;
            _baseLocalizer = baseLocalizer;
        }
        
        [HttpGet]
        [Route("api/wallet/currencies")]
        public async Task<IActionResult> GetAll()
        {
            if (ModelState.IsValid)
            {
                List<Currency> taskReturn = await Mediator.Send(new GetMultipleCurrenciesQuery());

                if (taskReturn != null && taskReturn.Count > 0)
                {
                    return Ok(new CurrenciesResponse(nameof(Currency), taskReturn, taskReturn.FirstOrDefault().statusCode, _baseLocalizer, _localizer));
                }
                else
                {
                    Currency currency = new Currency { ID = 0, statusCode = SharedStatusCodes.NotFound };
                    return Ok(new CurrenciesResponse(nameof(Currency), currency, currency.statusCode, _baseLocalizer, _localizer));
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("{id}")]
        [Route("api/wallet/currencies/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (ModelState.IsValid)
            {
                Currency taskReturn = await Mediator.Send(new GetSingleCurrencyQuery { CurrencyID = id });
                return Ok(new CurrenciesResponse(nameof(Currency), taskReturn, taskReturn.statusCode, _baseLocalizer, _localizer));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
       
        [HttpPost]
        [Route("api/wallet/currencies/add")]
        public async Task<IActionResult> Create([FromForm] CreateCurrencyCommand command)
        {
            if (ModelState.IsValid)
            {
                Currency taskReturn = await Mediator.Send(command);
                return Ok(new CurrenciesResponse(nameof(Currency), taskReturn, taskReturn.statusCode, _baseLocalizer, _localizer));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        
        [HttpPut("{id}")]
        [Route("api/wallet/currencies/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateCurrencyCommand command)
        {
            if (ModelState.IsValid)
            {
                command.ID = id;
                Currency taskReturn = await Mediator.Send(command);
                return Ok(new CurrenciesResponse(nameof(Currency), taskReturn, taskReturn.statusCode, _baseLocalizer, _localizer));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        
        [HttpDelete("{id}")]
        [Route("api/wallet/currencies/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                Currency taskReturn = await Mediator.Send(new DeleteCurrencyCommand { ID = id });
                return Ok(new CurrenciesResponse(nameof(Currency), taskReturn, taskReturn.statusCode, _baseLocalizer, _localizer));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
