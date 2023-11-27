using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entidad : MonoBehaviour
{
    public Animator _anim;
    public Rigidbody2D _rb;
    public SpriteRenderer _sr;
    public Transform _trans;

    public void DefinirEntidad()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _trans = GetComponent<Transform>();
    }

    public void Eliminar()
    {
        Destroy(gameObject);
        if (CompareTag("Enemigo")) 
        {
            GameManager.Instance.score += 20;
            GameManager.Instance.ActualizarScore();
        }
    }
    public void Congelar()
    {
        _rb.velocity = new Vector3( 0, 0, 0);
    }
}
