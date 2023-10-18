[System.Serializable]
public class DataPersisted
{
    private int Coins;
    private int Shields;
    private bool IframePostHit;
    private bool IframePostChangeColor;
    private bool DontLoseColorAtShoot;
    private int DoubleCoinsAtCollect;

    public DataPersisted(int coins,int shields,bool iframePostHit,bool iframePostChangeColor,bool dontLoseColorAtShoot,int doubleCoinsAtCollect)
    {
        this.Coins = coins;
        this.Shields = shields;
        this.IframePostHit = iframePostHit;
        this.IframePostChangeColor = iframePostChangeColor;
        this.DontLoseColorAtShoot = dontLoseColorAtShoot;
        this.DoubleCoinsAtCollect = doubleCoinsAtCollect;
        
    }
}
