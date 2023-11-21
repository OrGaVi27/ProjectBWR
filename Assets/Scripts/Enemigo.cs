using UnityEngine;


public class Enemigo : Mob
{
    private bool onCamera;

    private void Start()
    {
        DefinirEntidad();
        onCamera = false;
    }

    private void FixedUpdate()
    {
        if (onCamera) Disparar();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Proyectil"))
        {
            Destroy(col.gameObject);
            Eliminar();
        }

        if (col.CompareTag("MainCamera")) onCamera = true;
        else onCamera = false;
    }
}
