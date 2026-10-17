using HC.Catalog.Application.Abstractions.Persistence;
using HC.Catalog.Domain.Entities;
using HC.Catalog.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace HC.Catalog.Infrastructure.Persistence;

public class CatalogDbContext:DbContext, IUnitOfWork
{
    public CatalogDbContext(
        DbContextOptions<CatalogDbContext> options)
        : base(options)
    {
    }

    //public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    internal DbSet<CategoryAttributeDefinitionRecord>
   CategoryAttributeDefinitions =>
       Set<CategoryAttributeDefinitionRecord>();

    internal DbSet<AttributeOptionRecord>
        AttributeOptions =>
            Set<AttributeOptionRecord>();

    public DbSet<Product> Products => Set<Product>();

    internal DbSet<ProductSpecificationRecord> ProductSpecifications =>
   Set<ProductSpecificationRecord>();



    protected override void OnModelCreating(
       ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(CatalogDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }


}
