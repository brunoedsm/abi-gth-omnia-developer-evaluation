using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        public UpdateSaleRequestValidator()
        {
            RuleFor(sale => sale.Branch).NotNull().WithMessage("Branch cannot be null or empty.");
            RuleFor(sale => sale.Customer).NotNull().WithMessage("Customer cannot be null or empty.");
            RuleFor(sale => sale.Date).NotNull().WithMessage("Date cannot be null or empty.");
            RuleFor(sale => sale.Items).NotEmpty().WithMessage("Sales must have minimum 1 item.");
            RuleFor(sale => sale.TotalAmount).NotNull().WithMessage("Total Amount cannot be null or empty.");
            RuleFor(sale => sale.IsCancelled).NotNull().WithMessage("Cancellation flag cannot be null or empty.");
        }
    }
}
