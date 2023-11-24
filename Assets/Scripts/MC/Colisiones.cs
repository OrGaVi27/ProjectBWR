using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisiones : MonoBehaviour
{
    Controles cont;
    [SerializeField] private GameObject canvas;

    private void Start()
    {
        cont = GetComponent<Controles>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Letal":
            case "Enemigo":
                GameManager.Instance.Muerte();
                break;
            case "Blue":
                if(!gameObject.CompareTag("Blue"))
                {
                    GameManager.Instance.Muerte();
                }
                break;
            case "Red":
                if (!gameObject.CompareTag("Red"))
                {
                    GameManager.Instance.Muerte();
                }
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Coin":
                col.gameObject.SetActive(false);
                SoundManager.instance.Play("coin");
                GameManager.Instance.SumCoin();
                break;
            case "CambiarNivel":
                GameObject camara = GameObject.Find("Camara");
                Vector3 posicionCamara = GameObject.Find("InicioNivel").transform.position;
                gameObject.transform.position = new Vector3(posicionCamara.x - 7f, gameObject.transform.position.y, gameObject.transform.position.z);
                camara.transform.position = new Vector3(posicionCamara.x, camara.transform.position.y, camara.transform.position.z);
                break;
            case "MainCamera":
                cont.EnPantalla(true);
                break;
            case "MCRelativePosition":
                cont.atrasado = false;
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "MainCamera":
                cont.EnPantalla(false);
                break;
            case "MCRelativePosition":
                cont.atrasado = true;
                break;
        }
    }
}
