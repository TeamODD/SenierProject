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
	[SerializeField] Image[] selects;
	[SerializeField] Text[] selectTexts;

	[SerializeField] string eventName;

	// for debugging
	[SerializeField] int selectedItem = 0;

	[SerializeField] private int chatPos = 0;
	private int textPos = 0;
	private bool selection = false;

	private string name_text = "";
	private string script_text = "";

	private IEnumerator chatCoroutine;

	// Start is called before the first frame update
	void Start()
	{
		for (int i = 0; i < selects.Length; i++)
		{
			selects[i].color = new Color(255, 255, 255, 0);
			selectTexts[i].color = new Color(255, 255, 255, 0);
		}
		script.text = "";

		name_text = StoryParser.data.chat[chatPos].name;
		script_text = StoryParser.data.chat[chatPos].script;

		startChat();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
		{
			if (next.activeSelf)
			{
				chatPos++;
				// When you step a Seletion
				if (selection)
				{
					for (int i = 0; i < selects.Length; i++)
					{
						selects[i].color = new Color(255, 255, 255, 0);
						selectTexts[i].color = new Color(255, 255, 255, 0);
					}
					name_text = StoryParser.data.selection_result.Find(x => x.index == chatPos + (0.1f * (selectedItem + 1))).name;
					script_text = StoryParser.data.selection_result.Find(x => x.index == chatPos + (0.1f * (selectedItem + 1))).script;
				}
				else
				{
					if (StoryParser.data.chat[chatPos].name != "")
						name_text = StoryParser.data.chat[chatPos].name;
					script_text = StoryParser.data.chat[chatPos].script;
				}

				selection = false;
				textPos = 0;
				script.text = "";

				startChat();

			}
			else
			{
				script.text = script_text;
				StopCoroutine(chatCoroutine);
				if (selection)
				{
					Color color = new Color(255, 255, 255);
					color.a = 0.6f;
					for (int i = 0; i < selects.Length; i++)
					{
						selects[i].color = color;
						selectTexts[i].color = color;
						switch (i)
						{
							case 0:
								selectTexts[i].text = StoryParser.data.selection.Find(x => x.index == chatPos).select1;
								break;
							case 1:
								selectTexts[i].text = StoryParser.data.selection.Find(x => x.index == chatPos).select2;
								break;
							case 2:
								selectTexts[i].text = StoryParser.data.selection.Find(x => x.index == chatPos).select3;
								break;
						}
					}
					
					selectedItem = 0;
					color.a = 1.0f;
					selects[0].color = color;
					selectTexts[0].color = color;
				}
				next.SetActive(true);
			}
		}
		else if (selection)
		{
			if (selectedItem > 0 && Input.GetKeyDown(KeyCode.UpArrow))
			{
				selectedItem--;
			}
			else if (selectedItem < 2 && Input.GetKeyDown(KeyCode.DownArrow))
			{
				selectedItem++;
			}

			Color color = new Color(255, 255, 255);
			color.a = 0.6f;
			for (int i = 0; i < selects.Length; i++)
			{
				selects[i].color = color;
				selectTexts[i].color = color;
			}

			color.a = 1.0f;
			selects[selectedItem].color = color;
			selectTexts[selectedItem].color = color;
		}
	}

	IEnumerator chatDelay(float time, string scription)
	{
		
		while (textPos < scription.Length)
		{
			script.text += scription.Substring(textPos, 1);
			textPos++;
			yield return new WaitForSeconds(time);
		}
		if (selection)
		{
			selectedItem = 0;

			Color color = new Color(255, 255, 255);
			color.a = 0.6f;
			for (int i = 0; i < selects.Length; i++)
			{
				selects[i].color = color;
				selectTexts[i].color = color;
				switch (i)
				{
					case 0:
						selectTexts[i].text = StoryParser.data.selection.Find(x => x.index == chatPos).select1;
						break;
					case 1:
						selectTexts[i].text = StoryParser.data.selection.Find(x => x.index == chatPos).select2;
						break;
					case 2:
						selectTexts[i].text = StoryParser.data.selection.Find(x => x.index == chatPos).select3;
						break;
				}
			}

			selectedItem = 0;
			color.a = 1.0f;
			selects[0].color = color;
			selectTexts[0].color = color;
		}
		next.SetActive(true);
		yield break;
	}

	void startChat()
	{
		author.text = name_text;
		// Action
		if (StoryParser.data.chat[chatPos].action != "")
		{
			string[] actions = StoryParser.data.chat[chatPos].action.Split(",");

			foreach (string action in actions)
			{
				switch (action)
				{
					case "select":
						selection = true;
						break;
					default:
						break;
				}
			}
		}

		next.SetActive(false);

		chatCoroutine = chatDelay(0.05f, script_text);
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
