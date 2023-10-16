using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Controles : MonoBehaviour
{
    Animator _anim;
    Rigidbody2D _rb;
    SpriteRenderer _sr;
    Transform _trans;

    [SerializeField] private GameObject balaPrefab;
    [SerializeField] private Transform puntoDisparo;

    private float jumpForce = 14.0f; //Fuerza horizontal aplicada para simular un salto
    private float velocity = 8.0f;  //Velocidad lateral base
    private float cooldownDisparo = 0.5f; //Tiempo de espera entre disparos
    private float cooldownRecuperacionSalto = 0.1f; //Tiempo de espera mínimo para que el salto no se recupere antes de que el objeto se levante del suelo
    private float horaUltimoDisparo;    //Variable utilizada en la comprobación del cooldown de los disparos
    private float horaUltimoSalto;  //Variable utilizada en la comprobación del cooldown de recuperacion de los saltos
    private bool enTierra;  //Almacena el resultado de la comprobación del contacto del personaje con el suelo
    private float radioSuelo = 1;   //Variable usada en la formula de la comprobación "enTierra"
    private LayerMask layerSuelo;   //Almacena la capa del Suelo
    public int saltosDisponibles;   //Saltos disponibles en el momento
    private int saltosMaximos = 1;  //Saltos que se asignaran a saltosDisponibles en cuanto el MC repose en el suelo
    public bool agachado = false;

    // Start is called before the first frame update
    private void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _trans = GetComponent<Transform>();

        _rb.gravityScale = 3;
        horaUltimoDisparo = Time.time - 1f;
        horaUltimoSalto = Time.time;
        saltosDisponibles = saltosMaximos;
        layerSuelo = LayerMask.GetMask("Suelo");
    }

    // Update is called once per frame
    private void Update()
    {
        enTierra = Physics2D.OverlapCircle(_trans.position, radioSuelo, layerSuelo); ;

        _rb.velocity = new Vector2(velocity, _rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Disparar();
        }
        if(Input.GetKeyDown(KeyCode.W) && saltosDisponibles > 0)
        {
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
            agachado = true;
            _trans.localScale = new Vector3( 1, 0.5f, 1);
            _trans.position = new Vector3(_trans.position.x, _trans.position.y - 0.25f, _trans.position.z);
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            agachado = false;
            _trans.localScale = new Vector3(1, 1, 1);
            _trans.position = new Vector3(_trans.position.x, _trans.position.y + 0.25f, _trans.position.z);
        }

        if (enTierra && Time.time - horaUltimoSalto > cooldownRecuperacionSalto)
        {
            saltosDisponibles = saltosMaximos;
        }
    }

    private void Disparar()
    {
        if (Time.time - horaUltimoDisparo > cooldownDisparo)
        {
            Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
            horaUltimoDisparo = Time.time;
            CambioColor("White");
        }
    }
    private void Saltar()
    {
        _rb.velocity = Vector2.up * jumpForce;
        saltosDisponibles--;
        horaUltimoSalto = Time.time;
    }

    private void CambioColor(string color)
    {
        
        switch(color)
        {
            case "Blue":
                _sr.color = Color.blue;
                break;
            case "Red":
                _sr.color = Color.red;
                break;
            case "White":
                _sr.color = Color.white;
                break;
        }
    }
}
