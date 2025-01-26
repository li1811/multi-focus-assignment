/*This program converts temperatures. Me and olga worked together on this, she was responsible for the front end, i did the backend. There might be slight differences in our programs but it uses the same logic. Different endpoints for each type of conversion, and a single fetch request with string interpolation.
I think Olga's looks a lot nicer, she did a lot of front end work that I didn't im*/

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("allowAll", policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();


    });
});
var app = builder.Build();
app.UseCors("allowAll");

app.MapGet("/", () => "Hello World!");


// Start of celsius conversions
app.MapPost("/celsiustocelsius", ([FromForm] float input) => 
{
    if (input.GetType() == typeof(float))
    {
        return Results.Ok(input+"°C");
    }
    else 
        return Results.BadRequest(new {message="Input must be a number"});

}).DisableAntiforgery();

app.MapPost("/celsiustofahrenheit", ([FromForm] float input) => 
{
    if (input.GetType() == typeof(float))
    {
        var fahrenheit = input * 1.8 + 32;
        return Results.Ok(Math.Round(fahrenheit, 2)+"°F");
    }
    else 
        return Results.BadRequest(new {message="Input must be a number"});

}).DisableAntiforgery();

app.MapPost("/celsiustokelvin", ([FromForm] float input) => 
{
    if (input.GetType() == typeof(float))
    {
        var kelvin = input + 273.15;
        return Results.Ok(kelvin+"°K");
    }
    else 
        return Results.BadRequest(new {message="Input must be a number"});

}).DisableAntiforgery();
// End of celsius conversions

// Start of fahrenheit conversions
app.MapPost("/fahrenheittofahrenheit", ([FromForm] float input) => 
{
    if (input.GetType() == typeof(float))
    {
        return Results.Ok(input+"°F");
    }
    else 
        return Results.BadRequest(new {message="Input must be a number"});

}).DisableAntiforgery();

app.MapPost("/fahrenheittocelsius", ([FromForm] float input) => 
{
    if (input.GetType() == typeof(float))
    {
        var celsius = (input - 32) / 1.8;
        return Results.Ok(Math.Round(celsius, 2)+"°C");
    }
    else 
        return Results.BadRequest(new {message="Input must be a number"});

}).DisableAntiforgery();


app.MapPost("/fahrenheittokelvin", ([FromForm] float input) => 
{
    if (input.GetType() == typeof(float))
    {
        var kelvin = (input - 32) / 1.8 + 273.15;
        return Results.Ok(Math.Round(kelvin, 2)+"°K");
    }
    else 
        return Results.BadRequest(new {message="Input must be a number"});

}).DisableAntiforgery();
// End of fahrenheit conversions

// Start of kelvin conversions
app.MapPost("/kelvintokelvin", ([FromForm] float input) => 
{
    if (input.GetType() == typeof(float))
    {
        return Results.Ok(input +"°K");
    }
    else 
        return Results.BadRequest(new {message="Input must be a number"});

}).DisableAntiforgery();

app.MapPost("/kelvintocelsius", ([FromForm] float input) => 
{
    if (input.GetType() == typeof(float))
    {
        var celsius = input - 273.15;
        return Results.Ok(celsius+"°C");
    }
    else 
        return Results.BadRequest(new {message="Input must be a number"});

}).DisableAntiforgery();

app.MapPost("/kelvintofahrenheit", ([FromForm] float input) => 
{
    if (input.GetType() == typeof(float))
    {
        var fahrenheit = (input - 273.15) * 1.8 + 32;
        return Results.Ok(Math.Round(fahrenheit, 2)+"°F");
    }
    else 
        return Results.BadRequest(new {message="Input must be a number"});

}).DisableAntiforgery();
// End of kelvin conversions

app.Run();
