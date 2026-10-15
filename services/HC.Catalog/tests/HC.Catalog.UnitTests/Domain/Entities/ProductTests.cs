using HC.Catalog.Domain.Entities;
using HC.Catalog.Domain.Enums;
using HC.Catalog.Domain.ValueObjects;

namespace HC.Catalog.UnitTests.Domain.Entities;

public class ProductTests
{
    #region CreateProduct Tests
    [Fact]
    public void CreateProduct_Should_CreateProduct_When_ValidParameters()
    {
        // Arrange
        var name = ProductName.Create("Test Product");

        var description = "This is a test product.";

        var categoryId = Guid.NewGuid();

        var price = Price.Create(10.99m);

        // Act
        var product = Product.Create(name, description, categoryId, price);

        // Assert
        Assert.NotNull(product);
        Assert.Equal(name, product.Name);
        Assert.Equal(description, product.Description);
        Assert.Equal(categoryId, product.CategoryId);
        Assert.Equal(price, product.Price);
        Assert.Equal(ProductStatus.Draft, product.Status);
    }

    [Fact]
    public void CreateProduct_Should_Throw_ArgumentException_When_CategoryIdIsEmpty()
    {
        // Arrange
        var name = ProductName.Create("Test Product");

        var description = "This is a test product.";

        var categoryId = Guid.Empty;

        var price = Price.Create(10.99m);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            Product.Create(name, description, categoryId, price));
    }
    [Fact]
    public void CreateProduct_Should_Throw_ArgumentNullException_When_NameIsNull()
    {
        // Arrange
        ProductName name = null!;

        var description = "This is a test product.";

        var categoryId = Guid.NewGuid();

        var price = Price.Create(10.99m);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            Product.Create(name, description, categoryId, price));
    }
    [Fact]
    public void CreateProduct_Should_Throw_ArgumentNullException_When_PriceIsNull()
    {
        // Arrange
        var name = ProductName.Create("Test Product");

        var description = "This is a test product.";

        var categoryId = Guid.NewGuid();

        Price price = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            Product.Create(name, description, categoryId, price));
    }


    #endregion

    #region ChangePrice Tests
    [Fact]
    public void ChangePrice_Should_ChangePrice_When_ValidNewPrice()
    {
        // Arrange
        var name = ProductName.Create("Test Product");

        var description = "This is a test product.";

        var categoryId = Guid.NewGuid();

        var price = Price.Create(10.99m);

        var product = Product.Create(name, description, categoryId, price);

        var newPrice = Price.Create(15.99m);

        // Act
        product.ChangePrice(newPrice);
        // Assert
        Assert.Equal(newPrice, product.Price);
        Assert.NotEmpty(product.DomainEvents);
    }
    [Fact]
    public void ChangePrice_Should_Throw_ArgumentNullException_When_NewPriceIsNull()
    {
        // Arrange
        var name = ProductName.Create("Test Product");

        var description = "This is a test product.";

        var categoryId = Guid.NewGuid();

        var price = Price.Create(10.99m);

        var product = Product.Create(name, description, categoryId, price);
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => product.ChangePrice(null!));
    }
    [Fact]
    public void ChangePrice_Should_NotChangePrice_When_NewPriceIsSameAsCurrentPrice()
    {
        // Arrange
        var name = ProductName.Create("Test Product");

        var description = "This is a test product.";

        var categoryId = Guid.NewGuid();

        var price = Price.Create(10.99m);

        var product = Product.Create(name, description, categoryId, price);
        // Act
        product.ChangePrice(price);
        // Assert
        Assert.Equal(price, product.Price);
        Assert.Empty(product.DomainEvents);
    }

    #endregion



}
