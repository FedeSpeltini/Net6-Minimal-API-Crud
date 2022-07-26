using AutoMapper;
using BookShop.Data;
using BookShop.Dtos;
using BookShop.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var sqlConBuilder = new SqlConnectionStringBuilder();

sqlConBuilder.ConnectionString = builder.Configuration.GetConnectionString("SQLDbConnection");
sqlConBuilder.UserID = builder.Configuration["UserId"];
sqlConBuilder.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlConBuilder.ConnectionString));
builder.Services.AddScoped<IBookRepo, BookRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", async (IBookRepo repo, IMapper mapper ) =>
{
    var books = await repo.GetAllBooks();
    return Results.Ok(mapper.Map<IEnumerable<BookReadDto>>(books));
});


app.MapGet("/books/{id}", async (IBookRepo repo, IMapper mapper, int id ) =>
{
    var book = await repo.GetBookById(id);
    if (book != null)
    {
        return Results.Ok(mapper.Map<BookReadDto>(book));
    }
    return Results.NotFound();
});

app.MapPost("/book", async (IBookRepo repo, IMapper mapper, BookWriteDto bookWriteDto) => {
    var bookModel = mapper.Map<Book>(bookWriteDto);

    await repo.CreateBook(bookModel);
    await repo.SaveChanges();

    var cmdReadDto = mapper.Map<BookReadDto>(bookModel);

    return Results.Created($"/book/{cmdReadDto.Id}", cmdReadDto);

});


app.MapDelete("api/commands/{id}", async (IBookRepo repo, IMapper mapper, int id) => {
    var book = await repo.GetBookById(id);
    if (book == null)
    {
        return Results.NotFound();
    }

    repo.DeleteBook(book);

    await repo.SaveChanges();

    return Results.NoContent();

});

app.MapPut("api/books/{id}", async (IBookRepo repo, IMapper mapper, int id, BookUpdateDto bookUpdateDto) => {
    var book = await repo.GetBookById(id);
    if (book == null)
    {
        return Results.NotFound();
    }

    mapper.Map(bookUpdateDto, book);

    await repo.SaveChanges();

    return Results.NoContent();
});

app.Run();

