using UnityEngine;
using System.Collections;

public class EndingScene : MonoBehaviour {

	public GameObject Player;
	public GameObject[] Speech_Bubble = new GameObject[5];
	public GameObject[] Resident = new GameObject[6];

	int actionNum;
	int speechNum;

	float moveSpeed = 0.01f;
	float residentSpeed = 0.08f;

	const float firstX = -1.75f;
	int Cnt = 0;

	float timer;
	float waitingTime;

	// Use this for initialization
	void Start () {
		actionNum = 1;
		speechNum = 0;

		timer = 0.0f;
		waitingTime = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		switch (actionNum) {
		case 1:
			PlayerMove ();
			break;
		case 2:
			SpeechBubble ();

			timer += Time.deltaTime;
			if(timer >=  waitingTime){
				Count ();
				timer = 0.0f;
			}

			break;

		case 3:
			RotateResident();
			break;

		case 4:
			MoveResident ();
			break;
		case 5:
			PlayerMove_2();
			break;
		case 6:
			LastSpeech();
			break;
		}
	}

	void PlayerMove()
	{
		Player.transform.position = new Vector3 (Player.transform.position.x + moveSpeed, Player.transform.position.y, -1.0f);
		if (Player.transform.position.x >= firstX)
			actionNum++;
	}

	void SpeechBubble()
	{
		if (speechNum == 0) {
			Speech_Bubble[speechNum].transform.position = new Vector3(Speech_Bubble[speechNum].transform.position.x, Speech_Bubble[speechNum].transform.position.y, -1.0f);
		}
		else if (speechNum == 1) {
			Destroy (Speech_Bubble[0]);
			Speech_Bubble[speechNum].transform.position = new Vector3(Speech_Bubble[speechNum].transform.position.x, Speech_Bubble[speechNum].transform.position.y, -1.0f);
		}
		else if (speechNum == 2) {
			Destroy (Speech_Bubble[1]);
			Speech_Bubble[speechNum].transform.position = new Vector3(Speech_Bubble[speechNum].transform.position.x, Speech_Bubble[speechNum].transform.position.y, -1.0f);
		}
		if (speechNum == 3) {
			Destroy (Speech_Bubble[2]);
			actionNum++;
		}
	}

	void RotateResident()
	{
		for (int i=0; i<6; i++) {
			if( Resident[i].transform.rotation.z > 0 ) Resident[i].transform.Rotate(0,0,Resident[i].transform.rotation.z + 2.0f);
			if( Resident[i].transform.rotation.z < 0 ){
				Debug.Log ("asf : " + Resident[i].transform.rotation.z);
				Resident[i].transform.rotation = new Quaternion(0,0,0,0);
				Cnt++;
			}
		}
		if (Cnt == 6) {
			actionNum++;
			Cnt = 0;
		}
	}

	void MoveResident()
	{
		for (int i=0; i<6; i++) {
			Resident[i].transform.position = new Vector3( Resident[i].transform.position.x + residentSpeed, Resident[i].transform.position.y, 0 );
			if(Resident[i].transform.position.x >= 4.5f){
				Cnt++;
			}
			if (Cnt == 6) {
				actionNum++;
				Cnt = 0;
			}
		}
		Cnt = 0;
	}

	void PlayerMove_2()
	{
		Player.transform.position = new Vector3 (Player.transform.position.x + moveSpeed*2, Player.transform.position.y, -1.0f);
		if (Player.transform.position.x >= 0)
			actionNum++;
	}

	void LastSpeech()
	{
		Speech_Bubble[3].transform.position = new Vector3(Speech_Bubble[3].transform.position.x, Speech_Bubble[3].transform.position.y, -1.0f);
		Speech_Bubble[4].transform.position = new Vector3(Speech_Bubble[4].transform.position.x, Speech_Bubble[4].transform.position.y, -0.8f);
		Invoke ("DestroySpeech",2.0f);
	}

	void DestroySpeech()
	{
		Destroy (Speech_Bubble [3]);
		actionNum++;
	}

	void Count()
	{
		speechNum++;
	}
}
