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
            case "Coin":
                collision.gameObject.SetActive(false);
                GameManager.Instance.SumCoin();
                break;                
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Coin":
                collision.gameObject.SetActive(false);
                GameManager.Instance.SumCoin();
                break;
        }
    }
}
