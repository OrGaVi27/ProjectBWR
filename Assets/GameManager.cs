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

    public GameObject Player;

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
            UpdateCoins();
        }

        isDead = -1;

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
            else if (isDead == 0)
            {
                Player.GetComponent<SpriteRenderer>().enabled = false;
                Player.transform.position = new Vector3(-19, -3.5f, 0);
            }
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

    public void SceneChange(bool cont) 
    {
        if(SceneManager.GetActiveScene().buildIndex != 1 && !cont)
        {
            Destroy(Player);
            SoundManager.instance.Play("mainMenu");
            mainMenu.SetActive(true);
            SceneManager.LoadScene(1);
        }
        else
        {
            score = 0;
            startDate = Time.time;
            score = 0;
            isDead = -1;
            gameOver.SetActive(false);
            coinsObt = 0;
            SoundManager.instance.Stop("gameOver");
            UpdateScore();

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
            SoundManager.instance.Stop("slide");
            SoundManager.instance.Stop("music");
            SoundManager.instance.Play("death");
            SoundManager.instance.Play("gameOver");
            gameOver.SetActive(true);
            Player.GetComponent<Animator>().SetBool("isDead", true);
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
