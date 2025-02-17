using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    /// <summary>
    /// Sales Presentation Layer (API Controller)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : BaseController
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Create New Sale
        /// </summary>
        /// <param name="sale">Sale</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreatedAtRouteResult), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateSaleCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return CreatedAtRoute("GetSale", routeValues: new { id = response.Id }, null);
        }

        /// <summary>
        /// Get Existent Sale By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetSale")]
        [ProducesResponseType(typeof(GetSaleResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var saleResult = await _mediator.Send(new GetSaleCommand(id));
            return saleResult.Id == Guid.Empty ? (ActionResult)NotFound() : (ActionResult)Ok(_mapper.Map<GetSaleResponse>(saleResult));
        }

        /// <summary>
        /// Get All Sales
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetSaleResponse>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllSales([FromQuery] int? skip, [FromQuery] int? take)
        {
            var salesResult = await _mediator.Send(new GetSalesCommand(skip, take));

            return salesResult?.Sales?.Count() > 0 ? Ok(_mapper.Map<IEnumerable<GetSaleResponse>>(salesResult.Sales)) : (ActionResult)NotFound();
        }

        /// <summary>
        /// Update Sale
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateSale([FromRoute] Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateSaleCommand>(request);
            command.Id = id;

            var response = await _mediator.Send(command, cancellationToken);

            return response.Success ? NoContent() : BadRequest(response.Errors);
        }

        /// <summary>
        /// Delete Sale (Logical/cancellation)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteSaleCommand(id));
            return response.Success ? Ok() : BadRequest(response.Errors);
        }

        /// <summary>
        /// Delete Sale Item (Logical/cancellation)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{saleId}/items/{itemId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteSaleItem([FromRoute] Guid saleId, [FromRoute] Guid itemId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteSaleItemCommand(saleId, itemId));
            return response.Success ? Ok() : BadRequest(response.Errors);
        }
    }
}
