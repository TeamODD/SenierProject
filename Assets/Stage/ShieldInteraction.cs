using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldInteraction : MonoBehaviour
{
	bool moveable = false;

	Vector3 prevPosition;
	Vector3 targetPosition;

	[SerializeField] GameObject player;

	// Start is called before the first frame update
	void Start()
	{
		prevPosition = transform.position;
		targetPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, 0.1f);

		if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && moveable)
		{
			prevPosition = transform.position;
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
		if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Shield")
		{
			targetPosition = prevPosition;
			player.GetComponent<Player>().targetPosition = player.GetComponent<Player>().prevPosition;
			player.GetComponent<Player>().turnCount = player.GetComponent<Player>().prevTurnCount;
			moveable = false;
		}
		else if (collision.gameObject.tag == "Player")
		{
			moveable = true;
		}
	}
}
