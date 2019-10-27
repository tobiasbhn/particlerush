using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour {

    //Start Game-Start-Routine on load
    void Start() {
        Debug.Log(LogTime.Time() + ": Game Starter Script - Starting Game...");
        StartCoroutine(CheckPrestartConditions());
    }

    //Save Data on Application-Quit
    private void OnApplicationQuit() {
        Debug.Log(LogTime.Time() + ": Game Starter Script - Game is going to Quit. Saving Data...");
        SaveDataManager.Save();
        Debug.Log(LogTime.Time() + ": Game Starter Script - Data save. Have a nice Day...");
    }

    //Defined Start-Routine
    IEnumerator CheckPrestartConditions() {
        //Check if all Saved Data is collected
        Debug.Log(LogTime.Time() + ": Game Starter Script - Loading saved Data...");
        SaveDataManager.Load();
        while (!SaveDataManager.firstDataLoaded) {
            yield return new WaitForEndOfFrame();
        }
        Debug.Log(LogTime.Time() + ": Game Starter Script - All saved Data loaded...");

        //Set Language
        Debug.Log(LogTime.Time() + ": Game Starter Script - Going to apply current Language...");
        LanguageScript.UpdateLanguage();
        Debug.Log(LogTime.Time() + ": Game Starter Script - Language Set...");    

        //Call SceneManager to load whatever needs to be loaded
        Debug.Log(LogTime.Time() + ": Game Starter Script - Calling Scene Manager to load first Scene...");
        SceneManager.instance.startGame();
    }
}
