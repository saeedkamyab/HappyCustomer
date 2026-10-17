//using HC.Catalog.Application.Category.Queries.BackOffice.GetCategoryById;
using HC.Catalog.Application.Common;
using HC.Catalog.Application.Common.Enums;
using static HC.Catalog.Application.Common.Enums.SortEnum;

namespace HC.Catalog.Application.Abstractions.Persistence.Repositories.Queries;

public interface ICategoryReadService
{
    Task<PagedResult<ReadModels.CategoryReadModel>> GetCategoriesAsync(
       string? search,
       CategoryStatusFilter? status,
       CategorySortBy sortBy,
       SortDirection sortDirection,
       int pageNumber,
       int pageSize,
       CancellationToken cancellationToken);

    //Task<CategoryDetailsResult?> GetByIdAsync(
    //  Guid id,
    //  CancellationToken cancellationToken);

}
