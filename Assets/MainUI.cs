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
    Quit, //������� ���� �޴� + �ΰ��� �޴�
    Title //������� �ΰ��� �޴���
}

public class MainUI : MonoBehaviour
{
    public void PlayBtn()
    {
        SceneManager.LoadScene("Play");
    }
}
