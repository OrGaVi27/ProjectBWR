using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int Coins = 0;
    private int CoinsObt = 0;
    private float Score = 0;
    private float MaxScore = 0;
    public bool isDead = true;
    public GameObject gameOver;
    public GameObject MenuPrincipal;
    private float HoraInicio;

    private GameObject MC;

    public TextMeshProUGUI maxScoreText;    
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI coinsObtText;
    void Awake()
    {
        Score = Time.time;

        if (Instance == null) 
        {
            Instance = this;
            DataChanges dataChanges = new DataChanges();
            DataPersisted data = dataChanges.LoadData();
            if (data != null) 
            {
                Coins = data.Coins;
                MaxScore = (float)Math.Truncate(data.MaxScore);
                ScoreText.GetComponent<TextMeshProUGUI>().text = $"Score: {(Score)}";
                maxScoreText.GetComponent<TextMeshProUGUI>().text = $"High Score: {Math.Truncate(MaxScore)}";

            }
            DontDestroyOnLoad(this);

        }
        else if (Instance != this) Destroy(this.gameObject);

    }
    public void Start()
    {
        gameOver.SetActive(false);
        isDead = true;
    }

    private void Update()
    {
        if (isDead == false)
        {
            coinsText.GetComponent<TextMeshProUGUI>().text = $"Coins: {Coins}";
            if (Time.time - Score > 1f)
            {
                ScoreText.GetComponent<TextMeshProUGUI>().text = $"Score: {Math.Truncate(Score)}";
                Score = Time.time - HoraInicio;
                if (Score > MaxScore)
                {
                    MaxScore = Score;
                    maxScoreText.GetComponent<TextMeshProUGUI>().text = $"High Score: {Math.Truncate(MaxScore)}";
                }
            }
        }
    }

    public void OnLevelWasLoaded(int level)
    {
        Score = 0;
        HoraInicio = Time.time;
        MC = GameObject.Find("MC");
        maxScoreText.GetComponent<TextMeshProUGUI>().text = "High Score: " + MaxScore;
        ScoreText.GetComponent<TextMeshProUGUI>().text = Score.ToString();        
    }

    public void SceneChange(bool cont) 
    {
        if(SceneManager.GetActiveScene().buildIndex == 1 && cont == false)
        {
            MenuPrincipal.SetActive(true);
            ResetValues();
            Time.timeScale = 0;
            SceneManager.LoadScene(0);       
        }
        else
        {
            SceneManager.LoadScene(1);
            MenuPrincipal.SetActive(false);
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
        Score = 0;
        isDead = false;
        gameOver.SetActive(false);
        CoinsObt = 0;
    }
    public void Muerte()
    {
        MC.SetActive(false);
        gameOver.SetActive(true);
        isDead = true;
        coinsObtText.GetComponent<TextMeshProUGUI>().text = $"Coins: +{CoinsObt}";
        DataChanges dataChanges = new DataChanges();
        dataChanges.WriteData(new DataPersisted(Coins, 0, MaxScore, false, false, false, 0));
    }
    public void SumCoin() 
    {
        CoinsObt++;
        Coins++;
    }
}
