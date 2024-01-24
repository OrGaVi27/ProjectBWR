using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Controls : Mob
{
    public float baseSpeed;  //Velocidad lateral base.
    private bool onGround;  //Almacena el resultado de la comprobacion del contacto del personaje con el suelo.
    public int availableJumps;   //Saltos disponibles en el momento.
    public int maxJumps;  //Saltos que se asignaran a saltosDisponibles en cuanto el MC repose en el suelo.
    private bool onScreen; //Se modifica seg�n el MC entre o salga de pantalla.
    private float OutScreenDate; //Registra el momento en el que sale de pantalla.
    public bool delayed; //Comprueba si el jugador está más atras de su posición predefinida.
    [SerializeField] private Color marioStarColor;

    // Objetos que tiene el jugador para mostrar el escudo y el duplicador de monedas (gafas) respectivamente.
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject shieldText;
    [SerializeField] private GameObject glasses;

    // Variables para invulnerabilidad por golpe.
    private bool invulnerability;
    private float lastHitDate;
    private bool invulnerabilityItem;

    // Variables para invulnerabilidad por objeto.
    private float invulnerabilityDuration;
    private float invulnerabilityItemStart;
    private float invulnerabilityItemDuration;

    // Variables para el consumible de monedas dobles.
    private float doubleCoinsDuration;
    private float doubleCoinsActivationDate;

    // Variables para el Cambio de Color.
    private float lastColorChange;
    private float cooldownColorChange;

    // Contador de escudos usados por intento.
    private float shieldUsed;

    // Variable publica para comprobar si el duplicador de monedas está activo des de el CollisionManager.
    public bool doubleCoins;

    private void Start()
    {
        // Estadisticas base
        shootCooldown = 0.5f;
        jumpForce = 14.0f;
        _rb.gravityScale = 4f;
        cooldownColorChange = 0.5f - GameManager.Instance.data.lessCooldownColorChange * 0.25f;
        baseSpeed = 8.0f;
        maxJumps = 1 + GameManager.Instance.data.extraJumps;
        doubleCoinsDuration = 30f;
        invulnerabilityItemDuration = 5f;

        // Variables para los cooldowns puestas a 0.
        lastShootDate = 0;
        lastHitDate = 0;
        lastColorChange = 0;
        doubleCoinsActivationDate = 0;
        invulnerabilityItemStart = 0;

        // Otros
        doubleCoins = false;
        shieldUsed = 0;
        delayed = true;
        availableJumps = maxJumps;
        invulnerability = false;
        invulnerabilityDuration = 1f + 0.5f * GameManager.Instance.data.longerInvulnerability;
        if (GameManager.Instance.data.iFramePostHit) invulnerabilityDuration = 2f;
    }


    private void Update()
    {
        if (Time.time - lastHitDate > invulnerabilityDuration && Time.time - invulnerabilityItemStart > invulnerabilityItemDuration)
        {
            invulnerability = false;
            invulnerabilityItem = false;
        }
        if (Time.time - doubleCoinsActivationDate > doubleCoinsDuration)
        {
            doubleCoins = false;
            glasses.SetActive(false);
        }

        if (invulnerability) _sr.color = Color.green;
        else if (invulnerabilityItem) _sr.color = marioStarColor;
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

        if (GameManager.Instance.data.shields > 0 && shieldUsed < 3) shield.SetActive(true); 
        else shield.SetActive(false);
        shieldText.GetComponent<TextMeshPro>().text = $"{3 - shieldUsed}";


        // Velocidad constante
        if (delayed) _rb.velocity = new Vector2(baseSpeed + 2f, _rb.velocity.y);
        else _rb.velocity = new Vector2(baseSpeed, _rb.velocity.y);

        // Controlador de teclas

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameManager.Instance.data.biggerBullets) projectilePrefab.transform.localScale = Vector3.one * 2;
            Disparar();
            SoundManager.instance.Play("shoot");
        }
        if(Input.GetKeyDown(KeyCode.W) && availableJumps > 0)
        {
            Jump();
            SoundManager.instance.Play("jump");
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            if(Time.time - lastColorChange > cooldownColorChange && !CompareTag("Blue"))
            {
                ColorChange("Blue");
                lastColorChange = Time.time;
            }
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time - lastColorChange > cooldownColorChange && !CompareTag("Red"))
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
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!doubleCoins && GameManager.Instance.data.doubleCoinsAtCollect > 0)
            {
                doubleCoins = true;
                glasses.SetActive(true);
                GameManager.Instance.data.doubleCoinsAtCollect--;
                doubleCoinsActivationDate = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!invulnerability && !invulnerabilityItem && GameManager.Instance.data.marioStar > 0)
            {
                invulnerabilityItem = true;
                GameManager.Instance.data.marioStar--;
                invulnerabilityItemStart = Time.time;
            }
        }

        // Comprueba el tiempo que lleva el jugador fuera de pantalla y hace que muera pasado un tiempo anteriormente determinado
        if(!onScreen && OutScreenDate != 0) 
        {
            if (Time.time - OutScreenDate > 0.5f) GameManager.Instance.Death();
        }

        if (GameManager.Instance.isDead >= 0)
        {
            _rb.velocity = Vector2.zero;
            _sr.color = Color.white;
            if (TryGetComponent(out BoxCollider2D box)) box.enabled = false;
            if(_rb.gravityScale != 0) _rb.gravityScale = 0;
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
        if (!invulnerability && !invulnerabilityItem && GameManager.Instance.isDead == -1)
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
