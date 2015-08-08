using UnityEngine;
using System.Collections;

public class Monster : HMObejct {
	
	public float setTargetTime;
	public iTweenPath setPath = null;

	public AudioClip attackSound;
	public AudioClip hitSound;

	private bool moveFlag;
	private bool targetFlag;
	private GameObject target;

	private Vector3 startPosition;

	private float targetTime;

	void Start () {
		moveFlag	= false;
		targetFlag  = false;
		this.Life	= this.lifePoint;
		startPosition = this.transform.position;

		startPathMove();
	}

	void startPathMove()
	{
		if(setPath != null) {
			iTween.MoveFrom(gameObject, iTween.Hash ("path", iTweenPath.GetPath(setPath.pathName)
			                                         , "time", 10.0f
			                                         , "easetype", iTween.EaseType.linear
			                                         , "looptype", "pingpong"                     
			                                         ));
		}
	}
	
	void Update ()
	{
		if(moveFlag) {
			Vector2 targetPosition  = target.transform.position;
			transform.position		= Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
			objectRotate(this.transform, this.transform.position, targetPosition, 90.0f);
		}else{
			Vector2 originPosition	= transform.position;
			Vector2 endPosition		= transform.position;
			objectRotate(this.transform, this.transform.position, this.transform.position, 90.0f);
		}

		if(targetFlag) {
			targetTime += Time.deltaTime;

			if(targetTime >= setTargetTime) {
				moveFlag	= false;
				targetFlag  = false;
				iTween.MoveTo(gameObject, startPosition, 15.0f);
				startPathMove();
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
				this.Life -= 1;
				this.transform.Translate(knockBackX, knockBackY, 0);
				enterObject.gameObject.GetComponent<AudioSource>().pitch = 1.0f;
				enterObject.gameObject.GetComponent<AudioSource>().PlayOneShot(attackSound);

			} else {
				//front attack
				enterObject.gameObject.GetComponent<HeroMovement>().Life -= 1;
				enterObject.transform.Translate(knockBackX * -1, knockBackY * -1, 0);

				if(this.tag == "boss"){
					enterObject.gameObject.GetComponent<AudioSource>().pitch = 1.0f;
					enterObject.gameObject.GetComponent<AudioSource>().PlayOneShot(hitSound);
				}else{
					this.Life -= 1;
					this.transform.Translate(knockBackX, knockBackY, 0);
					enterObject.gameObject.GetComponent<AudioSource>().pitch = 1.0f;
					enterObject.gameObject.GetComponent<AudioSource>().PlayOneShot(attackSound);
				}
			}

			if(this.Life <= 0) {
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
