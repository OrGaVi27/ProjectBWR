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
    public int saltosDisponibles = 0;   //Saltos disponibles en el momento
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
        layerSuelo = LayerMask.GetMask("Suelo");
    }


    private void Update()
    {
        enTierra = Physics2D.OverlapCircle(_trans.position, radioSuelo, layerSuelo); ;

        // Velocidad constante
        _rb.velocity = new Vector2(velocity, _rb.velocity.y);

        // Casos por cada tecla
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

        // Recuperación de saltos al tocar el suelo
        if (enTierra && Time.time - horaUltimoSalto > cooldownRecuperacionSalto)
        {
            saltosDisponibles = saltosMaximos;
        }
    }

    private void Disparar()
    {
        if (Time.time - horaUltimoDisparo > cooldownDisparo)
        {
            SoundManager.instance.Play("shoot");
            Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
            horaUltimoDisparo = Time.time;
            CambioColor("White");
        }
    }
    private void Saltar()
    {
        SoundManager.instance.Play("jump");
        _rb.velocity = Vector2.up * jumpForce;
        saltosDisponibles--;
        horaUltimoSalto = Time.time;
    }

    private void CambioColor(string color)
    {
        
        switch (color)
        {
            case "Blue":
                SoundManager.instance.Play("changeColor");
                _sr.color = Color.blue;
                gameObject.tag = "Blue";
                // Desactiva las colisiones entre la layer default y la layer Blue
                Physics2D.IgnoreLayerCollision(0, LayerMask.NameToLayer("Blue"), true);
                // Activa las colisiones entre la layer default y la layer Red
                Physics2D.IgnoreLayerCollision(0, LayerMask.NameToLayer("Red"), false);
                break;
            case "Red":
                SoundManager.instance.Play("changeColor");
                _sr.color = Color.red;
                gameObject.tag = "Red";
                // Activa las colisiones entre la layer default y la layer Blue
                Physics2D.IgnoreLayerCollision(0, LayerMask.NameToLayer("Blue"), false);
                // Desactiva las colisiones entre la layer default y la layer Red
                Physics2D.IgnoreLayerCollision(0, LayerMask.NameToLayer("Red"), true); 
                break;
            case "White":
                _sr.color = Color.white;
                gameObject.tag = "Untagged";
                // Activa las colisiones entre la layer default y las dos de colores (Blue y Red)
                Physics2D.IgnoreLayerCollision(0, LayerMask.NameToLayer("Blue"), false);
                Physics2D.IgnoreLayerCollision(0, LayerMask.NameToLayer("Red"), false);
                break;
        }
    }
}
