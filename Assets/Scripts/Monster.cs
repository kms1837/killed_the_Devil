﻿using UnityEngine;
using System.Collections;

public class Monster : HMObejct {
	
	public float setTargetTime;

	private bool moveFlag;
	private bool targetFlag;
	private GameObject target;

	private float targetTime;

	void Start () {
		moveFlag	= false;
		targetFlag  = false;
	}

	void Update ()
	{
		if(moveFlag) {
			Vector2 targetPosition  = target.transform.position;
			transform.position		= Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
			objectRotate(this.transform, this.transform.position, targetPosition, 90.0f);
		}

		if(targetFlag) {
			targetTime += Time.deltaTime;

			if(targetTime >= setTargetTime) {
				moveFlag	= false;
				targetFlag  = false;
			}
		}
	}

	void targetOn(GameObject targetObject)
	{
		if(targetObject.tag == "Player") {
			targetFlag = false;
			targetTime = 0;
			
			if(!moveFlag) {
				moveFlag = true;
				target	 = targetObject.gameObject;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D enterObject) {
		if(enterObject.gameObject.tag == "Player"){
			Bounds objectBound  = enterObject.transform.FindChild("hero").GetComponent<Renderer>().bounds;
			Bounds monsterBound = gameObject.GetComponent<Renderer>().bounds;
			CNAbstractController joystick = enterObject.gameObject.GetComponent<HeroMovement>().MovementJoystick;

			float knockBackPoint = 0.5f;
			float knockBackX = joystick.GetAxis("Horizontal") + knockBackPoint;
			float knockBackY = joystick.GetAxis("Vertical")	  + knockBackPoint;

			if(objectBound.min.y > monsterBound.center.y) {
				//back attack
				this.lifePoint -= 1;
				this.transform.Translate(knockBackX, knockBackY, 0);
			} else {
				//front attack
				this.lifePoint -= 1;
				this.transform.Translate(knockBackX, knockBackY, 0);
				enterObject.gameObject.GetComponent<HeroMovement>().lifePoint -= 1;
				enterObject.transform.Translate(knockBackX * -1, knockBackY * -1, 0);
			}

			if(this.lifePoint <= 0) {
				Destroy(gameObject);
			}

			targetOn(enterObject.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D trigerObject)
	{
		targetOn(trigerObject.gameObject);
	}

	void OnTriggerExit2D()
	{
		targetFlag = true;
	}
}
