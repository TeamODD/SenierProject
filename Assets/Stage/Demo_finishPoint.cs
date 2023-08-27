using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().moveable = false;
            panel.SetActive(true);
        }
	}
}
