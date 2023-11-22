using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JellyDogMovement : Enemigo
{
    bool parriba;
    RaycastHit2D[] RaycastTop;
    RaycastHit2D[] RaycastBottom;

    // Start is called before the first frame update
    void Start()
    {
        DefinirEntidad();
        parriba = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        RaycastTop = Physics2D.RaycastAll(_trans.position, _trans.up);
        RaycastBottom = Physics2D.RaycastAll(_trans.position, -_trans.up);
        foreach (var hit in RaycastTop)
        {
            if (hit.distance < 1 && hit.collider.gameObject.layer == 10)
            {
                parriba = false;
            }
        }

        foreach (var hit in RaycastBottom)
        {
            if (hit.distance < 1 && hit.collider.gameObject.layer == 3)
            {
                parriba = true;
            }
        }

        if (parriba) _rb.velocity = new Vector3(_rb.velocity.x, 3f);
        else _rb.velocity = new Vector3(_rb.velocity.x, -3f);
    }
}
