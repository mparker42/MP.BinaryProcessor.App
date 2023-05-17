using MP.BinaryProcessor.App.Handlers;
using MP.BinaryProcessor.App.Interfaces.Handlers;
using MP.BinaryProcessor.App.Interfaces.Services;
using MP.BinaryProcessor.App.Schemas;
using MP.BinaryProcessor.App.Services;

namespace MP.BinaryProcessor.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services
                .AddScoped<IDecodeMessageHandler, DecodeMessageHandler>()
                .AddScoped<IByteOperatorService, ByteOperatorService>()
                .AddSingleton(MessageSchema.MessageSchemaItems);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}