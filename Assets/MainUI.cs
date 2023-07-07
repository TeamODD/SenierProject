using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BTNType
{
    New,
    Continue,
    Option,
    Back,
    Quit, //여기까지 메인 메뉴 + 인게임 메뉴
    Title //여기부터 인게임 메뉴만
}

public class MainUI : MonoBehaviour
{
    public void PlayBtn()
    {
        SceneManager.LoadScene("Play");
    }
}
