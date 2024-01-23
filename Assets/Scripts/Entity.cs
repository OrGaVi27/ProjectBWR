using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [HideInInspector]
    public Animator _anim;
    [HideInInspector]
    public Rigidbody2D _rb;
    [HideInInspector]
    public SpriteRenderer _sr;
    [HideInInspector]
    public Transform _trans;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _trans = GetComponent<Transform>();
    }
}
