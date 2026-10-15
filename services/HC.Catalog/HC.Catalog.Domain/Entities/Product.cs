using HC.Catalog.Domain.Enums;
using HC.Catalog.Domain.Events;
using HC.Catalog.Domain.ValueObjects;
using SharedKernel.Domain;

namespace HC.Catalog.Domain.Entities;

public sealed class Product
{
    private readonly List<IDomainEvent> _domainEvents = new();
    private readonly List<ProductSpecification> _specifications = new();

    public Guid Id { get; private set; }
    public ProductName Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public Guid CategoryId { get; private set; }
    public Price Price { get; private set; } = null!;
    public ProductStatus Status { get; private set; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

    public IReadOnlyCollection<ProductSpecification> Specifications =>
        _specifications.AsReadOnly();

    private Product()
    {
        // For EF Core
    }
    private Product(
       Guid id,
       ProductName name,
       string? description,
       Guid categoryId,
       Price price)
    {
        Id = id;
        Name = name;
        Description = description;
        CategoryId = categoryId;
        Price = price;
        Status = ProductStatus.Draft;

    }
    public static Product Create(ProductName name, string? description,
      Guid categoryId, Price price)
    {
        if (categoryId == Guid.Empty)
            throw new ArgumentException(
                "Category is required.",
                nameof(categoryId));

        var product = new Product(
            Guid.NewGuid(),
            name,
            description,
            categoryId,
            price);

        return product;
    }
    public void ChangePrice(Price newPrice)
    {
        ArgumentNullException.ThrowIfNull(newPrice);

        if (Price == newPrice)
            return;

        var oldPrice = Price;

        Price = newPrice;

        _domainEvents.Add(
            new ProductPriceChangedDomainEvent(
                Id,
                oldPrice.Amount,
                newPrice.Amount,
                DateTime.UtcNow));
    }

}

