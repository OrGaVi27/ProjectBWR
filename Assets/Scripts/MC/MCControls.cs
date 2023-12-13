using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Controls : Mob
{
    public float baseSpeed;  //Velocidad lateral base
    private bool onGround;  //Almacena el resultado de la comprobacion del contacto del personaje con el suelo
    public int availableJumps;   //Saltos disponibles en el momento
    private int maxJumps;  //Saltos que se asignaran a saltosDisponibles en cuanto el MC repose en el suelo
    private bool onScreen; //Se modifica seg�n el MC entre o salga de pantalla.
    private float OutScreenDate; //Registra el momento en el que sale de pantalla.
    public bool delayed;
    [SerializeField] private GameObject shield;
    private bool invulnerability;
    private float lastHitDate;
    private float invulnerabilityDuration;
    private float lastColorChange;
    private float cooldownColorChange;
    private float shieldUsed;

    private void Start()
    {
        // Estadisticas base
        shootCooldown = 0.5f;
        jumpForce = 14.0f;
        _rb.gravityScale = 4f;
        cooldownColorChange = 1f - GameManager.Instance.data.lessCooldownColorChange * 0.5f;
        baseSpeed = 8.0f;
        maxJumps = 1 + GameManager.Instance.data.extraJumps;

        // Variables para los cooldowns puestas a 0
        lastShootDate = 0;
        lastHitDate = 0;
        lastColorChange = 0;

        // Otros
        shieldUsed = 0;
        delayed = true;
        availableJumps = maxJumps;
        invulnerability = false;
        invulnerabilityDuration = 1f;
        if (GameManager.Instance.data.iFramePostHit) invulnerabilityDuration = 2f;
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

        if (GameManager.Instance.data.shields > 0 && shieldUsed < 3) shield.transform.localScale = Vector3.one;
        else shield.transform.localScale = Vector3.zero;

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
            if(Time.time - lastColorChange > cooldownColorChange)
            {
                ColorChange("Blue");
                lastColorChange = Time.time;
            }
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time - lastColorChange > cooldownColorChange)
            {
                ColorChange("Red");
                lastColorChange = Time.time;
            }
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            _anim.SetBool("isCrouching", true);
            SoundManager.instance.Play("slide");
            if(!onGround)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y - 10);
            }
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            _anim.SetBool("isCrouching", false);
            SoundManager.instance.Stop("slide");
        }

        // Recuperacion de saltos al tocar el suelo
        foreach (RaycastHit2D rc in Physics2D.RaycastAll(_trans.position, Vector2.down))
        {
            if (rc.collider.gameObject.layer == LayerMask.NameToLayer("Floor") && rc.distance < 0.6)
            {
                availableJumps = maxJumps;
                _anim.SetBool("isJumping", false);
            }
        }

        if(!onScreen && OutScreenDate != 0) 
        {
            if (Time.time - OutScreenDate > 0.5f) GameManager.Instance.Death();
        }
    }
    private void Jump()
    {
        _rb.velocity = Vector2.up * jumpForce;
        if (_anim.GetBool("isJumping")) availableJumps--;
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
            if (GameManager.Instance.data.shields > 0 && shieldUsed < 3)
            {
                GameManager.Instance.data.shields--;
                shieldUsed++;
            }
            else GameManager.Instance.Death();
            lastHitDate = Time.time;
            invulnerability = true;
        }
    }
}
