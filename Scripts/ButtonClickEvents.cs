using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelsSahnesiniAc()
    {
        SceneManager.LoadScene("LevelsMenu");
    }

    public void AnaMenuyeDon()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void YenidenOyna()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void LoadNextLevel()
    {
        // Þu anki sahnenin adýný al
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Level numarasýný bul
        int currentLevel = int.Parse(currentSceneName.Replace("Level", ""));

        // Bir sonraki levelin adýný oluþtur
        string nextLevelName = "Level" + (currentLevel + 1);

        // Bir sonraki leveli yükle
        SceneManager.LoadScene(nextLevelName);
    }
}
