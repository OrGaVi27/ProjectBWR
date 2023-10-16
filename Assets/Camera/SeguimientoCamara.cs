using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    private Transform _trans;
    private Rigidbody2D _rb;

    [SerializeField] GameObject foco;
    private Transform _transFoco;


    // Start is called before the first frame update
    void Start()
    {
        _trans = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();

        _transFoco = foco.GetComponent<Transform>();

        _trans.position = _transFoco.position;
        _rb.velocity = new Vector3(8.0f, 0, 0);

        _trans.position = new Vector3(_transFoco.position.x, _trans.position.y, -10f);
    }
}
