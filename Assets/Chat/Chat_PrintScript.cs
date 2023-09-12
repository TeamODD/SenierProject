using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static StoryParser;

public class Chat_PrintScript : MonoBehaviour
{
	// disable - able selection control

	[SerializeField] TextAsset dataFile;
	private ScriptData data;

	// 채팅 출력에 필요한 오브젝트 참조
	[SerializeField] GameObject next; // 출력이 끝났는지 확인하는 오브젝트 
	[SerializeField] Image image; // 캐릭터 이미지를 출력하는 오브젝트
	[SerializeField] Image background; // 배겨 이미지를 출력하는 오브젝트 
	[SerializeField] Text author; // 발화자를 출력하는 오브젝트
	[SerializeField] Text script; // 대사를 출력하는 오브젝트
	[SerializeField] Image[] selects; // 선택지 배경화면 오브젝트 목록 (위에서 아래로)
	[SerializeField] Text[] selectTexts; // 선택지 텍스트 오브젝트 목록 (위에서 아래로)

	[SerializeField] string eventName; // 출력할 이벤트

	// for debugging - 선택된 아이템
	[SerializeField] int selectedItem = 0;

	[SerializeField] Sprite aron;
	[SerializeField] Sprite haru;
	[SerializeField] Sprite john;
	[SerializeField] Sprite bgPrologue_1;
	[SerializeField] Sprite bgLecture_room;

	[SerializeField] private int chatPos = 0; // 현재 출력 중인 리스트 요소의 인덱스
	private int textPos = 0; // 텍스트에서 현재 출력 중인 글자의 인덱스
	private bool selection = false; // 선택지 존재 여부

	private string name_text = ""; // 발화자
	private string script_text = ""; // 대사

	private IEnumerator chatCoroutine; // 채팅 출력 애니메이션 구현

	// Start is called before the first frame update
	void Start()
	{
		// 데이터 파일 읽기
		data = JsonUtility.FromJson<ScriptData>(dataFile.text);

		for (int i = 0; i < selects.Length; i++)
			selects[i].gameObject.SetActive(false);

		script.text = "";

		name_text = data.chat[chatPos].name;
		script_text = data.chat[chatPos].script;

		startChat();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
		{
			if (next.activeSelf)
			{
				LoadChat();
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
						selects[i].gameObject.SetActive(true);
						selects[i].color = color;
						selectTexts[i].color = color;
						switch (i)
						{
							case 0:
								selectTexts[i].text = data.selection.Find(x => x.index == chatPos).select1;
								break;
							case 1:
								selectTexts[i].text = data.selection.Find(x => x.index == chatPos).select2;
								break;
							case 2:
								selectTexts[i].text = data.selection.Find(x => x.index == chatPos).select3;
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
		else if (selection && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)))
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
				selects[i].gameObject.SetActive(true);
				selects[i].color = color;
				selectTexts[i].color = color;
				switch (i)
				{
					case 0:
						selectTexts[i].text = data.selection.Find(x => x.index == chatPos).select1;
						break;
					case 1:
						selectTexts[i].text = data.selection.Find(x => x.index == chatPos).select2;
						break;
					case 2:
						selectTexts[i].text = data.selection.Find(x => x.index == chatPos).select3;
						break;
				}
			}

			color.a = 1.0f;
			selects[0].color = color;
			selectTexts[0].color = color;
		}
		next.SetActive(true);
		yield break;
	}

	void LoadChat()
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

			// 선택지 출ㅁ
			switch (selectedItem)
			{
				case 0:
					name_text = data.selection_result[(int)data.selection.Find(x => x.index == chatPos - 1).select1_action].name;
					script_text = data.selection_result[(int)data.selection.Find(x => x.index == chatPos - 1).select1_action].script;
					break;
				case 1:
					name_text = data.selection_result[(int)data.selection.Find(x => x.index == chatPos - 1).select2_action].name;
					script_text = data.selection_result[(int)data.selection.Find(x => x.index == chatPos - 1).select2_action].script;
					break;
				case 2:
					name_text = data.selection_result[(int)data.selection.Find(x => x.index == chatPos - 1).select2_action].name;
					script_text = data.selection_result[(int)data.selection.Find(x => x.index == chatPos - 1).select2_action].script;
					break;

			}
		}
		else
		{
			if (data.chat[chatPos].name != "")
				name_text = data.chat[chatPos].name;
			script_text = data.chat[chatPos].script;
		}

		if (data.chat[chatPos].standing != "") ChangeStading(data.chat[chatPos].standing);
		if (data.chat[chatPos].background != "") ChangeBackground(data.chat[chatPos].background);

		selection = false;
		textPos = 0;
		script.text = "";

		startChat();
	}

	void startChat()
	{
		author.text = name_text;
		// Action
		if (data.chat[chatPos].action != "")
		{
			string[] actions = data.chat[chatPos].action.Split(",");

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

	void ChangeStading(string file)
	{
		switch (file)
		{
			case "aron":
			case "arron-0":
			case "arron-embarrassment_1":
			case "arron-embarrassment_2":
				image.sprite = aron;
				break;
			case "haru-smile_1":
			case "haru-positive_1":
			case "haru-joke_1":
				image.sprite = haru;
				break;
		}
	}

	void ChangeBackground(string file)
	{
		switch (file)
		{
			case "bg-lecture_room":
				background.sprite = bgLecture_room;
				break;
			case "bg-prologue_1":
				background.sprite = bgPrologue_1;
				break;
		}
	}
}
