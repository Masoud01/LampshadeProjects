namespace ShopManagement.Application.Contracts.Slider
{
    public class SlideVIewModel
    {
        public int Id { get; set; }
        public string? Picture { get; set; }
        public string? Title { get; set; }
        public string? Heading { get; set; }
        public bool IsRemoved { get; set; }
        public string? CreateDate { get; set; }
    }
}
