using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisiones : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Letal":
            case "Enemigo":
                gameObject.SetActive(false);
                break;
            case "Blue":
                if(!gameObject.CompareTag("Blue"))
                {
                    gameObject.SetActive(false);
                }
                break;
            case "Red":
                if (!gameObject.CompareTag("Red"))
                {
                    gameObject.SetActive(false);
                }
                break;
        }
    }
}
