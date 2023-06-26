using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 코드 정리
public class Chat_PrintScript : MonoBehaviour
{

    [SerializeField] GameObject next;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChatData[] datas = Chat_ParseScript.GetScript(eventName);

            if (next.activeSelf)
            {
                if (chatPos == datas.Length) return;

                textPos = 0;
                author.text = datas[chatPos].name;
                script.text = "";
                next.SetActive(false);

                chatCoroutine = chatDelay(0.1f, datas);
                StartCoroutine(chatCoroutine);
            }
            else
            {
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
}
