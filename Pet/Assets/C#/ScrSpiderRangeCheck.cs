using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrSpiderRangeCheck : MonoBehaviour
{
	public ScrSpider spider;

	void OnEnable()
	{
		string updateRoute = transform.parent.GetInstanceID() + "UpdateRoute";
		EventManager.StartListening(updateRoute, UpdateRange);
	}

	void UpdateRange()
	{
		Vector2 newPosition = transform.position;
		newPosition.y = spider.transform.position.y;
		transform.position = newPosition;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player")) {
			Debug.Log("playerEntered");
			spider.playerNearby = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player")) {
			Debug.Log("playerExited");
			spider.playerNearby = false;
		}
	}

	void OnDisable()
	{
		string updateRoute = transform.parent.GetInstanceID() + "UpdateRoute";
		EventManager.StopListening(updateRoute, UpdateRange);
	}
}
