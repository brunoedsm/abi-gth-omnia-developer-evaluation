namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Sale Domain Entity
    /// </summary>
    public class Sale
    {
        /// <summary>
        /// Sale Unique Identifier
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Sale Date
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Customer Name
        /// </summary>
        public string? Customer { get; set; }
        /// <summary>
        /// Branch Text
        /// </summary>
        public string? Branch { get; set; }
        /// <summary>
        /// Sales Items
        /// </summary>
        public List<SaleItem> Items { get; set; } = new();
        /// <summary>
        /// Sale Amount Value
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// Cancellation Flag
        /// </summary>
        public bool IsCancelled { get; set; }
    }
}
