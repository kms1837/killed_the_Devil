using UnityEngine;
using System.Collections;

public class HMObejct : MonoBehaviour {
	public int lifePoint;
	public float moveSpeed;
	public AudioClip walkSound;

	private int life;

	public int Life
	{
		get{ return this.life; }
		set{ this.life = value; }
	}

	public void objectRotate(Transform rotateObject, Vector3 originP, Vector3 targetP, float correctAngle)
	{
		Vector3 moveDirection = targetP - originP; 
		if (moveDirection != Vector3.zero)
		{
			float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
			rotateObject.rotation = Quaternion.AngleAxis(angle + correctAngle, Vector3.forward);
		}
	}
}
