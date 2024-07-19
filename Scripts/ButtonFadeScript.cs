using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonFadeScript : MonoBehaviour
{
    [SerializeField]
    private Button playbtn, levelbtn, ibtn, soundbtn;

    [SerializeField]
    private Image RedBeadimg;

    [SerializeField]
    private GameObject igPanel;

    void Start()
    {
        
        
            playbtn.GetComponent<CanvasGroup>().DOFade(1, 1.5f);
            levelbtn.GetComponent<CanvasGroup>().DOFade(1, 1.5f);
            ibtn.GetComponent<CanvasGroup>().DOFade(1, 1.5f);
            soundbtn.GetComponent<CanvasGroup>().DOFade(1, 1.5f);
            RedBeadimg.GetComponent<CanvasGroup>().DOFade(1, 1.5f);
        

    }

    public void LevelsSahnesiniAc()
    {
        SceneManager.LoadScene("LevelsMenu");
    }

    public void igPanelAc()
    {
        if (igPanel.GetComponent<CanvasGroup>().alpha == 0)
        {
            igPanel.GetComponent<CanvasGroup>().DOFade(1, 1.5f);
        } else
        {
            igPanel.GetComponent<CanvasGroup>().DOFade(0, 1.5f);
        }
    }

    public void level1()
    {
        SceneManager.LoadScene("Level1");
    }

    
}
