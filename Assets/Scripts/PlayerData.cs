
public class PlayerData
{
    private string name; public string getName() { return name; }
    private int score; public int getScore() { return score; }
    private int highestChain; public int getHighestChain() { return highestChain; }

    public PlayerData()
    {
        this.name = "";
        this.score = 0;
        this.highestChain = 0;
    }

    public PlayerData(string name, int score, int highestChain)
    {
        this.name = name;
        this.score = score;
        this.highestChain = highestChain;
    }

    public void copy(PlayerData playerData)
    {
        this.name = playerData.name;
        this.score = playerData.score;
        this.highestChain = playerData.highestChain;
    }
}
