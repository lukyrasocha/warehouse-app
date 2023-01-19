using warehouse_app.Controllers;
using warehouse_app.utils;
using warehouse_app.Models;
public static class ArticleHelper{
    public static void updateArticles(Product deletedProduct, string type){
        var helper = new GoogleSheetsHelper();
        var controller = new ArticleController(helper);

        if (deletedProduct.articles is not null){
            foreach (var article in deletedProduct.articles){
                var art_id = article.art_id;
                var amount_of = article.amount_of;
                if (art_id != null && amount_of != null){
                    if (type == "addition"){
                        controller.updateStock(art_id, int.Parse(amount_of));
                    }
                    else if (type == "deletion"){
                        controller.updateStock(art_id, -int.Parse(amount_of));
                    }
                }
            }
        }
    }
}