using Microsoft.AspNetCore.Diagnostics;
using MusicApp.API.ErrorHandling;
using MusicApp.Core.Exception;
using System.Net;

namespace MusicApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;

                if (exception is MusicApp.Core.Exception.BusinessException businessException)
                {
                    var errorResponse = new MusicApp.API.ErrorHandling.ErrorHandling();

                    foreach (var item in businessException.ListErros)
                    {
                        errorResponse.Messages.Add(new ErrorMessage() { ErrorName = item.NomeErro, Message = item.MensagemErro });

                    }

                    context.Response.StatusCode = (int) HttpStatusCode.UnprocessableEntity;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(errorResponse);
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new { Error = exception?.Message });
                }

                
            }
            ));


            app.MapControllers();

            app.Run();
        }
    }
}