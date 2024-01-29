using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    Controls player;
    private float startTime;

    private void Start()
    {
        player = GetComponent<Controls>();
        startTime = Time.time;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Lethal":
                GameManager.Instance.Death();
                break;
        }
        if(col.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            foreach (RaycastHit2D rc in Physics2D.RaycastAll(player._trans.position, Vector2.down))
            {
                if (rc.collider.gameObject.layer == LayerMask.NameToLayer("Floor") || rc.collider.CompareTag("Obstacle") && rc.distance < 0.6f)
                {
                    player.availableJumps = player.maxJumps;
                    player._anim.SetBool("isJumping", false);
                }   
            }
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        switch (LayerMask.LayerToName(col.gameObject.layer))
        {
            case "Floor":
                player.availableJumps--;
                player._anim.SetBool("isJumping", true);
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
                GameManager.Instance.SumCoin(player.doubleCoins);
                break;
            case "ChangeLevel":
                col.GetComponent<Animator>().SetBool("transition", true);
                break;
            case "MainCamera":
                player.OnScreen(true);
                break;
            case "MCRelativePosition":
                player.delayed = false;
                break;
            case "ProjectileEnemy":
                Destroy(col);
                player.Hit();
                break;
            case "Enemy":
                player.Hit();
                break;
            case "Blue":
                if (!CompareTag("Blue"))
                {
                    player.Hit();
                } else
                {
                    GameManager.Instance.data.achieWalls++;
                }
                break;
            case "Red":
                if (!CompareTag("Red"))
                {
                    player.Hit();
                } else
                {
                    GameManager.Instance.data.achieWalls++;
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
            case "ChangeLevel":
                GameObject camara = GameObject.Find("Camera");
                Vector3 posicionCamara = GameObject.Find("LevelStart").transform.position;
                gameObject.transform.position = new Vector3(posicionCamara.x - 7f, gameObject.transform.position.y, gameObject.transform.position.z);
                camara.transform.position = new Vector3(posicionCamara.x, camara.transform.position.y, camara.transform.position.z);
                BiomeManager.Instance.RandomBiome();
                if (player.baseSpeed < 20) player.baseSpeed += 0.1f;
                col.GetComponent<Animator>().SetBool("transition", false);
                break;
        }
    }
}
