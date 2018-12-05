﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Application.Wallet.WalletAccounts.Commands.CreateWalletAccount;
using Core.Application.Wallet.WalletAccounts.Queries;
using Core.Application.Wallet.WalletAccounts.Queries.GetUserWalletAccounts;
using Microsoft.Extensions.Localization;
using Core.Domain.Wallet.Entities;
using API.APIResponse.Wallet;
using System.Collections.Generic;
using System.Linq;
using Core.Application.Wallet.WalletAccounts.Models;
using Core.Application.Wallet.WalletAccounts.Commands.DeleteWalletAccount;
using Core.Application.Wallet.WalletAccounts.Commands.UpdateWalletAccount;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.Wallet
{
    public class WalletAccountsController : WalletBaseController
    {
        private readonly IStringLocalizer<WalletAccountsController> _localizer;
        private readonly IStringLocalizer<SharedLocaleController.SharedLocaleController> _baseLocalizer;

        public WalletAccountsController(
            IStringLocalizer<SharedLocaleController.SharedLocaleController> baseLocalizer,
            IStringLocalizer<WalletAccountsController> localizer)
        {
            _localizer = localizer;
            _baseLocalizer = baseLocalizer;
        }

        // POST api/wallet/walletaccounts/create
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateWalletAccountCommand command)
        {
            if (ModelState.IsValid)
            {
                WalletAccount taskReturn = await Mediator.Send(command);
                return Ok(new WalletAccountsResponse(nameof(WalletAccount), taskReturn, taskReturn.statusCode, _baseLocalizer, _localizer));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // GET api/wallet/walletaccounts/getdetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            if (ModelState.IsValid)
            {
                WalletAccount taskReturn = await Mediator.Send(new WalletAccountDetailQuery { ID = id });
                return Ok(new WalletAccountsResponse(nameof(WalletAccount), taskReturn, taskReturn.statusCode, _baseLocalizer, _localizer));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        
        // GET api/wallet/walletaccounts/getuserwalletaccounts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserWalletAccounts(int id)
        {
            if (ModelState.IsValid)
            {
                List<WalletAccountDetailModel> taskReturn = await Mediator.Send(new GetUserWalletAccountsQuery { UserID = id });
                return Ok(new WalletAccountsResponse(nameof(WalletAccount), taskReturn, taskReturn.FirstOrDefault().statusCode, _baseLocalizer, _localizer));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/wallet/walletaccounts/update/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateWalletAccountCommand command)
        {
            if (ModelState.IsValid)
            {
                command.ID = id;
                WalletAccount taskReturn = await Mediator.Send(command);
                return Ok(new WalletAccountsResponse(nameof(WalletAccount), taskReturn, taskReturn.statusCode, _baseLocalizer, _localizer));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/wallet/walletaccounts/delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                WalletAccount taskReturn = await Mediator.Send(new DeleteWalletAccountCommand { ID = id });
                return Ok(new WalletAccountsResponse(nameof(WalletAccount), taskReturn, taskReturn.statusCode, _baseLocalizer, _localizer));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
