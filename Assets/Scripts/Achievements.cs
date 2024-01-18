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
        AchievementUI("Logro: 1", 0, 10);
        AchievementUI("Logro: 2", 1, 10);
        AchievementUI("Logro: 3", 2, 10);
        AchievementUI("Logro: 4", 3, 10);
        AchievementUI("Logro: 5", 4, 10);
        AchievementUI("Logro: 6", 5, 10);
        AchievementUI("Logro: 7", 6, 10);
        AchievementUI("Logro: 8", 7, 10);
        AchievementUI("Logro: 9", 8, 10);
        AchievementUI("Logro: 10", 9, 10);
        AchievementUI("Logro: 11", 10, 10);
    }
    private void AchievementUI(string text, float quantity, float maxQuantity)
    {
        GameObject achievement = Instantiate(prefab, GetComponentsInChildren<Transform>().Where(item => item.name == "container").ToArray()[0]);
        achievement.GetComponentInChildren<TextMeshProUGUI>().text = text;
        achievement.GetComponentsInChildren<RectTransform>().Where(item => item.name == "Bar").ToArray()[0].position -= new Vector3(776 * (1 -(quantity / maxQuantity)), 0,0);
    }
}
