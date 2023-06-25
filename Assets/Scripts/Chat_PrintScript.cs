using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat_PrintScript : MonoBehaviour
{
    public GameObject next;

    private Text text;
    private string test_text = "씨이... 나 혼자 버리고 가지 말라고,\n몇 번이나 말해...!!!";
    private int pos = 0;

    private IEnumerator chatCoroutine;

    // Start is called before the first frame update
    void Start()
    {

        text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (next.activeSelf)
            {
                pos = 0;
                text.text = "";
                next.SetActive(false);
                chatCoroutine = chatDelay(0.1f);
                StartCoroutine(chatCoroutine);
            }
            else
            {
                StopCoroutine(chatCoroutine);
                text.text = test_text;
                next.SetActive(true);
            }

        }
    }

    IEnumerator chatDelay(float time)
    {
        while (pos < test_text.Length)
        {
            text.text += test_text.Substring(pos, 1);
            pos++;
            yield return new WaitForSeconds(time);
        }
        next.SetActive(true);
        yield break;
    }
}
