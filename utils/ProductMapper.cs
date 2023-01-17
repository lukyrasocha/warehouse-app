using warehouse_app.Models;

namespace warehouse_app.utils
{
    public static class ProductMapper 
    {
        public static List<Product> MapFromRangeData(IList<IList<object>> values)
        {
            var products = new List<Product>();

            foreach (var value in values)
            {
                Product product = new()
                {
                    name = value[0].ToString(),
                    articles = value[1].ToString()
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
    }
}