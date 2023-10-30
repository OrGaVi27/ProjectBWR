using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisiones : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Letal":
            case "Enemigo":
                Muerte();
                break;
            case "Blue":
                if(!gameObject.CompareTag("Blue"))
                {
                    Muerte();
                }
                break;
            case "Red":
                if (!gameObject.CompareTag("Red"))
                {
                    Muerte();
                }
                break;
        }
    }
    void Muerte()
    {
        canvas.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("CambiarNivel"))
        {
            gameObject.transform.position = GameObject.Find("InicioNivel").transform.position;
            GameObject.Find("Camara").transform.position = GameObject.Find("InicioNivel").transform.position;
        }
    }
}
