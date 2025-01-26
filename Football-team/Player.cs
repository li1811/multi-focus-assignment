public class Player
{
    public string Name{get;set;} = "";
    public int Age{get;set;} = 0;
    public int? PlayerNumber{get;set;} = null; //If you don't specify a playernumber upon creation, one will be randomly generated
    public string Position{get;set;} = "";
    public int Ranking{get;set;} = 0;
    

    public Player() { }
}