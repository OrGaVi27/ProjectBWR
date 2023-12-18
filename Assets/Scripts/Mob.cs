using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Entity
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] public GameObject projectilePrefab;
    public float jumpForce;   //Fuerza horizontal aplicada para simular un salto
    public float shootCooldown;   //Tiempo de espera entre disparos
    public float lastShootDate; //Variable utilizada en la comprobación del cooldown de los disparos

    void Start()
    {
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
                break;
            case "Red":
                _sr.color = Color.red;
                gameObject.tag = "Red";
                break;
            case "White":
                _sr.color = Color.white;
                gameObject.tag = "Untagged";
                break;
        }
    }
    public bool Disparar()
    {
        if (Time.time - lastShootDate > shootCooldown)
        {
            var proyectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            lastShootDate = Time.time;
            if (gameObject.name == "MC" && !GameManager.Instance.data.dontLoseColorAtShoot) ColorChange("White");
            if (gameObject.name == "MC" && GameManager.Instance.data.biggerBullets) proyectile.transform.localScale *= 2;
            return true;
        }
        return false;
    }
}
