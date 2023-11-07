using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCamara : Entidad
{
    [SerializeField] GameObject foco;
    private Transform _transFoco;


    // Start is called before the first frame update
    void Start()
    {
        DefinirEntidad();

        _transFoco = foco.GetComponent<Transform>();

        _trans.position = _transFoco.position;
        _rb.velocity = new Vector3(8.0f, 0, 0);

        _trans.position = new Vector3(_transFoco.position.x + 4f, -0.06f, -10f);
    }

    void Update()
    {
        if (!foco.activeSelf)
        {
            _rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
