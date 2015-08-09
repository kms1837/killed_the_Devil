using UnityEngine;
using System.Collections;

public class HeroMovement : HMObejct {

	public CNAbstractController MovementJoystick;
	public AudioClip gameOverBGM;
	public AudioClip normalBGM;
	public AudioClip dangerBGM;
	private bool gameOverFlag;

	public float setHeartTimer;
	private float heartTimer;

	private Transform _heroTransform;
	private string stateName;

	// other(delete)
	public bool playState = false;
	private Color currentColor;
	private float currentTime;
	private float startTime;

	// UI Setting
	public int startL;
	public int startT;
	public int iconGAP;
	public Texture2D lifeTexure;
	public Texture2D lastLifeTexure;
	public Texture2D gameOverTexure;
	public Texture2D deadTexure;
	public Texture2D gameOverTalkTexure;

	//Screen Effect Setting
	private Color startColor;
	private Color endColor;
	private ScreenChangeEffect fadeEffect;
	
	void Update ()
	{
		if(!playState) {
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

			if(this.Life > 0){ //edit
				heartTimer += Time.deltaTime;

				if(heartTimer >= setHeartTimer) {
					heartTimer = 0;
					this.Life -= 1;
				}

				if(this.Life <= 1) danger();
			}
		}

		if(!gameOverFlag){
			if(Life <= 0){
				AudioSource soundSetting;
				
				gameOverFlag = true;
				GameObject backgroundObject = GameObject.Find("BackGround");
				fadeEffect = backgroundObject.GetComponent<ScreenChangeEffect>();
				fadeEffect.screenColorChange(new Color(0.2f, 0.2f, 0.2f, 0.7f));
				
				soundSetting = Camera.main.GetComponent<AudioSource>();
				soundSetting.clip = gameOverBGM;
				soundSetting.loop = false;
				soundSetting.Play();
				
				playState = true;
				startTime = Time.time;
			}
		}else{
			//game over
			float guiTime = Time.time - startTime;
			currentColor = Color.Lerp(new Color(0, 0, 0, 0), new Color(1.0f, 1.0f, 1.0f, 1.0f), guiTime/1.5f);
		}
	}

	public void safe()
	{
		AudioSource soundSetting;
		soundSetting = Camera.main.GetComponent<AudioSource>();
		soundSetting.clip = normalBGM;
		soundSetting.loop = true;
		soundSetting.Play();
	}

	public void danger()
	{
		AudioSource soundSetting;
		soundSetting = Camera.main.GetComponent<AudioSource>();
		soundSetting.clip = dangerBGM;
		soundSetting.loop = true;
		soundSetting.Play();
	}

	void OnGUI()
	{
		if(!gameOverFlag){
			for(int i=0; i<this.Life; i++){
				if(i==0){
					Rect beerIconR = new Rect(((50+iconGAP) * i) + startL, startT, 90, 50);
					GUI.DrawTexture(beerIconR, lastLifeTexure);
				}else{
					Rect beerIconR = new Rect(((50+iconGAP) * i) + startL, startT, 60, 50);
					GUI.DrawTexture(beerIconR, lifeTexure);
				}
			}
		}else{
			//game over

			//game over talk
			Rect talkScreenR = new Rect(20, Screen.height/2 - 300, Screen.width - 40, 300);
			GUI.depth = -1;
			GUI.color = currentColor;
			GUI.DrawTexture(talkScreenR, gameOverTalkTexure);

			//game over Text
			Rect textScreenR = new Rect(20, Screen.height/2 + 150, Screen.width - 40, 100);
			GUI.depth = -2;
			GUI.color = currentColor;
			GUI.DrawTexture(textScreenR, gameOverTexure);

			//game over char
			Rect charScreenR = new Rect(Screen.width/2 - 40, Screen.height/2, 90, 110);
			GUI.depth = -2;
			GUI.color = currentColor;
			GUI.DrawTexture(charScreenR, deadTexure);
		}
	}

	void Start()
	{
		_heroTransform = this.transform.FindChild("hero");
		gameOverFlag = false;
		Life		 = this.lifePoint;
		heartTimer = 0;
	}

}
