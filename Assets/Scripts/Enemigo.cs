using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : Mob
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Proyectil"))
        {
            Destroy(col.gameObject);
            SoundManager.instance.Play("enemyDeath");
            Eliminar();
        }
    }
}
