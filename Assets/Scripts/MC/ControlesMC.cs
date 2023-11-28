using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Controles : Mob
{
    public float velocidadBase = 8.0f;  //Velocidad lateral base
    private float cooldownRecuperacionSalto = 0.1f; //Tiempo de espera m�nimo para que el salto no se recupere antes de que el objeto se levante del suelo
    private float horaUltimoSalto;  //Variable utilizada en la comprobaci�n del cooldown de recuperacion de los saltos
    private bool enTierra;  //Almacena el resultado de la comprobaci�n del contacto del personaje con el suelo
    private float radioSuelo = 1;   //Variable usada en la formula de la comprobaci�n "enTierra"
    private LayerMask layerSuelo;   //Almacena la capa del Suelo
    private int saltosDisponibles = 0;   //Saltos disponibles en el momento
    private int saltosMaximos = 1;  //Saltos que se asignaran a saltosDisponibles en cuanto el MC repose en el suelo
    private bool enPantalla; //Se modifica seg�n el MC entre o salga de pantalla.
    private float horaFueraPantalla; //Registra el momento en el que sale de pantalla.
    public bool atrasado;

    private void Start()
    {
        DefinirEntidad();

        cooldownDisparo = 0.5f;
        fuerzaSalto = 14.0f;
        _rb.gravityScale = 4f;
        horaUltimoDisparo = Time.time - 1f;
        horaUltimoSalto = Time.time;
        layerSuelo = LayerMask.GetMask("Suelo");
        atrasado = true;
    }


    private void Update()
    {
        enTierra = Physics2D.OverlapCircle(_trans.position, radioSuelo, layerSuelo); ;

        // Velocidad constante
        if(atrasado) _rb.velocity = new Vector2(velocidadBase + 2f, _rb.velocity.y);
        else _rb.velocity = new Vector2(velocidadBase, _rb.velocity.y);

        // Casos por cada tecla

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Disparar();
            SoundManager.instance.Play("shoot");
        }
        if(Input.GetKeyDown(KeyCode.W) && saltosDisponibles > 0)
        {
            _anim.SetBool("isJumping", true);
            Saltar();
            SoundManager.instance.Play("jump");
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

        // Recuperaci�n de saltos al tocar el suelo
        if (enTierra && Time.time - horaUltimoSalto > cooldownRecuperacionSalto)
        {
            saltosDisponibles = saltosMaximos;
            _anim.SetBool("isJumping", false);
        }

        if(!enPantalla && horaFueraPantalla != 0) 
        {
            if (Time.time - horaFueraPantalla > 0.5f) GameManager.Instance.Muerte();
        }
    }
    private void Saltar()
    {
        _rb.velocity = Vector2.up * fuerzaSalto;
        saltosDisponibles--;
        horaUltimoSalto = Time.time;
    }
    public void EnPantalla(bool estado)
    {
        if (estado)
        {
            enPantalla = true;
            return;
        }
        enPantalla = false;
        horaFueraPantalla = Time.time;
    }
}
