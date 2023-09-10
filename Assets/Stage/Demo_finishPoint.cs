using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo_finishPoint : MonoBehaviour
{
    [SerializeField] GameObject panel;

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().moveable = false;
			collision.gameObject.GetComponent<Player>().clear = true;
			panel.SetActive(true);
        }
	}
}
