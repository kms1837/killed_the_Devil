using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D trigerObject)
	{
		GameObject targetObject = trigerObject.gameObject;
		if(targetObject.tag == "Player") {
			int maxLife = targetObject.GetComponent<HMObejct>().lifePoint;
			int nowLife = targetObject.GetComponent<HMObejct>().Life;

			if(nowLife < maxLife){
				if(targetObject.GetComponent<HMObejct>().Life <= 1) {
					targetObject.GetComponent<HeroMovement>().safe ();
				}
				targetObject.GetComponent<HMObejct>().Life += 1;
				Destroy(this.gameObject);
			}
		}
	}
}
