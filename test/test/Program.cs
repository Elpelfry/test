using test.Client.Pages;
using test.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped(a =>
    new HttpClient
    {
        BaseAddress = new Uri((builder.Configuration.GetSection("RutaApi")!.Value)!)
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();

app.UseCors(builder => builder
    .AllowAnyOrigin()
        .AllowAnyMethod()
            .AllowAnyHeader());

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(test.Client._Imports).Assembly);

app.Run();
