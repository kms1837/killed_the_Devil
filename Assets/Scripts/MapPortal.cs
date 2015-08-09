using UnityEngine;
using System.Collections;

public class MapPortal : MonoBehaviour {
	public GameObject movePortal;

	void OnTriggerEnter2D(Collider2D trigerObject)
	{
		if(trigerObject.gameObject.tag == "Player"){
			trigerObject.gameObject.transform.position = movePortal.transform.position;
		}
	}
}
