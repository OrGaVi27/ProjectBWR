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
    public int longerInvulnerability;
    public int extraJumps;
    public int lessCooldownColorChange;
    public bool biggerBullets;
    public int marioStar;
    public int achieCoins;
    public int achieEnemies;
    public int achieWalls;
    public int achieDeaths;

    public DataPersisted(int coins,float maxScore, int shields, bool iFramePostHit,bool bulletPenetration,
        bool dontLoseColorAtShoot,int doubleCoinsAtCollect, int longerInvulnerability, int extraJumps,
        int lessCooldownColorChange, bool biggerBullets, int marioStar,
        int achiCoins, int achieEnemies, int achieWalls, int achieDeaths)
    {
        this.coins = coins;
        this.maxScore = maxScore;
        this.shields = shields;
        this.iFramePostHit = iFramePostHit;
        this.bulletPenetration = bulletPenetration;
        this.dontLoseColorAtShoot = dontLoseColorAtShoot;
        this.doubleCoinsAtCollect = doubleCoinsAtCollect;
        this.longerInvulnerability = longerInvulnerability;
        this.extraJumps = extraJumps;
        this.lessCooldownColorChange = lessCooldownColorChange;
        this.biggerBullets = biggerBullets;
        this.marioStar = marioStar;
        this.achieCoins = achiCoins;
        this.achieEnemies = achieEnemies;
        this.achieWalls = achieWalls;
        this.achieDeaths = achieDeaths;
    }
    public DataPersisted()
    {
        coins = 0;
        maxScore = 0;
        shields = 0;
        iFramePostHit = false;
        bulletPenetration = false;
        dontLoseColorAtShoot = false;
        doubleCoinsAtCollect = 0;
        longerInvulnerability = 0;
        extraJumps = 0;
        lessCooldownColorChange = 0;
        biggerBullets = false;
        marioStar = 0;
    }
}
