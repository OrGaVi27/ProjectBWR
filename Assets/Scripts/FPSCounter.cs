using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class FPSCounter : MonoBehaviour
{
    private float count;

    private IEnumerator Start()
    {
        GUI.depth = 2;
        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.richText = true;
        style.normal.textColor = Color.yellow;
        GUI.Label(new Rect(5, 40, 400, 100), "FPS: " + Mathf.Round(count), style);
    }
}