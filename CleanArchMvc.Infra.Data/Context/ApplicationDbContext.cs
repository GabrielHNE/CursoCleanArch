﻿using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CleanArchMvc.Infra.Data.Identity;

namespace CleanArchMvc.Infra.Data.Context;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {}

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /*
            Busca os arquivos que implementaram o 
            IEntityTypeConfiguration e os insere manualmente
        */
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        /*Sem o ApplyConfiguration, poderiamos inserir cada configuracao manualmente*/
        // builder.ApplyConfiguration(new CategoryConfiguration());
    }
}
