[System.Serializable]
public class DataPersisted
{
    public int Coins;
    public float MaxScore;
    public int Shields;
    public bool IframePostHit;
    public bool IframePostChangeColor;
    public bool DontLoseColorAtShoot;
    public int DoubleCoinsAtCollect;

    public DataPersisted(int coins,int shields,float maxScore, bool iframePostHit,bool iframePostChangeColor,bool dontLoseColorAtShoot,int doubleCoinsAtCollect)
    {
        this.Coins = coins;
        this.MaxScore = maxScore;
        this.Shields = shields;
        this.IframePostHit = iframePostHit;
        this.IframePostChangeColor = iframePostChangeColor;
        this.DontLoseColorAtShoot = dontLoseColorAtShoot;
        this.DoubleCoinsAtCollect = doubleCoinsAtCollect;
        
    }
}
