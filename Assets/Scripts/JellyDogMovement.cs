using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JellyDogMovement : Enemy
{
    bool toUp;
    RaycastHit2D[] RaycastTop;
    RaycastHit2D[] RaycastBottom;

    // Start is called before the first frame update
    void Start()
    {
        DefineEntity();
        toUp = true;
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
                toUp = false;
            }
        }

        foreach (var hit in RaycastBottom)
        {
            if (hit.distance < 1 && hit.collider.gameObject.layer == 3)
            {
                toUp = true;
            }
        }

        if (toUp) _rb.velocity = new Vector3(_rb.velocity.x, 3f);
        else _rb.velocity = new Vector3(_rb.velocity.x, -3f);
    }
}
