using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuLevels : MonoBehaviour
{
    
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(LoadLevel);
        }
        else
        {
            Debug.LogWarning("Button component not found on the GameObject.");
        }
    }


    public void LoadLevel()
    {
        // Butonun içindeki metni al
        string buttonText = button.GetComponentInChildren<Text>().text;

        // Buton metnini seviye numarasýna dönüþtür
        int levelNumber;
        if (int.TryParse(buttonText, out levelNumber))
        {
            // Seviye adýný oluþtur
            string levelName = "Level" + levelNumber;

            // Ýlgili seviyeyi yükle
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.LogWarning("Button text is not a valid level number: " + buttonText);
        }

    }


}
