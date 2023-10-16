using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] private float speed = 30f;

    [Range(1, 10)]
    [SerializeField] private float lifetime = 3f;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.up * speed;
    }
}
