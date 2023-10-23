using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   private DataPersisted DataPers;

    public static GameManager Instance;
    void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            DataPers = DataChanges.LoadData();
            DontDestroyOnLoad(this);
        }
        else if (Instance != this) Destroy(this.gameObject);

    }

    public void SceneChange(int Scene) 
    {
        SceneManager.LoadScene(Scene);
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
