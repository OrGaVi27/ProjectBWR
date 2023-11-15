using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyDogMovement : Enemigo
{
    bool parriba;
    RaycastHit2D RaycastTop;
    RaycastHit2D RaycastBottom;
    // Start is called before the first frame update
    void Start()
    {
        DefinirEntidad();
        parriba = true;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastTop = Physics2D.Raycast(_trans.position, _trans.up);
        RaycastBottom = Physics2D.Raycast(_trans.position, -_trans.up);

        if (RaycastTop.collider != null)
        {
            Debug.Log(RaycastTop.collider.gameObject.name);
            if (RaycastTop.distance < 1)
            {
                parriba = false;
            }
        }

        if (RaycastBottom.collider != null)
        {
            if (RaycastBottom.distance < 1)
            {
                parriba = true;
            }
        }

        if (parriba) _rb.velocity = new Vector3(0, 3f);
        else _rb.velocity = new Vector3(0, -3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "JellyDogCollider") 
        {
            _rb.velocity += GameObject.Find("Camara").GetComponent<Rigidbody2D>().velocity;
        }
    }
}
