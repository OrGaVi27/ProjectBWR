using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Controles : Mob
{
    private float velocidad = 8.0f;  //Velocidad lateral base
    private float cooldownRecuperacionSalto = 0.1f; //Tiempo de espera mínimo para que el salto no se recupere antes de que el objeto se levante del suelo
    private float horaUltimoSalto;  //Variable utilizada en la comprobación del cooldown de recuperacion de los saltos
    private bool enTierra;  //Almacena el resultado de la comprobación del contacto del personaje con el suelo
    private float radioSuelo = 1;   //Variable usada en la formula de la comprobación "enTierra"
    private LayerMask layerSuelo;   //Almacena la capa del Suelo
    private int saltosDisponibles = 0;   //Saltos disponibles en el momento
    private int saltosMaximos = 1;  //Saltos que se asignaran a saltosDisponibles en cuanto el MC repose en el suelo
    //private bool agachado;

    private void Start()
    {
        DefinirEntidad();

        cooldownDisparo = 0.5f;
        fuerzaSalto = 14.0f;
        _rb.gravityScale = 4f;
        horaUltimoDisparo = Time.time - 1f;
        horaUltimoSalto = Time.time;
        layerSuelo = LayerMask.GetMask("Suelo");
    }


    private void Update()
    {
        enTierra = Physics2D.OverlapCircle(_trans.position, radioSuelo, layerSuelo); ;

        // Velocidad constante
        _rb.velocity = new Vector2(velocidad, _rb.velocity.y);

        // Casos por cada tecla
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Disparar();
        }
        if(Input.GetKeyDown(KeyCode.W) && saltosDisponibles > 0)
        {
            _anim.SetBool("isJumping", true);
            Saltar();
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            CambioColor("Blue");
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            CambioColor("Red");
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            _anim.SetBool("isCrouching", true);
            SoundManager.instance.Play("slide");
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            _anim.SetBool("isCrouching", false);
            SoundManager.instance.Stop("slide");
        }

        // Recuperación de saltos al tocar el suelo
        if (enTierra && Time.time - horaUltimoSalto > cooldownRecuperacionSalto)
        {
            saltosDisponibles = saltosMaximos;
            _anim.SetBool("isJumping", false);
        }
    }
    private void Saltar()
    {
        _rb.velocity = Vector2.up * fuerzaSalto;
        saltosDisponibles--;
        horaUltimoSalto = Time.time;
    }
}
