using System.Data.SqlTypes;
using System.Security.Cryptography;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IProductRepository,ProductRepository>(); //<Interface,implement Repository>
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


//----****下面程式碼在啟動應用程式時，將確保資料庫結構與應用程式的資料模型保持一致。****----//

using var scope = app.Services.CreateScope(); //建立新的服務範圍，在每個Http request都會自動建立一個服務範圍
var Services = scope.ServiceProvider;//從新建立的範圍中取得 ServiceProvider，這是一個可以用來解析服務的物件。
var context = Services.GetRequiredService<StoreContext>();//從 ServiceProvider 中取得 StoreContext 服務的實例。StoreContext 是一個Dbcontext，用於與資料庫進行交互。
var logger = Services.GetRequiredService<ILogger<Program>>();//從 ServiceProvider 中取得 ILogger<Program> 服務的實例，用於記錄應用程式的日誌信息
try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
    
}
catch (Exception ex)
{
    
   logger.LogError(ex,"An error occured during migration");
}

//這段程式碼會嘗試執行資料庫遷移。如果資料庫模型有變更，這將會更新資料庫結構以匹配新的模型。如果在遷移過程中出現異常，則會捕獲該異常並防止應用程式崩潰。


//------------------------****End****------------------------//


app.Run();
