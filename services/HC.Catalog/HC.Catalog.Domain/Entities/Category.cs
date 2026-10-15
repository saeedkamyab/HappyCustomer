using HC.Catalog.Domain.Enums;
using HC.Catalog.Domain.ValueObjects;

namespace HC.Catalog.Domain.Entities;

public sealed class Category
{
    private readonly List<CategoryAttributeDefinition>
    _attributeDefinitions = new();

    public Guid Id { get; private set; }

    public CategoryName Name { get; private set; } = null!;

    public Guid? ParentCategoryId { get; private set; }

    public CategoryStatus Status { get; private set; }


    public IReadOnlyCollection<CategoryAttributeDefinition>
    AttributeDefinitions =>
        _attributeDefinitions.AsReadOnly();

    private Category()
    {
        // For EF Core
    }

    private Category(
        Guid id,
        CategoryName name,
        Guid? parentCategoryId)
    {
        Id = id;
        Name = name;
        ParentCategoryId = parentCategoryId;
        Status = CategoryStatus.Active;
    }
    public static Category Create(CategoryName name, Guid? parentCategoryId = null)
    {
        ArgumentNullException.ThrowIfNull(name);

        if (parentCategoryId == Guid.Empty)
            parentCategoryId = null;

        return new Category(
           Guid.NewGuid(),
           name,
           parentCategoryId);
    }

    public static Category Rehydrate(Guid id, CategoryName name,
    Guid? parentCategoryId)
    {
        return new Category(
            id,
            name,
            parentCategoryId
        );
    }

    public void Rename(CategoryName newName)
    {
        ArgumentNullException.ThrowIfNull(newName);

        if (Name == newName)
            return;

        Name = newName;
    }
    public void ChangeParentCategory(Guid? newParentCategoryId)
    {
        if (newParentCategoryId == Guid.Empty)
            newParentCategoryId = null;

        if (newParentCategoryId == Id)
        {
            throw new InvalidOperationException(
                "A category cannot be its own parent.");
        }

        if (ParentCategoryId == newParentCategoryId)
            return;

        ParentCategoryId = newParentCategoryId;
    }
    public void Activate()
    {
        if (Status == CategoryStatus.Active)
            return;

        Status = CategoryStatus.Active;
    }

    public void Deactivate()
    {
        if (Status == CategoryStatus.Inactive)
            return;

        Status = CategoryStatus.Inactive;
    }

    public void AddAttributeDefinition(
   CategoryAttributeDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        if (_attributeDefinitions.Any(
            x => x.Name.Value.Equals(
                definition.Name.Value,
                StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException(
           "An attribute with the same name already exists.");
        }
        if (definition.Type == AttributeType.Option && definition.Options.Count == 0)
        {
            throw new InvalidOperationException(
                       "attribute with option type must have at least one option.");
        }
        if (definition.Type != AttributeType.Option && definition.Options.Count > 0)
        {
            throw new InvalidOperationException(
                       "Only attributes of type Option can have options.");
        }
        _attributeDefinitions.Add(definition);
    }

    public void RemoveAttributeDefinition(Guid definitionId)
    {
        var definition = _attributeDefinitions
            .FirstOrDefault(x => x.Id == definitionId);

        if (definition is null)
            return;

        _attributeDefinitions.Remove(definition);
    }


}