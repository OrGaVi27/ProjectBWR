using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JellyDogMovement : Enemigo
{
    bool parriba;
    RaycastHit2D RaycastTop;
    RaycastHit2D RaycastBottom;
    string[] ignoredTags = { "Blue", "Red", "MainCamera", "Enemigo" };

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

        Debug.Log("Top: " + RaycastTop.distance);
        Debug.Log("Bottom: " + RaycastBottom.distance);
        
        if (RaycastTop.collider != null)
        {
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

        if (parriba) _rb.velocity = new Vector3(_rb.velocity.x, 3f);
        else _rb.velocity = new Vector3(_rb.velocity.x, -3f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "JellyDogCollider") 
        {
            _rb.velocity += new Vector2(GameObject.Find("Camara").GetComponent<Rigidbody2D>().velocity.x, _rb.velocity.y);
        }
    }
}
