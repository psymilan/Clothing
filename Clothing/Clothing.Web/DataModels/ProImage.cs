
namespace Clothing.Web.DataModels
{
    public class ProImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public byte[] Image { get; set; }
        public int Type { get; set; }

        public Product Product { get; set; }
    }
}