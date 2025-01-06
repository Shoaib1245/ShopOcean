namespace Service.UI.Models
{
    public class AlldataModel
    {
        public List<CategoryModel>? CategoryData { get; set; }
        public List<SubCategoryModel>? SubCategoryData { get; set; }
        public List<ProductModel>? ProductData { get; set; }
        public List<ReviewAndComments>? ReviewAndComments { get; set; }
        public ProductModel? ProductDetail { get; set; }
    }
}
