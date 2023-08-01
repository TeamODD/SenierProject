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
<<<<<<< Updated upstream
    public TextMeshProUGUI Soundtext;
    private void Start()
    {
        defaultScale = buttonScale.localScale;
=======
    public TextMeshProUGUI button_text;
    public Color text_defaultcolor;
    public TMP_FontAsset font;
    public TMP_FontAsset tmp_font;
    private void Start()
    {
        defaultScale = buttonScale.localScale;
        button_text = GetComponentInChildren<TextMeshProUGUI>();
        text_defaultcolor = button_text.color;
        font = button_text.font;
>>>>>>> Stashed changes
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
                Debug.Log("����");
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
                Debug.Log("����");
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
<<<<<<< Updated upstream
        buttonScale.localScale = defaultScale * 1.2f;
=======
        //buttonScale.localScale = defaultScale * 1.2f;
        button_text.color = new Color32(250, 226, 0, 255);
        button_text.font = tmp_font;
>>>>>>> Stashed changes
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
<<<<<<< Updated upstream
=======
        button_text.color = text_defaultcolor;
        button_text.font = font;
>>>>>>> Stashed changes
    }
}

