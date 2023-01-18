using warehouse_app.Models;

namespace warehouse_app.utils
{
    public static class ProductMapper 
    {
        public static List<Product> MapFromRangeData(IList<IList<object>> values)
        {
            var products = new List<Product>();

            for (int i = 1; i < values.Count; i++) //skip first row of the sheet
            {

                string? articles = values[i][1].ToString();
                var articlesIds_Amounts = parseArticles(articles);

                Product product = new()
                {
                    name = values[i][0].ToString(),
                    articles = articlesIds_Amounts 
                };

                products.Add(product);
            }

            return products;
        }

        public static IList<IList<object>> MapToRangeData(Product product)
        {
            if (product.name != null && product.articles != null){
                var objectList = new List<object>() { product.name, product.articles };
                var rangeData = new List<IList<object>> { objectList };
                return rangeData;
            } else {
                
                return new List<IList<Object>>();
            }
        }

        private static List<object> parseArticles(string? articles){
            var articlesIds_Amounts = new List<object>();
            if (articles != null){
                string[] individualArticles = articles.Split(';');
                foreach(string article in individualArticles){
                    string[] items = article.Split(',');
                    var id_amount = new {
                        art_id = items[0],
                        amount_of = items[1]
                    };
                    articlesIds_Amounts.Add(id_amount);

                }
            }
            return articlesIds_Amounts;
        } 
    }
}