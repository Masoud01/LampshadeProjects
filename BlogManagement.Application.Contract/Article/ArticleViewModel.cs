namespace BlogManagement.Application.Contract.Article;

public class ArticleViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? ShortDescription { get; set; }
    public string? PublishDate { get; set; }
    public string? CreateDate { get;  set; }
}