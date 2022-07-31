using BankSystem.Api.Middlewares;
using BankSystem.Application;
using BankSystem.Application.Validators;
using BankSystem.Common.Filters;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationModule(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMvc(options =>
{
    options.Filters.Add<ValidationFilter>();
})
.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateClientDtoValidator>());

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();

app.UseStaticFiles();

//app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();

