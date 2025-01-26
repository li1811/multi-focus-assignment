public class Team
{
    public string Name{get;set;} = "";
    public List<Player> players = new List<Player>();

    // check if player exists
    public bool CheckIfPlayerExists(Player player)
    {
        var existingPlayer = players.FirstOrDefault(p => p.Name == player.Name);

        if (existingPlayer != null) 
            return true;
        else 
            return false;
    }

    public int GetPlayerNumber()
    {
        Random random = new Random();
        Number:
        int playerNumber = random.Next(100);
        var existingNumber = players.FirstOrDefault(p => p.PlayerNumber == playerNumber);
        if (existingNumber != null)
            goto Number;
        return playerNumber;
    }

    
}