﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Wallet.WalletAccountCategories.Commands.CreateWalletAccountCategory
{
    public class CreateWalletAccountCategoryCommand
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool RegistrationDefault { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
