using UnityEngine;
using System.Collections;

public class HeroMovement : HMObejct {
	public CNAbstractController MovementJoystick;
	private Transform _heroTransform;
	private string stateName;
	
	void Update ()
	{
		float positionX = MovementJoystick.GetAxis("Horizontal") * moveSpeed;
		float positionY = MovementJoystick.GetAxis("Vertical")	 * moveSpeed;
		Vector3 originPosition = gameObject.transform.position;
		Vector3 moveDirection;

		this.transform.Translate(positionX, positionY, 0);

		moveDirection = transform.position - originPosition; 

		if (moveDirection == Vector3.zero) {
			if(stateName != "heroIdle") {
				stateName = "heroIdle";
				_heroTransform.GetComponent<Animator>().Play("heroIdle");
				GetComponent<AudioSource>().Stop();
			}
		}else{
			if(stateName != "walk") {
				GetComponent<AudioSource>().clip = walkSound;
				GetComponent<AudioSource>().pitch = 0.8f;
				GetComponent<AudioSource>().loop = true;
				GetComponent<AudioSource>().Play ();
				stateName = "walk";
				_heroTransform.GetComponent<Animator>().Play("heroWalk");
			}
		}

		objectRotate(_heroTransform, originPosition, gameObject.transform.position, -90.0f);
	}

	void Start()
	{
		_heroTransform = this.transform.FindChild("hero");
	}
}
