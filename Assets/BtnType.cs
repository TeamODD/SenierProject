using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class BtnType : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public BTNType currentType;
    public Transform buttonScale;
    Vector3 defaultScale;
    public static bool isSound = true;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    public TextMeshProUGUI Soundtext;
    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }
    
    public void MainOnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.New:
                SceneLoad.LoadSceneHandle("Play", 0);
                break;
            case BTNType.Continue:
                SceneLoad.LoadSceneHandle("Play", 1);
                break;
            case BTNType.Option:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                break;
            case BTNType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                break;
            case BTNType.Quit:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
                PlayerPrefs.SetFloat("MusicVolume", 0.75f);
                Debug.Log("종료");
                break;
        }
    }

    public void PlayOnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.Option:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                break;
            case BTNType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                break;
            case BTNType.Quit:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
                PlayerPrefs.SetFloat("MusicVolume", 0.75f);
                Debug.Log("종료");
                break;
            case BTNType.Title:
                SceneManager.LoadScene("Main");
                break;
        }
    }

    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}

