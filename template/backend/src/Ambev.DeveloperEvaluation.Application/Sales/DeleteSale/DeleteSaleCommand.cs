﻿using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleCommand : IRequest<DeleteSaleCommandResult>
    {
        public Guid Id { get; }

        public DeleteSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
