public class Team
{
    public string Name{get;set;} = "";
    public List<Player> players = new List<Player>();

    // check if player exists, I didn't end up using this as much as i thought i would, but nice to have a method for it
    public bool CheckIfPlayerExists(Player player)
    {
        var existingPlayer = players.FirstOrDefault(p => p.Name == player.Name);

        if (existingPlayer != null) 
            return true;
        else 
            return false;
    }
    // Generates a new playernumber if one isn't specified when player is added. I use goto to generate a new number if the number it generates is already in use
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