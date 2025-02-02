using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager Instance;
    [HideInInspector]
    public DataPersisted data;
    [HideInInspector]
    public int coins;
    private int coinsObt = 0;
    [HideInInspector]
    public float score;
    [HideInInspector]
    public float maxScore;
    [HideInInspector]
    public float isDead;
    public GameObject gameOver;
    public GameObject mainMenu;
    public GameObject shop;
    public GameObject achievements;
    public GameObject statusBonus;
    public GameObject statusInv;
    private float clock;
    private float startDate;

    [HideInInspector]
    public GameObject Player;

    public TextMeshProUGUI maxScoreText;    
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI coinsObtText;


    void Awake()
    {
        score = Time.time;

        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(this);
            SceneManager.LoadScene("Menu");
        }
        else if (Instance != this) Destroy(gameObject);
    }
    public void Start()
    {
        if (PlayerPrefs.HasKey("FPSCounter")) GetComponent<FPSCounter>().enabled = Convert.ToBoolean(PlayerPrefs.GetFloat("FPSCounter"));
        else GetComponent<FPSCounter>().enabled = false;

        if (PlayerPrefs.HasKey("vsync")) QualitySettings.vSyncCount = Convert.ToInt32(Convert.ToBoolean(PlayerPrefs.GetFloat("vsync")));
        else QualitySettings.vSyncCount = 60;

        if (PlayerPrefs.HasKey("fullscreen")) Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetFloat("fullscreen"));
        else Screen.fullScreen = true;

        if (PlayerPrefs.HasKey("musicVolume")) AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
        else AudioListener.volume = 1;

        

        clock = Time.time;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Red"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Blue"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Obstacles"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Floor"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Ceiling"), true);

        data = DataChanges.LoadData();
        if (data == null) data = new();

        Resolution resolution = ResolutionControl.GetFilteredResolutions()[PlayerPrefs.GetInt("resolution")];
        if (PlayerPrefs.HasKey("resolution"))
        {
            Screen.SetResolution( resolution.width, resolution.height, Convert.ToBoolean(PlayerPrefs.GetFloat("fullscreen")));
        }

        SetRefreshRate(PlayerPrefs.GetFloat("frameRate"));

        coins = data.coins;
        maxScore = data.maxScore;
        UpdateScore();
        UpdateCoins();

        isDead = -1;
    }

    private void Update()
    {

        if (Time.time - clock > 0.1f)
        {
            if (isDead > 0) isDead--;
            clock = Time.time;

            if (isDead == -1 && SceneManager.GetActiveScene().buildIndex == 2)
            {
                if (Player == null) GameObject.FindGameObjectWithTag("Player");
                UpdateCoins();
                UpdateScore();

                score += 1;
                if (score > maxScore)
                {
                    maxScore = score;
                }
            }
            else if (isDead == 0 && Player != null)
            {
                Player.GetComponent<SpriteRenderer>().enabled = false;
                Player.transform.position = new Vector3(-19, -3.5f, 0);
            }
        }
    }

    public void SceneChange(bool cont) 
    {
        if(SceneManager.GetActiveScene().buildIndex != 1 && !cont)
        {
            Destroy(Player);
            SoundManager.instance.Stop("gameOver");
            SoundManager.instance.Play("mainMenu");
            mainMenu.SetActive(true);
            gameOver.SetActive(false);
            SceneManager.LoadScene(1);
        }
        else
        {
            score = 0;
            clock = Time.time;
            score = 0;
            isDead = -1;
            gameOver.SetActive(false);
            coinsObt = 0;
            SoundManager.instance.Stop("gameOver");
            UpdateScore();
            startDate = Time.time;

            SoundManager.instance.Stop("mainMenu");
            SoundManager.instance.Play("music");
            mainMenu.SetActive(false);
            SceneManager.LoadScene(2);
        }
    }
    public void CloseGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
    public void Death()
    {
        if(isDead == -1 && Time.time - startDate > 1)
        {
            data.achieDeaths++;

            SoundManager.instance.Play("death");
            SoundManager.instance.Play("gameOver");
            SoundManager.instance.Stop("slide");
            SoundManager.instance.Stop("music");

            Player.GetComponent<Animator>().SetBool("isDead", true);
            gameOver.SetActive(true);
            coinsObtText.GetComponent<TextMeshProUGUI>().text = $"Coins: +{coinsObt}";
            data.coins = coins;
            data.maxScore = maxScore;
            DataChanges.WriteData(data);
            isDead = 10f;
        }
    }
    public void ResetData()
    {
        data = new DataPersisted();
        DataChanges.WriteData(data);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        coins = data.coins;
        maxScore = data.maxScore;
        UpdateCoins();
        UpdateScore();
    }
    public void SumCoin(bool doubleCoins) 
    {
        if(doubleCoins)
        {
            coinsObt++;
            coins++;
            data.achieCoins++;
        }
        coinsObt++;
        coins++;
        data.achieCoins++;

        score += 5;
        UpdateScore();
    }
    public void AddCoins(int value)
    {
        coins += value;
        UpdateCoins();
    }
    public void UpdateScore()
    {
        ScoreText.GetComponent<TextMeshProUGUI>().text = $"Score: {score}";
        maxScoreText.GetComponent<TextMeshProUGUI>().text = $"High Score: {Math.Truncate(maxScore)}";
    }
    public void UpdateCoins()
    {
        coinsText.GetComponent<TextMeshProUGUI>().text = $"Coins: {coins}";
    }
    public void Purchase(string element)
    {
        int shieldPrice = 5;
        int extraJumpPrice = 100;
        int lessColorCooldownPrice = 50;
        int dontLoseColorPrice = 125;
        int piercingBulletsPrice = 50;
        int longerInvulnerabilityPrice = 50;
        int biggerBulletsPrice = 75;
        int doubleCoinsAtCollectPrice = 12;
        int invulnerabilityPrice = 15;

        switch (element)
        {
            case "Shield":
                if (data.shields < 99 && coins >= shieldPrice)
                {
                    data.shields++;
                    coins -= shieldPrice;
                }
                break;
            case "ExtraJump":
                if (data.extraJumps < 2 && coins >= extraJumpPrice)
                {
                    data.extraJumps++;
                    coins -= extraJumpPrice;
                }
                break;
            case "LessColorCooldown":
                if (data.lessCooldownColorChange < 2 && coins >= lessColorCooldownPrice)
                {
                    data.lessCooldownColorChange++;
                    coins -= lessColorCooldownPrice;
                }
                break;
            case "DontLoseColor":
                if (!data.dontLoseColorAtShoot && coins >= dontLoseColorPrice)
                {
                    data.dontLoseColorAtShoot = true;
                    coins -= dontLoseColorPrice;
                }
                break;
            case "PiercingBullets":
                if (!data.bulletPenetration && coins >= piercingBulletsPrice)
                {
                    data.bulletPenetration = true;
                    coins -= piercingBulletsPrice;
                }
                break;
            case "LongerInvulnerability":
                if (data.longerInvulnerability < 3 && coins >= longerInvulnerabilityPrice)
                {
                    data.longerInvulnerability++;
                    coins -= longerInvulnerabilityPrice;
                }
                break;
            case "BiggerBullets":
                if (!data.biggerBullets && coins >= biggerBulletsPrice)
                {
                    data.biggerBullets = true;
                    coins -= biggerBulletsPrice;
                }
                break;
            case "DoubleCoins":
                if (data.doubleCoinsAtCollect < 99 && coins >= doubleCoinsAtCollectPrice)
                {
                    data.doubleCoinsAtCollect++;
                    coins -= doubleCoinsAtCollectPrice;
                }
                break;
            case "Invulnerability":
                if (data.doubleCoinsAtCollect < 99 && coins >= invulnerabilityPrice)
                {
                    data.marioStar++;
                    coins -= invulnerabilityPrice;
                }
                break;
        }

        UpdateCoins();
    }
    public void SetResolution(Resolution resolution)
    {
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetResolution(int width, int height, bool fullscreen)
    {
        Screen.SetResolution(width, height, fullscreen);
    }
    public void SetRefreshRate(float maxFPS)
    {
        Application.targetFrameRate = (int)maxFPS;
    }
    public void SetStatus(float time, string status, bool active)
    {
        Player.TryGetComponent(out SpriteRenderer spr);
        if (Player == null || !active || !spr.enabled)
        {
            switch (status)
            {
                case "bonus":
                    statusBonus.SetActive(false);
                    break;
                case "inv":
                    statusInv.SetActive(false);
                    break;
            }
            return;
        }
        switch (status)
        {
            case "bonus":
                if (time <= 0) statusBonus.SetActive(false);
                else
                {
                    statusBonus.SetActive(true);
                    statusBonus.GetComponent<TextMeshProUGUI>().text = Timer(time);
                }
                break;
            case "inv":
                if (time <= 0) statusInv.SetActive(false);
                else
                {
                    statusInv.SetActive(true);
                    statusInv.GetComponent<TextMeshProUGUI>().text = Timer(time);
                }
                break;
        }
    }
    private string Timer(float time)
    {
        string min = "00", sec;

        if (time >= 60)
        {
            min = Math.Round(time / 60).ToString();
            if (time / 60 < 10) min = "0" + min;
        }

        sec = Math.Round(time % 60).ToString();
        if (time % 60 < 10) sec = "0" + sec;

        return $"{min}:{sec}"; 
    }
}
