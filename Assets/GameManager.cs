using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public DataPersisted data;
    public int coins;
    private int coinsObt = 0;
    public float score;
    private float maxScore;
    public float isDead;
    public GameObject gameOver;
    public GameObject mainMenu;
    public GameObject shop;
    public List<GameObject> shopButtons;
    private float startDate;
    public Toggle fullScreen;
    private float clock;

    private GameObject MC;

    public TextMeshProUGUI maxScoreText;    
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI coinsObtText;
    public TextMeshProUGUI resDisplay;

    void Awake()
    {
        score = Time.time;

        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += OnSceneWasLoaded;
            SceneManager.LoadScene("Menu");
        }
        else if (Instance != this) Destroy(gameObject);
    }
    public void Start()
    {
        clock = Time.time;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Red"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Blue"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Obstacles"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Floor"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Ceiling"), true);

        data = DataChanges.LoadData();
        QualitySettings.vSyncCount = 0;
        SetResolution(1920, 1090, true);
        SetRefreshRate(0);
        if (data != null)
        {
            coins = data.coins;
            maxScore = data.maxScore;
            UpdateScore();
        }
        UpdateCoins();

        gameOver.SetActive(false);
        isDead = 0;

        foreach (var item in shop.GetComponentsInChildren<Transform>())
        {
            if(item.name.Length >= 6 && item.name[..6] == "Button")
            {
                shopButtons.Add(item.gameObject);
            }
        }
        shop.SetActive(false);
    }

    private void Update()
    {
        if (isDead < 0)
        {
            UpdateCoins();
            if (Time.time - startDate > 0.1f)
            {
                startDate = Time.time;
                score += 1;
                if (score > maxScore)
                {
                    maxScore = score;
                }
                UpdateScore();
            }
        }
        else if (isDead == 0 && Time.timeScale != 0)
        {
            //MC.GetComponent<Animator>().SetBool("isDead", true);
            //MC.SetActive(false);
            MC.GetComponent<SpriteRenderer>().enabled = false;
            Time.timeScale = 0;
        }

        if (Time.time - clock > 0.1f)
        {
            if (isDead > 0) isDead--;
            clock = Time.time;
        }


        if (shop.activeSelf)
        {
            EditText(0, $"Shields: {data.shields}\n 5 Coins");
            EditText(1, $"ExtraJumps: {data.extraJumps}\n 5 Coins");
            EditText(2, $"Less Color Cooldown: {data.lessCooldownColorChange}\n 5 Coins");
            EditText(3, $"Don't lose Color: {data.dontLoseColorAtShoot}\n 5 Coins");
            EditText(4, $"Piercing Bullets: {data.bulletPenetration}\n 5 Coins");
            EditText(5, $"Longer Invulnerability: {data.longerInvulnerability}\n 5 Coins");
            EditText(6, $"Bigger Bullets: {data.biggerBullets}\n 5 Coins");
            EditText(7, $"Double Coins (Consum): {data.doubleCoinsAtCollect}\n 5 Coins");
            EditText(8, $"Invulnerability (Consum): {data.marioStar}\n 5 Coins");
        }
    }

    private void OnSceneWasLoaded(Scene scene, LoadSceneMode mode)
    {
        score = 0;
        startDate = Time.time;
        MC = GameObject.Find("MC");
        UpdateScore();
    }

    public void SceneChange(bool cont) 
    {
        if(SceneManager.GetActiveScene().buildIndex != 1 && cont == false)
        {
            mainMenu.SetActive(true);
            ResetValues();
            Time.timeScale = 0;
            SceneManager.LoadScene(1);
            SoundManager.instance.Play("mainMenu");
        }
        else
        {
            SoundManager.instance.Stop("mainMenu");
            SoundManager.instance.Play("music");
            SceneManager.LoadScene(2);
            mainMenu.SetActive(false);
            ResetValues();
        }
    }
    public void CloseGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
    public void ResetValues()
    {
        Time.timeScale = 1;
        score = 0;
        isDead = -1;
        gameOver.SetActive(false);
        coinsObt = 0;
        SoundManager.instance.Stop("gameOver");
    }
    public void Death()
    {
        if(isDead == -1)
        {
            UnityEngine.Debug.Log("a");
            SoundManager.instance.Play("death");
            SoundManager.instance.Play("gameOver");
            SoundManager.instance.Stop("slide");
            SoundManager.instance.Stop("music");
            gameOver.SetActive(true);
            MC.GetComponent<Animator>().SetBool("isDead", true);
            isDead = 10f;
            coinsObtText.GetComponent<TextMeshProUGUI>().text = $"Coins: +{coinsObt}";
            DataChanges.WriteData(new DataPersisted(coins, maxScore, 0, false, false, false, 0, 0, 0, 0, false, 0));
        }
    }

    public void SumCoin(bool doubleCoins) 
    {
        if(doubleCoins)
        {
            coinsObt++;
            coins++;
        }
        coinsObt++;
        coins++;
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
        int extraJumpPrice = 5;
        int lessColorCooldownPrice = 5;
        int dontLoseColorPrice = 5;
        int piercingBulletsPrice = 5;
        int longerInvulnerabilityPrice = 5;
        int biggerBulletsPrice = 5;
        int doubleCoinsAtCollectPrice = 5;
        int invulnerabilityPrice = 5;

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
    private void EditText(int index, string text)
    {
        shopButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
    public void SetResolution(Resolution resolution)
    {
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetResolution(int width, int height, bool fullscreen)
    {
        Screen.SetResolution(width, height, fullscreen);
    }
    public void SetFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void SetRefreshRate(float maxFPS)
    {
        Application.targetFrameRate = (int)maxFPS;
    }
}
