using StudentTrackerServer.DbServices;
using StudentTrackerServer.Services;
using System.Text.Json.Serialization;

namespace StudentTrackerServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<GroupsDbCollectionService>();
            builder.Services.AddScoped<HeadersDbCollectionService>();
            builder.Services.AddScoped<MarksDbCollectionService>();
            builder.Services.AddScoped<StudentsDbCollectionService>();
            builder.Services.AddScoped<SubjectsDbCollectionService>();
            builder.Services.AddScoped<TeachersDbCollectionService>();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<STDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
