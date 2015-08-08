using UnityEngine;
using System.Collections;

public class DrinkingBeers : MonoBehaviour {

	public int maxBeersCount;
	public AudioClip drinkSound;

	private int beersCount;
	private int drinkCount;
	private int targetDrinkCount;
	private int tKey;

	// UI Setting
	public int startL;
	public int startT;
	public int iconGAP;
	public Texture2D beerTexure;
	public Texture2D heartTexure;
	public Sprite[] drinkTexure;

	void Start () {
		beersCount = 0;
		drinkCount = 0;
		targetDrinkCount = 4;
		tKey = 1;
	}

	void OnMouseDown ()
	{
		int tIndex;
		drinkCount++;

		tIndex = drinkCount / tKey;
		GetComponent<SpriteRenderer>().sprite = drinkTexure[tIndex];
		GetComponent<AudioSource>().PlayOneShot(drinkSound);

		if(drinkCount >= targetDrinkCount){
			targetDrinkCount = targetDrinkCount * 2;
			tKey = targetDrinkCount / 4;
			drinkCount = 0;
			if(beersCount < maxBeersCount) beersCount++;
		}
	}

	void OnGUI()
	{
		for(int i = 0; i < beersCount; i++){
			Rect beerIconR = new Rect(((50+iconGAP) * i) + startL, startT, 50, 50);
			if(i==0) GUI.DrawTexture(beerIconR, heartTexure);
			else 	 GUI.DrawTexture(beerIconR, beerTexure);
		}
	}
}
