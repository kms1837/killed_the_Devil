using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {
	public string moveSceneName;

	void OnTriggerEnter2D(Collider2D trigerObject)
	{
		Application.LoadLevel(moveSceneName);
	}
}
