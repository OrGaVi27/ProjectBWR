using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private void Start()
    {
        AchievementUI("Polla 1", 1, 10, 0);
        AchievementUI("Polla 2", 1, 10, 1);
        AchievementUI("Polla 3", 1, 10, 2);
        AchievementUI("Polla 4", 1, 10, 3);
    }
    private void AchievementUI(string text, float quantity, float maxQuantity, int index)
    {
        GameObject achievement = Instantiate(prefab, transform);
        achievement.transform.position = new Vector3(achievement.transform.position.x, achievement.transform.position.y - 200 * index, 0);
        achievement.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}
