using MediatR;

namespace HC.Catalog.Application.Category.Queries.BackOffice.GetCategoryById;

public sealed record GetCategoryByIdQuery(
    Guid CategoryId
) : IRequest<CategoryDetailsResult?>;