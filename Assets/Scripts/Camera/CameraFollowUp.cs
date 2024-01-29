using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowUp : Entity
{
    [SerializeField] GameObject focus;
    private void Start()
    {
        GameManager.Instance.Player = focus;
    }
    void Update()
    {
        Controls cont = focus.GetComponent<Controls>();

        if(GameManager.Instance.isDead == -1) _rb.velocity = new Vector3(cont.baseSpeed, 0, 0);
        else _rb.velocity = new Vector3(0, 0, 0);
    }
}
