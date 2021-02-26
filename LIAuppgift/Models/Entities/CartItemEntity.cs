namespace LIAuppgift.Controllers
{
    using System.ComponentModel.DataAnnotations;

    public class CartItemEntity
    {
        [Key]
        public int Id { get; set; }

        public int Price { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int SumPrice { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }
    }
}