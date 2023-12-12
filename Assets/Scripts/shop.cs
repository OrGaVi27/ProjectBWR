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
        var data = GameManager.Instance.data;
        EditText(0, $"Shields: {data.shields}");
        EditText(1, $"ExtraJumps: {data.extraJumps}");
    }
    private void EditText(int index, string text)
    {
        buttonText[index].GetComponent<TextMeshProUGUI>().text = text;
    }
}
