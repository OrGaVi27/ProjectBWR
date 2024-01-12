using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Entity
{
    [Range(1, 100)]
    [SerializeField] private float speed = 30f;

    [Range(1, 10)]
    [SerializeField] private float lifetime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.right * speed;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("MainCamera")) Destroy(gameObject);
    }
}
