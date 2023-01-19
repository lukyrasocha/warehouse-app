using warehouse_app.Models;

namespace warehouse_app.utils
{
    public static class ProductMapper 
    {
        public static List<Product> MapFromRangeData(IList<IList<object>> values)
        {
            var products = new List<Product>();
            Product product;

            for (int i = 1; i < values.Count; i++) //skip first row of the sheet
            {

                if (values[i].Count > 1){ //Check whether the sheet has empty rows
                string? articles = values[i][1].ToString();
                var articlesIds_Amounts = parseArticles(articles);

                product = new()
                {
                    name = values[i][0].ToString(),
                    articles = articlesIds_Amounts 
                };
                } 
                else { //If we have an empty row
                    product = new()
                    {
                        name = "",
                        articles = new List<ProductArticles>()
                    };
                }
                products.Add(product);
            }

            return products;
        }

        public static IList<IList<object>> MapToRangeData(Product product)
        {
            if (product.name != null && product.articles != null){
                string finalArticle ="";

                foreach (ProductArticles article in product.articles){
                    string intermediate = $"art_id:{article.art_id},amount_of:{article.amount_of};";
                    finalArticle += intermediate;
                }
                var objectList = new List<object>() { product.name, finalArticle };
                var rangeData = new List<IList<object>> { objectList };
                return rangeData;
            } else {
                
                return new List<IList<Object>>();
            }
        }

        private static List<ProductArticles> parseArticles(string? articles){
            var articlesIds_Amounts = new List<ProductArticles>();
            if (articles != null){
                string[] individualArticles = articles.Split(';');
                foreach(string article in individualArticles){
                    string[] items = article.Split(',');
                    if (items.Count() == 2){
                        var id_amount = new ProductArticles {
                            art_id = items[0].Split(':')[1],
                            amount_of = items[1].Split(':')[1]
                        };
                        articlesIds_Amounts.Add(id_amount);
                    }

                }
            }
            return articlesIds_Amounts;
        } 
    }
}