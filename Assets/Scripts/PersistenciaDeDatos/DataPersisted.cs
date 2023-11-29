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
        Coins = coins;
        MaxScore = maxScore;
        Shields = shields;
        IframePostHit = iframePostHit;
        IframePostChangeColor = iframePostChangeColor;
        DontLoseColorAtShoot = dontLoseColorAtShoot;
        DoubleCoinsAtCollect = doubleCoinsAtCollect;
        
    }
}
