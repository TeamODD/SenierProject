using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[System.Serializable]
public struct ChatData
{
    public string name;
    public string script;
    public string filename;
}

public class Chat_ParseScript : MonoBehaviour
{
    private static Dictionary<string, ChatData[]> data = new Dictionary<string, ChatData[]>();

    [SerializeField] private TextAsset dataFile = null;

    public void Start()
    {
        string originalData = dataFile.text.Substring(0, dataFile.text.Length - 1);
        string[] lines = originalData.Split(new char[] { '\n' });

        string eventName = "";
        List<ChatData> items = new List<ChatData>();
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(new char[] { ',' });
            if (values[0] == "end")
            {
                data.Add(eventName, items.ToArray());
                items.Clear();
            }
            else if (values[0] != "")
            {
                eventName = values[0];
            }
            else
            {
                ChatData chat;
                chat.name = values[1];
                chat.script = values[2];
                chat.filename = values[3];
                items.Add(chat);
            }
        }
    }

    public static ChatData[] GetScript(string eventName)
    {
        return data[eventName];
    }
}
