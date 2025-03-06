using Refit;
using ZeroStoreApp.CrossCutting.Common;
using ZeroStoreApp.Web.Models;

namespace ZeroStoreApp.Web.Services;

public interface IApiService
{
    [Get("/Product")]
    Task<PaginatedResponse<ProductListModel>> GetProducts(ProductRequest request);

    [Get("/Product/{id}")]
    Task<ProductModel> GetProduct(Guid id);

    [Post("/Product")]
    Task<ProductModel> CreateProduct(CreateProductModel model);

    [Put("/Product")]
    Task<ProductModel> UpdateProduct(UpdateProductModel model);

    [Delete("/Product/{id}")]
    Task<ProductModel> DeleteProduct(Guid id);
}
