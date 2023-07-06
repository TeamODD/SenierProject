using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat_PrintScript : MonoBehaviour
{
	[SerializeField] GameObject next;
	[SerializeField] Image image;
	[SerializeField] Text author;
	[SerializeField] Text script;
	[SerializeField] Image select1;
	[SerializeField] Image select2;
	[SerializeField] Text select1Text;
	[SerializeField] Text select2Text;

	[SerializeField] string eventName;

	// for debugging
	[SerializeField] ChatData[] datas;
	[SerializeField] int selectedItem = 0;

    [SerializeField] private int chatPos = 0;
	private int textPos = 0;
	private bool selection = false;

	private IEnumerator chatCoroutine;

	// Start is called before the first frame update
	void Start()
	{
		datas = Chat_ParseScript.GetScript(eventName);
		select1.color = new Color(255, 255, 255, 0);
		select2.color = new Color(255, 255, 255, 0);
		chatPos = 0;
		startChat();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
		{

			if (next.activeSelf)
			{
                if (selection)
				{
					// 선택지 발동
					if (selectedItem == 0)
					{
						string script = datas[chatPos].select1Event;
                        datas = Chat_ParseScript.GetScript(script);
                    }
                    else
                    {
                        string script = datas[chatPos].select2Event;
                        datas = Chat_ParseScript.GetScript(script);
                    }
                    select1.color = new Color(255, 255, 255, 0);
                    select2.color = new Color(255, 255, 255, 0);
                    chatPos = 0;
                }
				else if (chatPos == datas.Length - 1)
				{
					// 대화 끝남
					return;
				}
				chatPos++;
				startChat();
			}
			else
			{
                script.text = datas[chatPos].script;
				StopCoroutine(chatCoroutine);
				if (selection)
				{
					selectedItem = 0;
					select1.color = new Color(255, 255, 255, 255);
					select1Text.text = datas[chatPos].select1;

					select2.color = new Color(255, 255, 255, 160);
					select2Text.text = datas[chatPos].select2;
				}
				next.SetActive(true);
            }
        }
		else if (selection)
		{
			if (selectedItem == 1 && Input.GetKeyDown(KeyCode.UpArrow))
			{
				selectedItem = 0;

                Color color = select1.color;
                color.a = 1.0f;
                select1.color = color;

                color.a = 0.6f;
                select2.color = color;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
                selectedItem = 1;

                Color color = select1.color;
                color.a = 0.6f;
                select1.color = color;

                color.a = 1.0f;
                select2.color = color;
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
        if (selection)
        {
            selectedItem = 0;

			Color color = select1.color;
			color.a = 1.0f;
			select1.color = color;
            select1Text.text = datas[chatPos].select1;

			color.a = 0.6f;
            select2.color = color;
            select2Text.text = datas[chatPos].select2;
        }
        next.SetActive(true);
        yield break;
	}

	void startChat()
	{
		selection = false;
		textPos = 0;
		author.text = datas[chatPos].name;
		script.text = "";

		if (datas[chatPos].status.Trim() != "")
		{
			Sprite s = Resources.Load<Sprite>("파일 이름");
			image.sprite = s;
		}
		if (datas[chatPos].select1.Trim() != "")
		{
			selection = true;
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
