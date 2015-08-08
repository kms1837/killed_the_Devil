using UnityEngine;
using System.Collections;

public class HeroMovement : HMObejct {
	public CNAbstractController MovementJoystick;
	private Transform _heroTransform;
	private string stateName;

	private bool gameOverFlag;
	public AudioClip gameOverBGM;

	// UI Setting
	public int startL;
	public int startT;
	public int iconGAP;
	public Texture2D lifeTexure;

	//Screen Effect Setting
	private Color startColor;
	private Color endColor;
	private ScreenChangeEffect fadeEffect;

	
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

		if(!gameOverFlag){
			if(lifePoint <= 0){
				AudioSource soundSetting;

				gameOverFlag = true;
				GameObject backgroundObject = GameObject.Find("BackGround");
				fadeEffect = backgroundObject.GetComponent<ScreenChangeEffect>();
				fadeEffect.screenColorChange(new Color(0.2f, 0.2f, 0.2f, 0.7f));

				soundSetting = Camera.main.GetComponent<AudioSource>();
				soundSetting.clip = gameOverBGM;
				soundSetting.Play();
			}
		}
	}

	void OnGUI()
	{
		for(int i=0; i<this.lifePoint; i++){
			Rect beerIconR = new Rect(((50+iconGAP) * i) + startL, startT, 50, 50);
			GUI.DrawTexture(beerIconR, lifeTexure);
		}
	}

	void Start()
	{
		_heroTransform = this.transform.FindChild("hero");
		gameOverFlag = false;
	}

}
