using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCamara : Entidad
{
    [SerializeField] GameObject foco;
    private Controles cont;

    // Start is called before the first frame update
    void Start()
    {
        DefinirEntidad();

        cont = foco.GetComponent<Controles>();

        _rb.velocity = new Vector3(cont.velocidadBase, 0, 0);
    }

    void Update()
    {
        if (!foco.activeSelf)
        {
            _rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
