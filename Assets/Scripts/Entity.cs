using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator _anim;
    public Rigidbody2D _rb;
    public SpriteRenderer _sr;
    public Transform _trans;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _trans = GetComponent<Transform>();
    }

    public void Delete()
    {
        Destroy(gameObject);
        if (CompareTag("Enemy")) 
        {
            GameManager.Instance.score += 20;
            GameManager.Instance.UpdateScore();
        }
    }
}
