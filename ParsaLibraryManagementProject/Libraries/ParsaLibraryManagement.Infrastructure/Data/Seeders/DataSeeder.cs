using Microsoft.Extensions.DependencyInjection;
using ParsaLibraryManagement.Domain.Entities;
using ParsaLibraryManagement.Infrastructure.Data.Contexts;
using Microsoft.AspNetCore;
using System;
using Microsoft.AspNetCore.Identity;

namespace ParsaLibraryManagement.Infrastructure.Data.Seeders
{
    public static class DataSeeder
    {


        /// <summary>
        /// This is a workaround for missing seed data functionality in EF 7.0       
        /// </summary>
        /// <param name="app">
        /// An instance that provides the mechanisms to get instance of the database context.
        /// </param>
        public static void SeedData(this IServiceScopeFactory scopeFactory)
        {
            using (var serviceScope = scopeFactory.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ParsaLibraryManagementDBContext>();
                if (context is not null)
                {
                    SeedGenders(context);
                    SeedRoles(context);
                    SeedPermissions(context);
                }
            }
        }

        public static void SeedGenders(ParsaLibraryManagementDBContext context)
        {

            if (!context.Genders.Any())
            {
                var lstGender = new List<Gender>
                {
                new Gender { Code="M",Title = "Male", },
                new Gender { Code="F",Title = "Female", },
                new Gender { Code="O",Title = "Other", },
            };
                context.AddRange(lstGender);
                context.SaveChanges();
            }
        }

        public static void SeedRoles(ParsaLibraryManagementDBContext context)
        {

            if (!context.Roles.Any())
            {
                var lstRole = new List<Role>
                {
                new Role { Name="SuperAdmin"},
                new Role { Name="Admin"},
                new Role { Name="Manager"},
                new Role { Name="Contributor"},
                new Role { Name="User"},


            };
                context.AddRange(lstRole);
                context.SaveChanges();
            }
        }

        public static void SeedPermissions(ParsaLibraryManagementDBContext context)
        {
            if (!context.Permissions.Any())
            {
                var lstPermission = new List<Permission>
                {
                new Permission { Name="can-create-user"},
                new Permission { Name="can-view-user-listing"},
                new Permission { Name="can-view-user"},
                new Permission { Name="can-delete-user"},
                new Permission { Name="can-update-user"},

                new Permission { Name="can-create-role"},
                new Permission { Name="can-view-role-listing"},
                new Permission { Name="can-view-role"},
                new Permission { Name="can-delete-role"},
                new Permission { Name="can-update-role"},

                new Permission { Name="can-create-permission"},
                new Permission { Name="can-view-permission-listing"},
                new Permission { Name="can-view-permission"},
                new Permission { Name="can-delete-permission"},
                new Permission { Name="can-update-permission"},

                new Permission { Name="can-create-book"},
                new Permission { Name="can-view-book-listing"},
                new Permission { Name="can-view-book"},
                new Permission { Name="can-delete-book"},
                new Permission { Name="can-update-book"},

                new Permission { Name="can-create-publisher"},
                new Permission { Name="can-view-publisher-listing"},
                new Permission { Name="can-view-publisher"},
                new Permission { Name="can-delete-publisher"},
                new Permission { Name="can-update-publisher"},

            };
                context.AddRange(lstPermission);
                context.SaveChanges();
            }
        }

        public static void SeedUsers(ParsaLibraryManagementDBContext context)
        {
            var passwordHasher = new PasswordHasher<User>();
            if (!context.Genders.Any())
            {
                var genderId=context.Genders.FirstOrDefault().GenderId;
                var lstUser = new List<User>
                {
                new User { 
                    Email="admin@parsalms.com" ,
                    FirstName="Super", 
                    LastName="Admin",
                    GenderId=genderId,
                    PasswordHash= passwordHasher.HashPassword(null, "admin"),                
            },
                
            };
                context.AddRange(lstUser);
                context.SaveChanges();
            }
        }
    }
}
