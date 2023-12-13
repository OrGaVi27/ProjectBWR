using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    Controls player;

    private void Start()
    {
        player = GetComponent<Controls>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Lethal":
                GameManager.Instance.Death();
                break;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        switch (LayerMask.LayerToName(col.gameObject.layer))
        {
            case "Floor":
                player.availableJumps--;
                Debug.Log("Salto--");
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
                if(player.baseSpeed < 20) player.baseSpeed += 0.1f;
                break;
            case "MainCamera":
                player.OnScreen(true);
                break;
            case "MCRelativePosition":
                player.delayed = false;
                break;
            case "Projectile":
                Destroy(col);
                player.Hit();
                break;
            case "Enemy":
                player.Hit();
                break;
            case "Blue":
                if (!gameObject.CompareTag("Blue"))
                {
                    player.Hit();
                }
                break;
            case "Red":
                if (!gameObject.CompareTag("Red"))
                {
                    player.Hit();
                }
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "MainCamera":
                player.OnScreen(false);
                break;
            case "MCRelativePosition":
                player.delayed = true;
                break;
        }
    }
}
