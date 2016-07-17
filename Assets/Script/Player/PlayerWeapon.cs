using UnityEngine;
using System.Collections;

public class PlayerWeapon : Weapon {

	private float attackDistancce = 10;
	private GameObject target = null;
	private PlayerMove playerMove;
	public bool isClickEnemy = false;

	void Start()
	{
		base.Start ();
		playerMove = GetComponent<PlayerMove> ();
	}

	public override void Attack()
	{
		if (Input.GetMouseButtonDown(0)) 
		{
			//从屏幕鼠标的位置发出一条射线
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			//保存射线碰撞信息
			RaycastHit hitInfor;
			//发出射线，看是否能碰到东西
			//		bool isCollider = Physics.Raycast (ray, out hitInfor, Mathf.Infinity,1 << LayerMask.NameToLayer("Ground"));
			bool isCollider = Physics.Raycast (ray, out hitInfor);
			
			if (isCollider && (hitInfor.collider.tag == Tags.enemy || hitInfor.collider.tag == Tags.blueBaseBuilding))
			{
				isClickEnemy = true;
				target = hitInfor.collider.gameObject;
			}
			else
			{
				isClickEnemy = false;
				target = null;
			}
		}

		if (target && isClickEnemy) 
		{
			if (Vector3.Distance(target.transform.position, this.transform.position) <= attackDistancce)
			{
				transform.LookAt (target.transform.position);
				Shoot();
			}
			
			else
			{
				playerMove.MoveToTarget(target.transform.position);
			}
		}
	}
}
