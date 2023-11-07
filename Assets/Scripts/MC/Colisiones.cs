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
            GameObject camara = GameObject.Find("Camara");
            Vector3 posicionCamara = GameObject.Find("InicioNivel").transform.position;
            gameObject.transform.position = new Vector3(posicionCamara.x, posicionCamara.y, gameObject.transform.position.z);
            camara.transform.position = new Vector3(posicionCamara.x, posicionCamara.y, camara.transform.position.z);
        }
    }
}
