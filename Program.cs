using GameStore.Api.Dtos;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

//Data transfer Object 
List<GameDto> games = [
    new (
        1, 
        "Street Fither 11",
        "Fighting",
        19.99M,
        new DateOnly(1992,7,18)),
     new (
        2, 
        "GTA 5",
        "Acttion",
        1999.99M,
        new DateOnly(1992,7,18)),

     new (
        3, 
        "FIFA 25",
        "Soccer",
        566.99M,
        new DateOnly(1992,7,18))
];

// This is to get games
app.MapGet("games", () => games);

//GET/games/1
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(GetGameEndpointName);
  
//Post
app.MapPost("games", (CreateGameDto newGame) => {
    GameDto game = new (
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate);

        games.Add(game);  

        return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game); 
});

//PUT/Game
app.MapPut("games/{id}", (int id, UpdateGameDto updateGameDto)  => {
    //find the index of the project
    var index = games.FindIndex(game => game.Id == id);

    games[index] = new GameDto(
        id, 
        updateGameDto.Name,
        updateGameDto.Genre,
        updateGameDto.Price,
        updateGameDto.ReleaseDate);
   
   return Results.NoContent();

});

app.Run();
