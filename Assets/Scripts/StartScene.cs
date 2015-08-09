using UnityEngine;
using System.Collections;

public class StartScene : MonoBehaviour {
	public Sprite[] talkTexure;
	private int talkCount;

	// Use this for initialization
	void Start () {
		talkCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0)){
			if(talkCount < talkTexure.Length - 1) {
				talkCount++;
				gameObject.GetComponent<SpriteRenderer>().sprite = talkTexure[talkCount];
				GameObject.Find ("player").GetComponent<HeroMovement>().playState = true;
				GameObject.Find("player").transform.FindChild("hero").GetComponent<Animator>().Play("heroIdle");
			}else{
				GameObject.Find ("player").GetComponent<HeroMovement>().playState = false;
				Destroy(gameObject);
			}
		}
	}
}
