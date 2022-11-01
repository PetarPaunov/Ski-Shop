namespace SkiShop.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using SkiShop.Core.Contracts;
    using SkiShop.Core.Contracts.Common;
    using SkiShop.Core.Models.BrandModels;
    using SkiShop.Core.Models.ModelViewModels;
    using SkiShop.Core.Models.ProductViewModels;
    using SkiShop.Core.Models.TypeModels;
    using SkiShop.Data.Common;
    using SkiShop.Data.Models.ProductCommon;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AddProductService : IAddProductService
    {
        private readonly ICommonService commonService;
        private readonly IRepository repository;

        public AddProductService(ICommonService _commonService, IRepository _repository)
        {
            commonService = _commonService;
            repository = _repository;
        }

        public async Task AddNewProductAsync(AddProductViewModel model)
        {
            var imageUrl = commonService.UploadedFile(model.FrontImage);

            var product = new Product()
            {
                Title = model.Title,
                Description = model.Description,
                BrandId = model.BrandId,
                ModelId = model.ModelId,
                Price = model.Price,
                Quantity = model.Quantity,
                ImageUrl = imageUrl,
                TypeId = model.TypeId,
            };

            await repository.AddAsync(product);
            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<BrandViewModel>> GetAllBrandsAsync()
        {
            return await repository.All<Brand>()
                .Select(x => new BrandViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ModelViewModel>> GetAllModelsAsync()
        {
            return await repository.All<Model>()
                .Select(x => new ModelViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TypeViewModel>> GetAllTypesAsync()
        {
            return await repository.All<Type>()
                .Select(x => new TypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }
    }
}