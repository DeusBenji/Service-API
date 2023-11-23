using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.BusinessLogicLayer;
using Service_Api;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.DatabaseLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICustomerGroup, CustomerGroupDatabaseAccess>();
builder.Services.AddScoped<ICustomerGroupData, CustomerGroupDataControl>();
builder.Services.AddScoped<IDiscount, DiscountDatabaseAccess>();
builder.Services.AddScoped<IDiscountData, DiscountDataControl>();
builder.Services.AddScoped<IProduct, ProductDatabaseAccess>();
builder.Services.AddScoped<IProductData, ProductDataControl>();
builder.Services.AddScoped<IShop, ShopDatabaseAccess>();
builder.Services.AddScoped<IShopData, ShopDataControl>();
builder.Services.AddScoped<IIngredient, IngredientDatabaseAccess>();
builder.Services.AddScoped<IIngredientData, IngredientDataControl>();
builder.Services.AddScoped<IProductGroup, ProductGroupDatabaseAccess>();
builder.Services.AddScoped<IProductGroupData, ProductGroupDataControl>();
builder.Services.AddScoped<ICombo, ComboDatabaseAccess>();
builder.Services.AddScoped<IComboData, ComboDataControl>();
builder.Services.AddScoped<IShopProduct, ShopProductDatabaseAccess>();
builder.Services.AddScoped<IShopProductData, ShopProductDataControl>();
builder.Services.AddScoped<IIngredientProduct, IngredientProductDatabaseAccess>();
builder.Services.AddScoped<IIngredientProductData, IngredientProductDataControl>();

builder.Services.AddScoped<IIngredientOrderline, IngredientOrderlineDatabaseAccess>();
builder.Services.AddScoped<IIngredientOrderlineData, IngredientOrderlineDataControl>();

// Configure CORS to allow any origin
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin() // Allow any origin
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

// Enable CORS
app.UseCors();

app.UseAuthorization();
app.MapControllers();

app.Run();