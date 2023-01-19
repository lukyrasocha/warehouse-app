namespace warehouse_app.Models;

public class Product
{
    public string? name { get; set; }

    public List<ProductArticles>? articles {get; set; } 
}