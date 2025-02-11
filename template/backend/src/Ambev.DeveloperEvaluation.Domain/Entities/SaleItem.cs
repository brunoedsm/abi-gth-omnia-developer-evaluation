namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Sale Item Domain Entity
    /// </summary>
    public class SaleItem
    {
        /// <summary>
        /// Sale Item Unique Identifier 
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Product Description 
        /// </summary>
        public string? Product { get; set; }
        /// <summary>
        /// Item Quantity
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Item Unit Price
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// Discount Amount When Applicable
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// Cancellation flag
        /// </summary>
        public bool IsCancelled { get; set; }
    }
}
