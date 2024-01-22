using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class Achievements : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private void Start()
    {
        AchievementUI("Coge 200 monedas:", GameManager.Instance.data.achieCoins, 200);
        AchievementUI("Elimina 100 enemigos:", GameManager.Instance.data.achieEnemies, 10);
        AchievementUI("Llega a 1.000 de Score:", GameManager.Instance.maxScore, 1000);
        AchievementUI("Atraviesa 200 muros:", GameManager.Instance.data.achieWalls, 10);
        AchievementUI("Muere 69 veces:", 4, 10);
        AchievementUI("Compra toda la tienda (no consumibles):", 5, 10);
    }
    private void AchievementUI(string text, float quantity, float maxQuantity)
    {
        float percent = quantity / maxQuantity;
        if (percent > 1) percent = 1;
        GameObject achievement = Instantiate(prefab, GetComponentsInChildren<Transform>().Where(item => item.name == "container").ToArray()[0]);
        achievement.GetComponentInChildren<TextMeshProUGUI>().text = $"{text} {quantity} / {maxQuantity}";
        achievement.GetComponentsInChildren<RectTransform>().Where(item => item.name == "Bar").ToArray()[0].position -= new Vector3(776 * (1 -(percent)), 0,0);
    }
}
