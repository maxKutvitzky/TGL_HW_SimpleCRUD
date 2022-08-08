using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleCrud.Dal.DbContextData;
using SimpleCrud.Dal.Initialization;
using SimpleCrud.Dal.Repositories.Interfaces;
using SimpleCrud.Dal.Repositories;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("Development");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SimpleDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    using (var scope = app.Services.CreateScope())
    {
        SimpleDbContext dbContext = scope.ServiceProvider.GetRequiredService<SimpleDbContext>();
        int studentsCount = app.Configuration.GetValue<int>("StudentsCount");
        if (builder.Configuration.GetValue<bool>("RecreateOnInit"))
        {
            DbInitializer.InitializeDataBaseWithRecreation(dbContext, studentsCount);
        }
        else
        {
            DbInitializer.InitializeDataBase(dbContext, studentsCount);
        }
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
