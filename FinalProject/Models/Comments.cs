namespace FinalProject.Models
{
    public partial class Comments
    {
        public int Id { get; set; }
        public int? FavoriteId { get; set; }
        public string Comment { get; set; }
        public virtual Favorite Favorite { get; set; }
    }
}
