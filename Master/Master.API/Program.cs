using Master.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSwagger()
    .AddFluentValidation()
    .AddDataContext(builder.Configuration)
    .AddIdentityAndDb(builder.Configuration)
    .AddJwtAuthenticationAndAuthorization(builder.Configuration)
    .AddAutoMapperAndOtherServices()
    .AddHangfireServices(builder.Configuration);

var app = builder.Build();

await app.EnsureSeededAsync();

app.UseMasterPipeline();

app.Run();
