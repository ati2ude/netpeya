﻿using Core.Domain.Wallet.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Wallet.SavedCards.Models
{
    public class BaseSavedCardCommand : SavedCard, IRequest<SavedCard>
    {
    }
}
