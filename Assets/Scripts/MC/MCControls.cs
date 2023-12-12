using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Controls : Mob
{
    public float baseSpeed = 8.0f;  //Velocidad lateral base
    private float cooldownJumpRecover = 0.1f; //Tiempo de espera m�nimo para que el salto no se recupere antes de que el objeto se levante del suelo
    private float lastJumpDate;  //Variable utilizada en la comprobaci�n del cooldown de recuperacion de los saltos
    private bool onGround;  //Almacena el resultado de la comprobaci�n del contacto del personaje con el suelo
    private float radiusGround = 1;   //Variable usada en la formula de la comprobaci�n "enTierra"
    private LayerMask layerGround;   //Almacena la capa del Suelo
    private int availableJumps;   //Saltos disponibles en el momento
    private int maxJumps = 1;  //Saltos que se asignaran a saltosDisponibles en cuanto el MC repose en el suelo
    private bool onScreen; //Se modifica seg�n el MC entre o salga de pantalla.
    private float OutScreenDate; //Registra el momento en el que sale de pantalla.
    public bool delayed;
    [SerializeField] private GameObject shield;
    private bool invulnerability;
    private float lastHitDate;
    private float invulnerabilityDuration;

    private void Start()
    {
        shootCooldown = 0.5f;
        jumpForce = 14.0f;
        _rb.gravityScale = 4f;
        lastShootDate = Time.time - 1f;
        lastJumpDate = Time.time;
        layerGround = LayerMask.GetMask("Floor");
        delayed = true;
        availableJumps = maxJumps;
        invulnerability = false;
        invulnerabilityDuration = 1f;
        if (GameManager.Instance.data.iFramePostHit) invulnerabilityDuration = 2f;
        lastHitDate = 0;
    }


    private void Update()
    {
        if (Time.time - lastHitDate > invulnerabilityDuration) invulnerability = false;

        if (invulnerability) _sr.color = Color.green;
        else
        {
            switch (tag)
            {
                case "Red":
                    _sr.color = Color.red;
                    break;
                case "Blue":
                    _sr.color = Color.blue;
                    break;
                case "Untagged":
                    _sr.color = Color.white;
                    break;
            }
        }

        if (GameManager.Instance.data.shields > 0) shield.transform.localScale = Vector3.one;
        else shield.transform.localScale = Vector3.zero;

        onGround = Physics2D.OverlapCircle(_trans.position, radiusGround, layerGround); ;

        // Velocidad constante
        if(delayed) _rb.velocity = new Vector2(baseSpeed + 2f, _rb.velocity.y);
        else _rb.velocity = new Vector2(baseSpeed, _rb.velocity.y);

        // Casos por cada tecla

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Disparar();
            SoundManager.instance.Play("shoot");
        }
        if(Input.GetKeyDown(KeyCode.W) && availableJumps > 0)
        {
            _anim.SetBool("isJumping", true);
            Jump();
            SoundManager.instance.Play("jump");
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            ColorChange("Blue");
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            ColorChange("Red");
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
        if (onGround && Time.time - lastJumpDate > cooldownJumpRecover)
        {
            availableJumps = maxJumps;
            _anim.SetBool("isJumping", false);
        }

        if(!onScreen && OutScreenDate != 0) 
        {
            if (Time.time - OutScreenDate > 0.5f) GameManager.Instance.Death();
        }
    }
    private void Jump()
    {
        _rb.velocity = Vector2.up * jumpForce;
        availableJumps--;
        lastJumpDate = Time.time;
    }
    public void OnScreen(bool estado)
    {
        if (estado)
        {
            onScreen = true;
            return;
        }
        onScreen = false;
        OutScreenDate = Time.time;
    }
    public void Hit()
    {
        if (!invulnerability)
        {
            if (GameManager.Instance.data.shields > 0) GameManager.Instance.data.shields--;
            else GameManager.Instance.Death();
            lastHitDate = Time.time;
            invulnerability = true;
        }
    }
}
