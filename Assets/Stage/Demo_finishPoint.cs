using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Demo_finishPoint : MonoBehaviour
{
    [SerializeField] GameObject panel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (panel.activeSelf && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Main");
        }
    }

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
