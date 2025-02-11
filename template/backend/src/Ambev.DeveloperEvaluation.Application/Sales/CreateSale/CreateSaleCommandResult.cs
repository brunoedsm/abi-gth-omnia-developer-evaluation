﻿using Ambev.DeveloperEvaluation.Application.Base;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Command result for Sale Creation
    /// </summary>
    public class CreateSaleCommandResult : BaseResult
    {
        public Guid Id { get; set; }
    }
}
