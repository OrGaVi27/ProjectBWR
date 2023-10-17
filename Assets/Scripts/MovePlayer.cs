using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    public float speed;
    //public float acceleration;
    //funcion para ir aumentando la velocidad
    //double movement() {
    //    speed = speed/10 + acceleration/10;
    //    return speed; }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        transform.position += new Vector3(speed * 1, 0, 0);
    }
}
