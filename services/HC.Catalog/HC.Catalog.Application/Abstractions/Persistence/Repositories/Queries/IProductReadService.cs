//using HC.Catalog.Application.Common;
//using static HC.Catalog.Application.Common.Enums.SortEnum;
//using HC.Catalog.Application.Common.Enums;
//using HC.Catalog.Application.Product.Queries.BackOffice.GetProductById;
//using HC.Catalog.Application.Product.Queries.BackOffice.GetProducts;

//namespace HC.Catalog.Application.Abstractions.Persistence.Repositories.Queries;

//public interface IProductReadService
//{
//    Task<PagedResult<ProductListItem>> GetProductsAsync(
//      string? search,
//      ProductStatusFilter? status,
//      ProductSortBy sortBy,
//      SortDirection sortDirection,
//      int pageNumber,
//      int pageSize,
//      CancellationToken cancellationToken);

//    Task<ProductDetailsResult?> GetByIdAsync(
//      Guid id,
//      CancellationToken cancellationToken);
//}
