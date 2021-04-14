namespace LIAuppgift.Models.Entites
{
    using System.ComponentModel.DataAnnotations;

    public class CartItemEntity
    {

        [Key]
        public int Id { get; set; }

        public int ProductPrice { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public double ConvertedPrice { get; set; }

        public string ProductName { get; set; }

        public string UserId { get; set; }
    }
}