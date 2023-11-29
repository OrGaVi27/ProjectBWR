using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    Controls cont;

    private void Start()
    {
        cont = GetComponent<Controls>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Lethal":
            case "Enemy":
                GameManager.Instance.Death();
                break;
            case "Blue":
                if(!gameObject.CompareTag("Blue"))
                {
                    GameManager.Instance.Death();
                }
                break;
            case "Red":
                if (!gameObject.CompareTag("Red"))
                {
                    GameManager.Instance.Death();
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
            case "ChangeLevel":
                GameObject camara = GameObject.Find("Camera");
                Vector3 posicionCamara = GameObject.Find("LevelStart").transform.position;
                gameObject.transform.position = new Vector3(posicionCamara.x - 7f, gameObject.transform.position.y, gameObject.transform.position.z);
                camara.transform.position = new Vector3(posicionCamara.x, camara.transform.position.y, camara.transform.position.z);
                BiomeManager.Instance.RandomBiome();
                break;
            case "MainCamera":
                cont.OnScreen(true);
                break;
            case "MCRelativePosition":
                cont.delayed = false;
                break;
            case "Projectile":
                GameManager.Instance.Death();
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "MainCamera":
                cont.OnScreen(false);
                break;
            case "MCRelativePosition":
                cont.delayed = true;
                break;
        }
    }
}
