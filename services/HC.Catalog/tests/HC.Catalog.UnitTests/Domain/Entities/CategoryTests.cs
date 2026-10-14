using HC.Catalog.Domain.Entities;
using HC.Catalog.Domain.Enums;
using HC.Catalog.Domain.ValueObjects;

namespace HC.Catalog.UnitTests.Domain.Entities;

public class CategoryTests
{

    #region Create Category Tests

    [Fact]
    public void Create_Should_Create_Active_Category_With_Valid_Paramerters()
    {
        var name = CategoryName.Create("Electronics");


        var category=Category.Create(name);

        Assert.Null(category.ParentCategoryId);
        Assert.NotEqual(Guid.Empty, category.Id);
        Assert.Equal(name,category.Name);
        Assert.Equal(CategoryStatus.Active, category.Status);
    }

    [Fact]
    public void Create_Should_Create_Child_Category()
    {
        var parentCategoryId = Guid.NewGuid();

        var category = Category.Create(CategoryName.Create("Electronics"), parentCategoryId);
        
        Assert.Equal(parentCategoryId, category.ParentCategoryId);
    }

    [Fact]
    public void Create_Should_Throw_Exception_When_Name_Is_Null()
    {
        Assert.Throws<ArgumentNullException>(() => Category.Create(null!));
    }

    #endregion

    #region Rename Category Tests

    [Fact]
    public void Rename_Should_Update_Category_Name()
    {
        var category = Category.Create(CategoryName.Create("Electronics"));
        var newName = CategoryName.Create("New Electronics");

        category.Rename(newName);

        Assert.Equal(newName, category.Name);
    }


    [Fact]
    public void Rename_Should_Throw_Exception_When_NewName_Is_Null()
    {
        var category = Category.Create(CategoryName.Create("Electronics"));

        Assert.Throws<ArgumentNullException>(() => category.Rename(null!));
    }

    #endregion

    #region Activate/Deactivate Category Tests
    [Fact]
    public void Activate_Should_Set_Status_To_Active()
    {
        var category = Category.Create(CategoryName.Create("Electronics"));
        category.Deactivate();
        category.Activate();
        Assert.Equal(CategoryStatus.Active, category.Status);
    }

    [Fact]
    public void Deactivate_Should_Set_Status_To_Inactive()
    {
        var category = Category.Create(CategoryName.Create("Electronics"));
        category.Deactivate();
        Assert.Equal(CategoryStatus.Inactive, category.Status);
    }

    #endregion

    #region Add Attribute Definition Tests

    [Fact]
    public void AddAttributeDefinition_Should_Add_Definition()
    {
        var category = Category.Create(CategoryName.Create("Electronics"));

        var definition = CategoryAttributeDefinition.Create("RAM",AttributeType.Number,true);


        category.AddAttributeDefinition(definition);

        var result = Assert.Single(
                 category.AttributeDefinitions);

        Assert.Equal(definition, result);
    }

    [Fact]
    public void AddAttributeDefinition_Should_Throw_Exception_When_Definition_Is_Null()
    {
        var category = Category.Create(CategoryName.Create("Electronics"));

        Assert.Throws<ArgumentNullException>(() => category.AddAttributeDefinition(null!));
    }


    [Fact]
    public void AddAttributeDefinition_Should_Throw_Exception_When_Definition_Already_Exists()
    {
        var category = Category.Create(CategoryName.Create("Electronics"));

        var definition = CategoryAttributeDefinition.Create("RAM", AttributeType.Number, true);

        category.AddAttributeDefinition(definition);

        Assert.Throws<InvalidOperationException>(() => category.AddAttributeDefinition(definition));
    }
    #endregion

    #region Remove Attribute Definition Tests

    [Fact]
    public void RemoveAttributeDefinition_Should_Remove_Definition()
    {
        var category = Category.Create(CategoryName.Create("Electronics"));

        var definition = CategoryAttributeDefinition.Create("RAM", AttributeType.Number, true);

        category.AddAttributeDefinition(definition);

        category.RemoveAttributeDefinition(definition.Id);

        Assert.Empty(category.AttributeDefinitions);
    }

    #endregion

}
