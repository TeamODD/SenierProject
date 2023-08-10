using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[System.Serializable]
public struct ChatData
{
    public string name;
    public string script;
    public string status;
    public string select1;
    public string select2;
    public string select1Event;
    public string select2Event;
    public string nextEvent;
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
                continue;
            }
            else if (values[0] != "")
            {
                eventName = values[0];
            }

            ChatData chat;
            chat.name = values[1];
            chat.script = values[2];
            chat.status = values[3];
            chat.select1 = values[4];
            chat.select1Event = values[5];
            chat.select2 = values[6];
            chat.select2Event = values[7];
            chat.nextEvent = values[8];
            items.Add(chat);
        }
    }

    public static ChatData[] GetScript(string eventName)
    {
        return data[eventName];
    }
}
