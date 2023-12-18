using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowUp : Entity
{
    [SerializeField] GameObject focus;
    void Update()
    {
        Controls cont = focus.GetComponent<Controls>();

        _rb.velocity = new Vector3(cont.baseSpeed, 0, 0);

        if (!focus.activeSelf)
        {
            _rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
