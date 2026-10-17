using HC.Catalog.Application.Abstractions.Persistence.Repositories.Queries;
using HC.Catalog.Application.Common;
using HC.Catalog.Application.Common.Enums;
using MediatR;
using static HC.Catalog.Application.Common.Enums.SortEnum;

namespace HC.Catalog.Application.Category.Queries.BackOffice.GetCategories;

public sealed class GetCategoriesQueryHandler
       : IRequestHandler<
        GetCategoriesQuery,
        PagedResult<CategoryListItem>>
{
    private readonly ICategoryReadService _categoryReadService;

    public GetCategoriesQueryHandler(
        ICategoryReadService categoryReadService)
    {
        _categoryReadService = categoryReadService;
    }

    public async Task<PagedResult<CategoryListItem>> Handle(
        GetCategoriesQuery request,
        CancellationToken cancellationToken)
    {

        var result = await _categoryReadService.GetCategoriesAsync(
      request.Search,
      StringToEnumConvertor.ToNullableEnum<CategoryStatusFilter>(request.Status),
      StringToEnumConvertor.ToEnum<CategorySortBy>(request.SortBy),
      StringToEnumConvertor.ToEnum<SortDirection>(request.SortDirection),
      request.PageNumber,
      request.PageSize,
      cancellationToken);


        var items = result.Items.Select(x => new CategoryListItem(
                                        x.Id, x.Name,x.Status,
                                        x.ParentCategoryId,
                                        x.ParentCategoryName)).ToArray();

        return new PagedResult<CategoryListItem>(
            items,
            result.TotalCount,
            result.PageNumber,
            result.PageSize);


    }
}
