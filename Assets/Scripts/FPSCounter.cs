using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using System;

public class FPSCounter : MonoBehaviour
{
    private float count;

    private void OnDisable()
    {
        PlayerPrefs.SetInt("fpsCounter", 0);
        Debug.Log(Convert.ToBoolean(PlayerPrefs.GetInt("fpsCounter")));
    }
    private void OnEnable()
    {        
        Debug.Log(Convert.ToBoolean(PlayerPrefs.GetInt("fpsCounter")));
        this.enabled=Convert.ToBoolean(PlayerPrefs.GetInt("fpsCounter"));
        PlayerPrefs.SetInt("fpsCounter", 1);
    }
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