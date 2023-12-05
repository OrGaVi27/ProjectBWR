using Unity.VisualScripting;
using UnityEngine;


public class Enemy : Mob
{
    private bool onCamera;

    private void Start()
    {
        onCamera = false;
    }

    private void FixedUpdate()
    {
        if (onCamera) Disparar();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Projectile"))
        {
            Destroy(col.gameObject);
            SoundManager.instance.Play("enemyDeath");
            Delete();
        }

        if (col.CompareTag("MainCamera")) onCamera = true;

        if (col.gameObject.name == "JellyDogCollider" && gameObject.name.Split(' ')[0] == "JellyDog")
        {
            _rb.velocity += new Vector2(GameObject.Find("Camera").GetComponent<Rigidbody2D>().velocity.x, _rb.velocity.y);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("MainCamera")) onCamera = false;
    }
}
