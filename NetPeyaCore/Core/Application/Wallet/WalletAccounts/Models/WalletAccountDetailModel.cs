﻿using Core.Domain.Wallet.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Wallet.WalletAccounts.Models
{
    public class WalletAccountDetailModel : WalletAccount
    {
        public Currency Currency { get; set; }
        public WalletAccountCategory WalletAccountCategory { get; set; }
    }
}
