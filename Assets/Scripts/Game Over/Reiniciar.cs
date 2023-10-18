using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Reiniciar : MonoBehaviour
{
    public bool continuar = false;
    void Awake()
    {
        GameObject obj = GameObject.Find("Reiniciar escena");
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}