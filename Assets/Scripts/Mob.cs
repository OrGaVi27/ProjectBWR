using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Entidad
{
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private GameObject proyectilPrefab;
    public float fuerzaSalto;   //Fuerza horizontal aplicada para simular un salto
    public float cooldownDisparo;   //Tiempo de espera entre disparos
    public float horaUltimoDisparo; //Variable utilizada en la comprobación del cooldown de los disparos

    void Start()
    {
        DefinirEntidad();
        horaUltimoDisparo = Time.time - 1f;
    }
    public void CambioColor(string color)
    {
        SoundManager.instance.Play("changeColor");
        switch (color)
        {
            case "Blue":
                _sr.color = Color.blue;
                gameObject.tag = "Blue";
                // Desactiva las colisiones entre la layer default y la layer Blue
                Physics2D.IgnoreLayerCollision(0, LayerMask.NameToLayer("Blue"), true);
                // Activa las colisiones entre la layer default y la layer Red
                Physics2D.IgnoreLayerCollision(0, LayerMask.NameToLayer("Red"), false);
                break;
            case "Red":
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
    public void Disparar()
    {
        if (Time.time - horaUltimoDisparo > cooldownDisparo)
        {
            Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);
            horaUltimoDisparo = Time.time;
            CambioColor("White");
        }
    }
}
