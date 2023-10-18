using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionesBala : MonoBehaviour
{

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
        if(collision.gameObject.CompareTag("Enemigo"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
