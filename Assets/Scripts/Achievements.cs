using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private void Update()
    {
        AchievementUI("AchiCoins", "Coge 200 monedas:", GameManager.Instance.data.achieCoins, 200);
        AchievementUI("AchiEnemies", "Elimina 100 enemigos:", GameManager.Instance.data.achieEnemies, 100);
        AchievementUI("AchiScore", "Llega a 1.000 de Score:", GameManager.Instance.maxScore, 1000);
        AchievementUI("AchiWalls", "Atraviesa 200 muros:", GameManager.Instance.data.achieWalls, 200);
        AchievementUI("AchiDeaths", "Muere 69 veces:", GameManager.Instance.data.achieDeaths, 69);
        AchievementUI("AchiShop", "Compra todas las mejoras:", GameManager.Instance.data.AchieShop(false), GameManager.Instance.data.AchieShop(true));
    }
    private void AchievementUI(string name, string text, float quantity, float maxQuantity)
    {
        float percent = quantity / maxQuantity;
        if (percent > 1) percent = 1;

       if(GameObject.Find(name) != null)
        {
            var item = GameObject.Find(name);
            item.GetComponentInChildren<TextMeshProUGUI>().text = $"{text} {quantity} / {maxQuantity}";
            var bar = item.GetComponentsInChildren<RectTransform>().Where(item => item.name == "Bar").ToArray()[0];
            bar.localPosition = new Vector3(-198.0409f - 392 * (1 - percent), 0, 0);
            if (percent == 1) bar.GetComponent<Image>().color = Color.green;
            return;
        }

        GameObject achievement = Instantiate(prefab, GetComponentsInChildren<Transform>().Where(item => item.name == "container").ToArray()[0]);
        achievement.GetComponentInChildren<TextMeshProUGUI>().text = $"{text} {quantity} / {maxQuantity}";
        achievement.GetComponentsInChildren<RectTransform>().Where(item => item.name == "Bar").ToArray()[0].localPosition = new Vector3(-198.0409f - 392 * (1 - percent), 0,0);
        achievement.name = name;
        return;
    }
}
