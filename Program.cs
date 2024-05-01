using DeShawn.Models;
using DeShawn.Models.DTOs;
using System.Linq;

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
        CityId = 1
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
        Id = 6,
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
    "/api/cities",
    () =>
    {
        return cities.Select(c => new CitiesDTO
        {
            Id = c.Id,
            Name = c.Name,
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
            dog.Walkers = walkers.FirstOrDefault(w => w.Id == dog.WalkerId);
        }
        
        return dogs.Select(d => new DogsDTO
        {
            Id = d.Id,
            Name = d.Name,
            CityId = d.CityId,
            WalkerId = d.WalkerId,
            Walkers = d.WalkerId == 0 ? null :
            new WalkersDTO
            {
                Id = d.Walkers.Id,
                Name = d.Walkers.Name
            },
            City = new CitiesDTO
            {
                Id = d.City.Id,
                Name = d.City.Name
            }

        });
    }
);

// need to iterate through dogs based on dogId and match to walkerID to display
app.MapGet("/api/dogs/{id}", (int id) =>
{
    Dogs dog = dogs.FirstOrDefault(d => d.Id == id);
    Walkers walker = walkers.FirstOrDefault(w => w.Id == dog.WalkerId);
    Cities city = cities.FirstOrDefault(c => c.Id == dog.CityId);

    DogsDTO dogDTO = new DogsDTO
    {
        Id = dog.Id,
        Name = dog.Name,
        CityId = dog.CityId,
        WalkerId = dog.WalkerId,
        City = new CitiesDTO
        {
            Id = city.Id,
            Name = city.Name
        },
        Walkers = walker == null ? null : new WalkersDTO
        {
            Id = walker.Id,
            Name = walker.Name,
            DogId = walker.DogId
        }
    };

    return Results.Ok(dogDTO);
});

app.MapPost("/api/addadog", (Dogs dog) =>
{
    dog.Id = dogs.Max(d => d.Id) + 1;
    dogs.Add(dog);
    
    return Results.Created($"/api/dogs/{dog.Id}", new DogsDTO
    {
        Id = dog.Id,
        Name = dog.Name,
        CityId = dog.CityId,
        WalkerId = dog.WalkerId
    });
});


app.Run();
