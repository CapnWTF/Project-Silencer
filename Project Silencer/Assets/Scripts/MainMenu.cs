using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public GUISkin skin;
    private void OnGUI()
    {
        GUI.skin = skin;
        GUI.Label(new Rect(10, 10, 400, 50), "SlideBox");
        if (PlayerPrefs.GetInt("Level Completed") > 0)
        {
            if (GUI.Button(new Rect(10, 150, 100, 50), "Continue Run"))
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("Level Completed"));
            }
        }
        if (GUI.Button(new Rect(10, 210, 100, 50), "New Run"))
        {
            PlayerPrefs.SetInt("Level Completed", 0);
            SceneManager.LoadScene(0);
            
        }

        if (GUI.Button(new Rect(10, 270, 100, 50), "Quit"))
        {
            Application.Quit();
        }
    }
    
}
