using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisiones : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Letal")
        {
            canvas.SetActive(true);
            gameObject.SetActive(false);
            
        }
    }
}
