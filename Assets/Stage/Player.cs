using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	[SerializeField] Text countText; // 남은 턴을 표시하는 텍스트 - 유니티 화면에서 지정 필요

	public Vector3 targetPosition;
	public Vector3 prevPosition;
	public bool moveable = true;
	public bool clear = false;
	[SerializeField] public int turnCount = 10; // 유니티 에디터 화면에서 확인 가능
	public int prevTurnCount; // 유니티 에디터 화면에서 확인 가능

	// 살려줘요
	[SerializeField] Image message;
	[SerializeField] Text messageText;
	bool isMsg = false;
	int tiktok = 0;

	// Start is called before the first frame update
	void Start()
	{
		message.gameObject.SetActive(false);
		messageText.gameObject.SetActive(false);
		prevTurnCount = turnCount;
		prevPosition = transform.position;
		targetPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		// 턴 수 표시
		countText.text = turnCount.ToString();

		// 캐릭터 위치를 지정한 위치로 이동
		transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, 0.1f);

		if (Input.GetKeyDown(KeyCode.E) && !isMsg)
		{
			message.gameObject.SetActive(true);
			messageText.gameObject.SetActive(true);
			isMsg = true;
			Invoke("DisableMessage", 2.0f);
			return;
		}

		// 움직임 관련 코드 / 움직일 수 있고 입력이 있을 때
		if (moveable && turnCount > 0 && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
		{
			prevTurnCount = turnCount;
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
			turnCount--;
			Invoke("FinishMove", 0.2f);
		}
	}

	void FinishMove()
	{
		moveable = true;
		if (!clear && turnCount == 0)
		{
			SceneManager.LoadScene("Stage1");
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Wall")
		{
			targetPosition = prevPosition;
			turnCount = prevTurnCount;
		}
	}

	void DisableMessage()
	{
		message.gameObject.SetActive(false);
		isMsg = false;
	}
}
