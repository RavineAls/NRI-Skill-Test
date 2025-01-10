using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // load the MainMenu scene
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //load the Guide scene
    public void LoadGuide()
    {
        SceneManager.LoadScene("GuideMenu");
    }

    // load the BattleScene scene
    public void LoadBattleScene()
    {
        SceneManager.LoadScene("BattleScene");
    }

    // close the game
    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
