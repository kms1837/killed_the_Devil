using UnityEngine;
using System.Collections;

public class DrinkingBeers : MonoBehaviour {

	public int maxBeersCount;
	public AudioClip drinkSound;

	private int beersCount;
	private int drinkCount;
	private int targetDrinkCount;
	private int tKey;

	private bool drinkFlag;
	private bool talkFlag;
	private bool flag;

	private float talkFlagTime;

	// UI Setting
	public int startL;
	public int startT;
	public int iconGAP;
	public Texture2D beerTexure;
	public Texture2D heartTexure;
	public Texture2D openingTexure;
	public Texture2D talkTexure;
	public Sprite[] drinkTexure;

	private Color currentColor;
	private float startTime;

	void Start () {
		beersCount = 1;
		drinkCount = 0;
		targetDrinkCount = 4;
		tKey = 1;
		drinkFlag 	= false;
		flag		= false;
		talkFlag	= false;
		GameObject.Find("BackGround").GetComponent<ScreenChangeEffect>().drawBackground(Color.black);
	}

	void OnMouseDown ()
	{
		if(drinkFlag){
			if(!flag){
				int tIndex;
				drinkCount++;

				tIndex = drinkCount / tKey;
				GetComponent<SpriteRenderer>().sprite = drinkTexure[tIndex];
				GetComponent<AudioSource>().PlayOneShot(drinkSound);

				if(drinkCount >= targetDrinkCount){
					drinkCount = 0;
					targetDrinkCount = targetDrinkCount; //+ 4;
					tKey = targetDrinkCount / 4;

					if(beersCount < (maxBeersCount+1)){
						GetComponent<SpriteRenderer>().sprite = drinkTexure[0];
						beersCount++;

					}else{
						GameObject.Find("BackGround").GetComponent<ScreenChangeEffect>().fadeOut();
						flag = true;
						talkFlagTime = 0;
					}
				}
			} else {
				if(talkFlag){
					Application.LoadLevel("StartStage");
				}
			}
		}else{
			drinkFlag = true;
			GameObject.Find("BackGround").GetComponent<ScreenChangeEffect>().fadeIn();
		}
	}

	void Update()
	{
		if(flag) {
			if(talkFlag) {
				float guiTime = Time.time - startTime;
				currentColor = Color.Lerp(new Color(0, 0, 0, 0), new Color(1.0f, 1.0f, 1.0f, 1.0f), guiTime/1.5f);
			}else{
				talkFlagTime += Time.deltaTime;

				if(talkFlagTime >= 5) {
					startTime = Time.time;
					talkFlag  = true;
				}
			}
		}
	}

	void OnGUI()
	{
		if(!drinkFlag) {
			Rect screenR = new Rect(0, 0, Screen.width, Screen.height);
			GUI.DrawTexture(screenR, openingTexure);

		}else{
			for(int i = 0; i < beersCount; i++) {
				Rect beerIconR = new Rect(((50+iconGAP) * i) + startL, startT, 50, 50);
				if(i==0) GUI.DrawTexture(beerIconR, heartTexure);
				else 	 GUI.DrawTexture(beerIconR, beerTexure);
			}

			if(flag){
				Rect screenR = new Rect(20, Screen.height/2 - 150, Screen.width - 40, 300);
				GUI.depth = -2;
				GUI.color = currentColor;
				GUI.DrawTexture(screenR, talkTexure);
			}
		}
	}
}
