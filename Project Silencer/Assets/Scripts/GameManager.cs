using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    //count
    public int currentScore;
    public int hiScore;

    public int tokenCount;
    private int totalTokenCount;
    public static int currentLevel = 0;
    public int unlockedLevel;
    
    //timer
    public Rect timerRect;
    public float startTime;
    private string currentTime;
    public Color warningColorTimer;
    public Color defaultColorTimer;
    
    //GUI Skin
    public GUISkin skin;
    public int winScreenWidth = 200;
    public int winScreenHeight = 250;

    //References
    public GameObject tokenParent;
    private bool completed = false;
    private bool showWinScreen = false;

     void Update()
    {
        if (!completed)
        {
            startTime -= Time.deltaTime;
            currentTime = string.Format("{0:0.0}", startTime);
            //print(currentTime);
            //print("This level is: " + currentLevel);

            if (startTime <= 0)
            {
                startTime = 0;
                Destroy(gameObject);
                SceneManager.LoadScene("Main_Menu");
                print("Time's Up!");
            }
        }
    }

    void Start()
    {
        totalTokenCount = tokenParent.transform.childCount;

        if (PlayerPrefs.GetInt("Level Completed") > 1)
        {
            currentLevel = PlayerPrefs.GetInt("Level Completed");
            print("currentLevel: " + currentLevel);

        }
        else
        {
            currentLevel = 1;
        }
            //DontDestroyOnLoad(gameObject); 
    }

    //things done on level complete
    public void completeLevel()
    {
        showWinScreen = true;
        completed = true;
    }

    //loads the next level
    void LoadNextLevel()
    {
        //resets time to normal after winning the level freezes it
        Time.timeScale = 1f;

        if (currentLevel < 3)
        {
            currentLevel += 1;
            SaveGame();
            SceneManager.LoadScene(currentLevel);
        }
        else
        {
            print("You... Did It!");
            PlayerPrefs.SetInt("Level Completed", 0);
        }
    }

    void SaveGame()
    {
        PlayerPrefs.SetInt("Level Completed", currentLevel);
        PlayerPrefs.SetInt("Level " + currentLevel.ToString() + " score", currentScore);

    }

    public void AddToken()
    {
        tokenCount += 1;
    }

    private void OnGUI()
    {
        GUI.skin = skin;
        if(startTime <5f)
        {
            skin.GetStyle("Timer").normal.textColor = warningColorTimer;    
        }
        else
        {
            skin.GetStyle("Timer").normal.textColor = defaultColorTimer;
        }

        GUI.Label(timerRect, currentTime, skin.GetStyle("Timer"));
        GUI.Label(new Rect(45,100,200,200), tokenCount.ToString() + "/" + totalTokenCount.ToString(), skin.GetStyle("Token"));
        
        if (showWinScreen)
        {
            //rectangle containing the Win Screen
            Rect winScreenRect = new Rect(Screen.width/2 -(Screen.width*.5f/2),Screen.height/2-(Screen.height*.5f/2), Screen.width*.5f,Screen.height*.5f);
            GUI.Box(winScreenRect, "Yeah");

            //Score calculator
            int gameTime = (int)startTime;
            currentScore = tokenCount * gameTime;

            //Win Screen Buttons// 
           if( GUI.Button(new Rect(winScreenRect.x + winScreenRect.width-170,winScreenRect.y + winScreenRect.height-60,150,40), "Continue"))
                {
                LoadNextLevel();
                }

            if (GUI.Button(new Rect(winScreenRect.x + 20, winScreenRect.y + winScreenRect.height - 60, 100, 40), "quit"))
            {
                SceneManager.LoadScene("Main_Menu");
                Time.timeScale = 1f;
            }

            //shows score
            GUI.Label (new Rect(winScreenRect.x + 20,winScreenRect.y + 40, 300,50), currentScore.ToString() + " POINTO!");
            GUI.Label(new Rect(winScreenRect.x + 20, winScreenRect.y + 70, 300, 50), "You beat Level " + currentLevel +"!");


        }
    }
}
