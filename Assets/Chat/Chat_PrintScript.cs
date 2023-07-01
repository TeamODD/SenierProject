using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 코드 정리
public class Chat_PrintScript : MonoBehaviour
{
    [SerializeField] GameObject next;
    [SerializeField] Image image;
    [SerializeField] Text author;
    [SerializeField] Text script;

    [SerializeField] string eventName;
    [SerializeField] ChatData[] datas;

    private int chatPos = 0;
    private int textPos = 0;

    private IEnumerator chatCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        datas = Chat_ParseScript.GetScript(eventName);
        chatPos = 0;
        startChat();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {

            if (next.activeSelf)
            {
                if (chatPos == datas.Length)
                {
                    // 대화 끝남
                    return;
                }
                startChat();
            }
            else
            {
                script.text = datas[chatPos].script;
                chatPos++;
                StopCoroutine(chatCoroutine);
                next.SetActive(true);
            }

        }
    }

    IEnumerator chatDelay(float time, ChatData[] datas)
    {
        
        while (textPos < datas[chatPos].script.Length)
        {
            script.text += datas[chatPos].script.Substring(textPos, 1);
            textPos++;
            yield return new WaitForSeconds(time);
        }
        chatPos++;
        next.SetActive(true);
        yield break;
    }

    void startChat()
    {
        textPos = 0;
        author.text = datas[chatPos].name;
        script.text = "";

        if (datas[chatPos].filename.Trim() != "")
        {
            Sprite s = Resources.Load<Sprite>("파일 이름");
            image.sprite = s;
        }
        next.SetActive(false);

        chatCoroutine = chatDelay(0.1f, datas);
        StartCoroutine(chatCoroutine);
    }

    string checkFile(string file)
    {
        string filepath = "Sprites/";
        switch (file)
        {
            case "주인공 (긴장함)":
                filepath += "파일 이름";
                break;
            case "주인공 (놀람)":
                filepath += "파일 이름";
                break;
            default:
                filepath += "기본 파일";
                break;
        }
        return filepath;
    }
}
