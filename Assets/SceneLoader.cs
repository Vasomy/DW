using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string gameScene;
    public GameObject mainMenuUI;
    
    public void GoToGame()
    {
        SceneManager.LoadSceneAsync(gameScene, LoadSceneMode.Additive);
        HideMainMenuUI();
    }

    private void HideMainMenuUI()
    {
        mainMenuUI.SetActive(false);
    }
}
