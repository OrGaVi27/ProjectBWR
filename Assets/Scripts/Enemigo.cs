using UnityEngine;


public class Enemigo : Mob
{

    private void Start()
    {
        DefinirEntidad();
    }

    private void FixedUpdate()
    {
        Disparar();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Proyectil"))
        {
            Destroy(col.gameObject);
            Eliminar();
        }
    }
}
