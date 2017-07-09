using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public int levelToLoad;
    private string loadPrompt;
    private bool inRange;
    private bool canLoadLevel;
    private  int completedLevel;

    //padlock icon
    public GameObject padlock;
     Vector3 lockPos ;

    private void Start()
    {
        completedLevel = PlayerPrefs.GetInt("Level Completed");

        if (completedLevel == 0)
        {
            completedLevel = 1; //if the level is ever 0, set it to 1, this is because there is no level 0 and trying to load it will cause an error.
        }

        if (levelToLoad <= completedLevel)
            {
            canLoadLevel = true;
            }
        else
        {
            canLoadLevel = false;
        }

       

        if(!canLoadLevel)
        {
            Instantiate(padlock, new Vector3(transform.position.x + 2f, 0f, transform.position.z),Quaternion.Euler(0,90,0));
        }
    }

    private void Update()
    {
        if (canLoadLevel && Input.GetButtonDown("Action") && inRange  )
        {
            SceneManager.LoadScene("level_0" + levelToLoad.ToString());

        }
    }

    void OnTriggerStay(Collider other)
    {
        inRange = true;
        if (canLoadLevel)
        {
            loadPrompt = "[E] to load level 0" + levelToLoad.ToString();
        }
        else
        {
            loadPrompt = "Level 0" + levelToLoad.ToString() + " is locked";
        }
     }
    void OnTriggerExit()
    {
        loadPrompt = "";
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(30,Screen.height*.9f,200,40),loadPrompt);
    }

    
}
