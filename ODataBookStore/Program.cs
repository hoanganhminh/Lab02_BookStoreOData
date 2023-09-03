using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ODataBookStore;
using ODataBookStore.Models;

static IEdmModel GetEdmModel() //step 4
{
    ODataConventionModelBuilder oDataConventionModelBuilder = new ODataConventionModelBuilder();
    oDataConventionModelBuilder.EntitySet<Book>("Books");
    oDataConventionModelBuilder.EntitySet<Press>("Presses");
    return oDataConventionModelBuilder.GetEdmModel();
}

//Them cac service 
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookStoreContext>(opt => opt.UseInMemoryDatabase("BookLists")); //add dbcontext step 6
builder.Services.AddControllers().AddOData(option  //step 5
    => option.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel()));
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
app.UseODataBatching(); //step 5
app.UseAuthorization();
app.MapControllers();
app.Run();
