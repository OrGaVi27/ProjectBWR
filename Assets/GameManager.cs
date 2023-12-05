using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int coins;
    private int coinsObt = 0;
    public float score;
    private float maxScore;
    public bool isDead = true;
    public GameObject gameOver;
    public GameObject mainMenu;
    private float startDate;
    public Toggle fullScreen;

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
        SetResolution(1920, 1090, true);
        SetRefreshRate(60);
        DataPersisted data = DataChanges.LoadData();
        if (data != null)
        {
            coins = data.Coins;
            coinsText.GetComponent<TextMeshProUGUI>().text = $"Coins: {coins}";
            //MaxScore = (float)Math.Truncate(data.MaxScore);
            maxScore = data.MaxScore;
            UpdateScore();
        }

        gameOver.SetActive(false);
        isDead = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            resDisplay.text = Screen.currentResolution.ToString();
        }
        if (isDead == false)
        {
            coinsText.GetComponent<TextMeshProUGUI>().text = $"Coins: {coins}";
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
        isDead = false;
        gameOver.SetActive(false);
        coinsObt = 0;
        SoundManager.instance.Stop("gameOver");
    }
    public void Death()
    {
        SoundManager.instance.Play("death");
        SoundManager.instance.Play("gameOver");
        MC.SetActive(false);
        SoundManager.instance.Stop("slide");
        SoundManager.instance.Stop("music");
        gameOver.SetActive(true);
        isDead = true;
        Time.timeScale = 0;
        coinsObtText.GetComponent<TextMeshProUGUI>().text = $"Coins: +{coinsObt}";
        DataChanges.WriteData(new DataPersisted(coins, 0, maxScore, false, false, false, 0));
    }
    public void SumCoin() 
    {
        coinsObt++;
        coins++;
        score += 5;
        UpdateScore();
    }
    public void UpdateScore()
    {
        ScoreText.GetComponent<TextMeshProUGUI>().text = $"Score: {score}";
        maxScoreText.GetComponent<TextMeshProUGUI>().text = $"High Score: {Math.Truncate(maxScore)}";
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
