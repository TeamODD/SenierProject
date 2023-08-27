using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInteraction : MonoBehaviour
{
	bool moveable = false;

	Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
		targetPosition = transform.position;
	}

    // Update is called once per frame
    void Update()
    {
		transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, 0.1f);

		if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && moveable)
		{
			targetPosition = transform.position;

			if (Input.GetAxisRaw("Horizontal") < 0)
			{
				// 좌측 이동
				targetPosition.x -= 1.0f;
			}
			else if (Input.GetAxisRaw("Horizontal") > 0)
			{
				// 우측 이동
				targetPosition.x += 1.0f;
			}
			else if (Input.GetAxisRaw("Vertical") > 0)
			{
				// 상향 이동
				targetPosition.y += 1.0f;
			}
			else if (Input.GetAxisRaw("Vertical") < 0)
			{
				// 하향 이동
				targetPosition.y -= 1.0f;
			}

			moveable = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Wall")
		{
            Destroy(gameObject);
		}
		else if (collision.gameObject.tag == "Player")
		{
			moveable = true;
		}
	}
}
