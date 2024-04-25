using DeShawn.Models;
using DeShawn.Models.DTOs;

var builder = WebApplication.CreateBuilder(args);

List<Walkers> walkers = new List<Walkers>
{
    new Walkers
    {
        Id = 1,
        Name = "John",
        DogId = 1
    },
    new Walkers
    {
        Id = 2,
        Name = "Emily",
        DogId = 2
    },
    new Walkers
    {
        Id = 3,
        Name = "Michael",
        DogId = 3
    },
    new Walkers
    {
        Id = 4,
        Name = "Sophia",
        DogId = 4
    },
    new Walkers
    {
        Id = 5,
        Name = "William",
        DogId = 5
    }
};

List<Cities> cities = new List<Cities>
{
    new Cities
    {
        Id = 1,
        Name = "New York",
    },
    new Cities
    {
        Id = 2,
        Name = "Los Angeles",
    },
    new Cities
    {
        Id = 3,
        Name = "Chicago",
    },
    new Cities
    {
        Id = 4,
        Name = "Houston",
    },
    new Cities
    {
        Id = 5,
        Name = "San Francisco",
    }
};


List<Dogs> dogs = new List<Dogs>
{
    new Dogs
    {
        Id = 1,
        Name = "Barkley",
        CityId = 1,
        WalkerId = 1
    },
    new Dogs
    {
        Id = 2,
        Name = "Peanut",
        CityId = 2,
        WalkerId = 2
    },
    new Dogs
    {
        Id = 3,
        Name = "Mya",
        CityId = 3,
        WalkerId = 3
    },
    new Dogs
    {
        Id = 4,
        Name = "Rocky",
        CityId = 4,
        WalkerId = 4
    },
    new Dogs
    {
        Id = 5,
        Name = "Cooper",
        CityId = 5,
        WalkerId = 5
    },
     new Dogs
    {
        Id = 5,
        Name = "Comet",
        CityId = 5,
        WalkerId = 5
    }
};



// Add services to the container.
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

app.MapGet("/api/hello", () =>
{
    return new { Message = "Welcome to DeShawn's Dog Walking" };
});

app.MapGet(
    "/api/walkers",
    () =>
    {
        return walkers.Select(w => new WalkersDTO
        {
            Id = w.Id,
            Name = w.Name,
            DogId = w.DogId
        });
    }
);

app.MapGet(
    "/api/dogs",
    () =>
    {
        foreach(Dogs dog in dogs)
        {
            dog.City = cities.FirstOrDefault(c => c.Id == dog.CityId);
        }
        return dogs.Select(d => new DogsDTO
        {
            Id = d.Id,
            Name = d.Name,
            CityId = d.CityId,
            WalkerId = d.WalkerId,
            City = new CitiesDTO
            {
                Id = d.City.Id,
                Name = d.City.Name
            }

        });
    }
);
app.Run();
