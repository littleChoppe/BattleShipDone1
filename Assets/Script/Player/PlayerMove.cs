/// <summary>
/// Player move.
/// 如果未到达目标，往人物朝向直走
/// </summary>
using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	public bool isMoving = false;
	public float moveSpeed = 4;

	private PlayerDir dir;
	private PlayerWeapon attack;

	void Start () 
	{
		dir = GetComponent<PlayerDir> ();
		attack = GetComponent<PlayerWeapon> ();
	}

	public void Move()
	{
		if (attack.isClickEnemy == false) 
		{
			float distance = Vector3.Distance (transform.position, dir.targetPos);
			
			//如果未到底则直走
			if (distance > 0.3f) {
				isMoving = true;
				transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
			} else
				isMoving = false;
		}
	}

	//走向敌人
	public void MoveToTarget(Vector3 targetPosition)
	{
		transform.LookAt (targetPosition);
		Debug.Log ("走向敌人");
		transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
	}
}
