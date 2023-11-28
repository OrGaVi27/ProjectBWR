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
    private int Coins;
    private int CoinsObt = 0;
    public float Score;
    private float MaxScore;
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
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += OnSceneWasLoaded;
            SceneManager.LoadScene("Menu");

        }
        else if (Instance != this) Destroy(gameObject);

    }
    public void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemigos"), LayerMask.NameToLayer("Red"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemigos"), LayerMask.NameToLayer("Blue"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemigos"), LayerMask.NameToLayer("Obstaculos"), true);

        DataPersisted data = DataChanges.LoadData();
        if (data != null)
        {
            Coins = data.Coins;
            coinsText.GetComponent<TextMeshProUGUI>().text = $"Coins: {Coins}";
            //MaxScore = (float)Math.Truncate(data.MaxScore);
            MaxScore = data.MaxScore;
            ActualizarScore();
        }

        gameOver.SetActive(false);
        isDead = true;
    }

    private void Update()
    {
        if (isDead == false)
        {
            coinsText.GetComponent<TextMeshProUGUI>().text = $"Coins: {Coins}";
            if (Time.time - HoraInicio > 0.1f)
            {
                HoraInicio = Time.time;
                Score += 1;
                if (Score > MaxScore)
                {
                    MaxScore = Score;
                }
                ActualizarScore();
            }
        }
    }

    private void OnSceneWasLoaded(Scene scene, LoadSceneMode mode)
    {
        Score = 0;
        HoraInicio = Time.time;
        MC = GameObject.Find("MC");
        ActualizarScore();
    }

    public void SceneChange(bool cont) 
    {
        if(SceneManager.GetActiveScene().buildIndex == 1 && cont == false)
        {
            MenuPrincipal.SetActive(true);
            ResetValues();
            Time.timeScale = 0;
            SceneManager.LoadScene(0);
            SoundManager.instance.Play("mainMenu");
        }
        else
        {
            SoundManager.instance.Stop("mainMenu");
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
        SoundManager.instance.Stop("gameOver");
    }
    public void Muerte()
    {
        SoundManager.instance.Play("death");
        SoundManager.instance.Play("gameOver");
        MC.SetActive(false);
        SoundManager.instance.Stop("slide");
        SoundManager.instance.Stop("music");
        gameOver.SetActive(true);
        isDead = true;
        Time.timeScale = 0;
        coinsObtText.GetComponent<TextMeshProUGUI>().text = $"Coins: +{CoinsObt}";
        DataChanges.WriteData(new DataPersisted(Coins, 0, MaxScore, false, false, false, 0));
    }
    public void SumCoin() 
    {
        CoinsObt++;
        Coins++;
        Score += 5;
        ActualizarScore();
    }
    public void ActualizarScore()
    {
        ScoreText.GetComponent<TextMeshProUGUI>().text = $"Score: {Score}";
        maxScoreText.GetComponent<TextMeshProUGUI>().text = $"High Score: {Math.Truncate(MaxScore)}";
    }
}
