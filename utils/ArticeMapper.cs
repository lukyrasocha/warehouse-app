
using warehouse_app.Models;

namespace warehouse_app.utils
{
    public static class ArticleMapper 
    {
        public static List<Article> MapFromRangeData(IList<IList<object>> values)
        {
            var articles = new List<Article>();

            foreach (var value in values)
            {
                Article article = new()
                {
                    art_id = value[0].ToString(),
                    name = value[1].ToString(),
                    stock = value[2].ToString()
                };

                articles.Add(article);
            }

            return articles;
        }

        public static IList<IList<object>> MapToRangeData(Article article)
        {
            if (article.art_id != null && article.name != null && article.stock != null){
                var objectList = new List<object>() { article.art_id, article.name, article.stock };
                var rangeData = new List<IList<object>> { objectList };
                return rangeData;
            } else {
                
                return new List<IList<Object>>();
            }
        }
    }
}