using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoryParser : MonoBehaviour
{

	[Serializable]
	public struct ChatData
	{
		public float index;
		public string name;
		public string script;
		public string standing;
		public string background;
		public string music;
		public string action;
	}

	[Serializable]
	public struct SelectData
	{
		public float index;
		public string name;
		public string select1;
		public float select1_action;
		public string select2;
		public float select2_action;
		public string select3;
		public float select3_action;
	}

	[Serializable]
	public struct ScriptData
	{
		public List<ChatData> chat;
		public List<SelectData> selection;
		public List<ChatData> selection_result;
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		
	}
}