using UnityEngine;
using System.Collections;

public class DrinkingBeers : MonoBehaviour {

	public int maxBeersCount;

	private int beersCount;
	private int drinkCount;
	private int targetDrinkCount;

	// UI Setting
	public int startL;
	public int startT;
	public int iconGAP;
	public Texture2D beerTexure;

	void Start () {
		beersCount = 0;
		drinkCount = 0;
		targetDrinkCount = 5;
	}
	
	void Update () {
	
	}

	void OnMouseDown ()
	{
		drinkCount++;
		if(drinkCount >= targetDrinkCount){
			targetDrinkCount = targetDrinkCount * 2;
			if(beersCount < maxBeersCount) beersCount++;
		}
	}

	void OnGUI()
	{
		for(int i = 0; i < beersCount; i++){
			Rect beerIconR = new Rect(((50+iconGAP) * i) + startL, startT, 50, 50);
			GUI.DrawTexture(beerIconR, beerTexure);
		}
	}
}
