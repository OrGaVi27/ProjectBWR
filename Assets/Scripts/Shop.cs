using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<GameObject> shopButtons;
    void Start()
    {
        foreach (var item in GetComponentsInChildren<Transform>())
        {
            if (item.name.Length >= 6 && item.name[..6] == "Button")
            {
                shopButtons.Add(item.gameObject);
            }
        }
    }
    private void Update()
    {

        EditText(0, $"Shields: {GameManager.Instance.data.shields}\n 5 Coins");
        EditText(1, $"ExtraJumps: {GameManager.Instance.data.extraJumps}\n 100 Coins");
        EditText(2, $"Less Color Cooldown: {GameManager.Instance.data.lessCooldownColorChange}\n 50 Coins");
        EditText(3, $"Don't lose Color: {GameManager.Instance.data.dontLoseColorAtShoot}\n 125 Coins");
        EditText(4, $"Piercing Bullets: {GameManager.Instance.data.bulletPenetration}\n 50 Coins");
        EditText(5, $"Longer Invulnerability: {GameManager.Instance.data.longerInvulnerability}\n 50 Coins");
        EditText(6, $"Bigger Bullets: {GameManager.Instance.data.biggerBullets}\n 75 Coins");
        EditText(7, $"Double Coins (Consum): {GameManager.Instance.data.doubleCoinsAtCollect}\n 12 Coins");
        EditText(8, $"Invulnerability (Consum): {GameManager.Instance.data.marioStar}\n 15 Coins");
    }
    private void EditText(int index, string text)
    {
        shopButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}
