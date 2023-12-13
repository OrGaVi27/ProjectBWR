using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class shop : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttonText;

    private void Update()
    {
        EditText(0, $"Shields: {GameManager.Instance.data.shields}\n 5 Coins");
        EditText(1, $"ExtraJumps: {GameManager.Instance.data.extraJumps}\n 5 Coins");
    }
    private void EditText(int index, string text)
    {
        buttonText[index].GetComponent<TextMeshProUGUI>().text = text;
    }
}
