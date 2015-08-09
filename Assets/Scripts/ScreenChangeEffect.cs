using System.Collections;
using UnityEngine;

public class ScreenChangeEffect : MonoBehaviour {
	public  Texture fadeTexture;
	public  float setEffectSpeed;
	public	int	  orderInLayer;
	private float duration;
	private float startTime;
	private int   effectNumber;
	private Color startColor;
	private Color endColor;
	private Color currentColor;
	public  bool  playState;
	/*
		playState
		true  - effectPlay
		false - effectEnd
	 */
	
	void Start () {
		effectNumber = 0;
		startTime	 = Time.time;
		currentColor = startColor;
		//fadeIn();
	}

	public void fadeIn()
	{
		effectNumber = 1;
		playState	 = true;
		startTime	 = Time.time;
		duration 	 = setEffectSpeed;
		startColor   = new Color(0,0,0,1);
		endColor     = new Color(0,0,0,0);
	}

	public void fadeOut()
	{
		effectNumber = 1;
		playState	 = true;
		startTime	 = Time.time;
		duration 	 = setEffectSpeed;
		startColor   = new Color(0,0,0,0);
		endColor     = new Color(0,0,0,1);
	}

	public void fadeInOut()
	{
		effectNumber = 2;
		playState	 = true;
		startTime	 = Time.time;
		duration 	 = setEffectSpeed;
		startColor   = new Color(0,0,0,1);
		endColor     = new Color(0,0,0,0);
	}

	public void fadeOutIn()
	{
		effectNumber = 2;
		playState	 = true;
		startTime	 = Time.time;
		duration	 = setEffectSpeed;
		startColor   = new Color(0,0,0,0);
		endColor     = new Color(0,0,0,1);
	}

	public void screenColorChange(Color setColor)
	{
		effectNumber = 1;
		playState	 = true;
		startTime	 = Time.time;
		duration	 = setEffectSpeed;
		startColor   = new Color(0,0,0,0);
		endColor     = setColor;
	}

	public void drawBackground(Color setColor)
	{
		currentColor = setColor;
		playState	 = true;
	}
	
	void OnGUI() {
		if(effectNumber >= 0){
			GUI.depth = orderInLayer;
			GUI.color = currentColor;
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
		}
	}

	void FixedUpdate () {
		float guiTime = Time.time - startTime;
		switch(effectNumber) {
			case 1:
				currentColor = Color.Lerp(startColor, endColor, guiTime/duration);
				if(currentColor == endColor) {
					playState = false;
				}
				break;
			case 2:{
				currentColor = Color.Lerp(startColor, endColor, guiTime/duration);
				if(currentColor == endColor) {
					startTime		= Time.time;
					Color swapColor = endColor;
					endColor 		= startColor;
					startColor 		= swapColor;
					effectNumber 	= 1;
					playState		= false;
				}
				break;
			}
		}
	}
}
