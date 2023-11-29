using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Entity
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject projectilePrefab;
    public float jumpForce;   //Fuerza horizontal aplicada para simular un salto
    public float shootCooldown;   //Tiempo de espera entre disparos
    public float lastShootDate; //Variable utilizada en la comprobación del cooldown de los disparos

    void Start()
    {
        DefineEntity();
        lastShootDate = Time.time - 1f;
    }
    public void ColorChange(string color)
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
    public bool Disparar()
    {
        if (Time.time - lastShootDate > shootCooldown)
        {
            Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            lastShootDate = Time.time;
            if (gameObject.name == "MC") ColorChange("White");
            return true;
        }
        return false;
    }
}
