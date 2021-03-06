﻿using Core.Domain.Wallet.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Wallet.PaymentMethods.Queries.GetSinglePaymentMethod
{
    public class GetSinglePaymentMethodQuery : PaymentMethod, IRequest<PaymentMethod>
    {
    }
}
