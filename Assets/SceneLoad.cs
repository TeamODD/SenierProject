using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoad : MonoBehaviour
{
    public Slider progressbar;
    public TextMeshProUGUI loadtext;
    public static string loadScene;
    public static int loadType;
    private void Start()
    {
        //loadtext = GetComponent<TextMeshProUGUI>(); //�� ��?
        StartCoroutine(LoadScene());
    }
    public static void LoadSceneHandle(string _name, int _loadType)
    {
        loadScene = _name;
        loadType = _loadType;
        SceneManager.LoadScene("Loading");
    }
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("Chat");
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            yield return null;

            //if (loadType == 0)
            //    Debug.Log("������");
            //else if (loadType == 1)
            //    Debug.Log("��������");

            if (progressbar.value < 1f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }
            
            if(progressbar.value >= 1f && operation.progress>=0.9f)
            {
                operation.allowSceneActivation = true;
            }        
        }
    }
}
