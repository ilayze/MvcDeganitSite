using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MvcDeganitSite.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;

namespace MvcDeganitSite.Models
{
    public class RecipesContext:DbContext
    {
       public DbSet<MainCategory> MainCategories { get; set; }
       public DbSet<Recipe> Recipes { get; set; }
       public DbSet<NavigationWord> NavigationWords { get; set; }
       public DbSet<User> Users { get; set; }

       protected override void OnModelCreating(DbModelBuilder modelBuilder)
       {
           
           modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
           modelBuilder.Entity<MainCategory>()
               .HasMany(r => r.Recipes).WithRequired(m => m.MainCategory); //one to many 
           modelBuilder.Entity<Recipe>()
               .HasMany(n => n.NavigationWords).WithMany(r => r.Recipes) //many to many
               .Map(t => t.MapLeftKey("RecipeID")
                   .MapRightKey("Name")
                   .ToTable("RecipeNavigations"));
           modelBuilder.Entity<Recipe>()
               .HasMany(n => n.Users).WithMany(r => r.Recipes) //many to many
               .Map(t=>t.MapLeftKey("RecipeID")
                .MapRightKey("UserName")
                 .ToTable("UsersRecipes"));

           
         
               
               

               
           
       }
    }

}