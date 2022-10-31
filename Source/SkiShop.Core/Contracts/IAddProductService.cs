namespace SkiShop.Core.Contracts
{
    using SkiShop.Core.Models.BrandModels;
    using SkiShop.Core.Models.ModelViewModels;
    using SkiShop.Core.Models.ProductViewModels;
    using SkiShop.Core.Models.TypeModels;

    public interface IAddProductService
    {
        Task<IEnumerable<TypeViewModel>> GetAllTypesAsync();
        Task<IEnumerable<ModelViewModel>> GetAllModelsAsync();
        Task<IEnumerable<BrandViewModel>> GetAllBrandsAsync();
        Task AddNewProductAsync(AddProductViewModel model);
    }
}
