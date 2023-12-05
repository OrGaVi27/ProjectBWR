[System.Serializable]
public class DataPersisted
{
    public int coins;
    public float maxScore;
    public int shields;
    public bool iFramePostHit;
    public bool bulletPenetration;
    public bool dontLoseColorAtShoot;
    public int doubleCoinsAtCollect;
    public int invulnerability;

    public DataPersisted(int coins,float maxScore, int shields, bool iFramePostHit,bool bulletPenetration,bool dontLoseColorAtShoot,int doubleCoinsAtCollect, int invulnerability)
    {
        this.coins = coins;
        this.maxScore = maxScore;
        this.shields = shields;
        this.iFramePostHit = iFramePostHit;
        this.bulletPenetration = bulletPenetration;
        this.dontLoseColorAtShoot = dontLoseColorAtShoot;
        this.doubleCoinsAtCollect = doubleCoinsAtCollect;
        this.invulnerability = invulnerability;
}
}
