using Microsoft.EntityFrameworkCore;
using SimpleCrud.Dal.DbContextData;
using SimpleCrud.Dal.Initialization;
using SimpleCrud.Dal.Repositories;
using SimpleCrud.Dal.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Development");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SimpleDbContext>(options => { options.UseSqlServer(connectionString); });
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    app.UseHsts();
else
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<SimpleDbContext>();
        var studentsCount = app.Configuration.GetValue<int>("StudentsCount");
        if (builder.Configuration.GetValue<bool>("RecreateOnInit"))
            DbInitializer.InitializeDataBaseWithRecreation(dbContext, studentsCount);
        else
            DbInitializer.InitializeDataBase(dbContext, studentsCount);
    }

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();