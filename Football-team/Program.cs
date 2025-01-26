using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Team? team = null;

app.MapGet("/", () => "Hello World!");

// Create team
app.MapPost("/createteam", ([FromBody] Team newTeam) =>
{

     
    if (team != null)
    {
        return Results.Ok(new { Message = $"There is already a team with the name {team.Name}." });
    }

    team = newTeam;

    return Results.Ok(new { Message = $"Team {team.Name} created." });

});

// Show Team
app.MapGet("/showteam", () => 
{
    if(team == null)
    {
        return Results.BadRequest(new {Message = "There is no team, one must be created first."});
    }
    return Results.Ok(team.players);
});

// Add player
app.MapPost("/addplayer", ([FromBody] Player player) =>
{
    string message = "";
    
    if(team == null)
    {
        return Results.BadRequest(new {Message = "You must create a team first"});
    }

    if(team.CheckIfPlayerExists(player))
    {
        return Results.BadRequest(new {Message = $"Player with name: {player.Name} already exists"});
    }

    if (player.PlayerNumber == null)
    {
        player.PlayerNumber = team.GetPlayerNumber();
    }

    var existingNumber = team.players.FirstOrDefault(p => p.PlayerNumber == player.PlayerNumber);
    if (existingNumber != null)
    {
        message += $"The number {player.PlayerNumber} is already in use, {player.Name} will be assigned a random number\n";
        player.PlayerNumber = team.GetPlayerNumber();
    }

    team.players.Add(player);
    message += @$"Player {player.Name} added";
    Console.WriteLine(message);

    return Results.Ok(message);
});


//Update using playernumber
app.MapPut("/updateplayer/{playernumber:int}", ([FromRoute] int playerNumber, [FromBody] Player updatedPlayer) =>
{
    if (team == null)
    {
        return Results.BadRequest(new { Message = "You must create a team first" });
    }

    
    var existingPlayer = team.players.FirstOrDefault(p => p.PlayerNumber == updatedPlayer.PlayerNumber);
    if (existingPlayer == null)
    {
        return Results.NotFound(new { Message = $"Player with number {playerNumber} not found" });
    }

    
    //check against empty entry 
    if (!string.IsNullOrWhiteSpace(updatedPlayer.Name))
    {
        existingPlayer.Name = updatedPlayer.Name;

    }

    existingPlayer.Age = updatedPlayer.Age;

    return Results.Ok(new { Message = $"Player with number {updatedPlayer.PlayerNumber} updated" });

});

// Find a player by playernumber
app.MapGet("/findplayer/{playerNumber:int}", ([FromRoute] int playerNumber) =>
{
    if (team == null)
    {
        return Results.BadRequest(new { Message = "You must create a team first" });
    }

    var player = team.players.FirstOrDefault(s => s.PlayerNumber == playerNumber);
    if (player == null)
    {
        return Results.NotFound(new { Message = $"Player with number {playerNumber} not found" });
    }

    return Results.Ok(player);
});

// Delete using playernumber
app.MapDelete("/deleteplayer/{playernumber:int}", ([FromRoute] int playernumber) =>
{
    
    if (team == null)
    {
        return Results.BadRequest(new { Message = "You must create a team first" });
    }

    
    var existingPlayer = team.players.FirstOrDefault(s => s.PlayerNumber == playernumber);

    if (existingPlayer == null)
    {
        return Results.NotFound(new { Message = $"Player with number {playernumber} not found" });
    }

    
    team.players.Remove(existingPlayer);

    return Results.Ok(new { Message = $"Player with number {playernumber} deleted" });
});

// Find youngest player
app.MapGet("/findyoungest", () =>
{

    
    if (team == null)
    {
        return Results.BadRequest(new { Message = "You must create a team first" });
    }

    var youngestPlayerAge = team.players.Min(s => s.Age);
    var youngestPlayer = team.players.Where(s => s.Age == youngestPlayerAge);

    return Results.Ok(new { Message = youngestPlayer });



});

// Find oldest player
app.MapGet("/findoldest", () =>
{

    
    if (team == null)
    {
        return Results.BadRequest(new { Message = "You must create a team first" });
    }
    var oldestPlayerAge = team.players.Max(s => s.Age);
    var oldestPlayer = team.players.Where(s => s.Age == oldestPlayerAge);
    return Results.Ok(new { Message = oldestPlayer });

});

// Get all players with specified ranking
app.MapGet("/findbyranking/{rank:int}",([FromRoute] int rank) =>
{
    List<Player> matchingPlayers = new List<Player>();
    if (team == null)
    {
        return Results.BadRequest(new { Message = "You must create a team first" });
    }

    foreach (var player in team.players)
    {
        if (player.Ranking == rank)
        {
            matchingPlayers.Add(player);
        }
    }

    return Results.Ok(matchingPlayers);
});

// Get team rank(average of all players)
app.MapGet("/getteamrank", () =>
{
    if (team == null)
    {
        return Results.BadRequest(new { Message = "You must create a team first" });
    }
    var sum = 0;
    foreach(Player player in team.players)
    {
        sum += player.Ranking;
    }
    double average = Math.Round((double)sum / team.players.Count, 1);

    return Results.Ok(new {Message = $"Team rank is {average}(team rank = average of all players)"});
});


app.Run();
