using UnityEngine;
using System.Collections;

public class ShipAI : Unit {

	public float rotateSpeed;
	public float stopRange;
	public float searchRange;
	public float changeRangeTime;
	public float attackRange;

	private NavMeshAgent nma;
	private LayerMask enemyLayer;
	private GameObject enemy;
	void Start()
	{
		base.Start ();
		nma = GetComponent<NavMeshAgent> ();
		enemyLayer = LayerManager.GetEnemyLayer (team);
		weapon = GetComponent<Weapon> ();
		weapon.Init (team);
		StartCoroutine (Timer());
	}

	void Update()
	{
		if (!enemy) 
		{
			searchEnemy();	//如果这帧没有敌人，搜索敌人，下一帧执行
			return;
		}
		float dist = Vector3.Distance (enemy.transform.position, transform.position);
		
		if (dist > stopRange)
			nma.SetDestination (enemy.transform.position);	//以某一坐标自动寻路
		else 
		{
			nma.ResetPath();	//如果距离小于半径停下来
			//瞄准
			Vector3 dir = enemy.transform.position - transform.position;		//对准的方向的三维坐标
			Quaternion wantRotation = Quaternion.LookRotation (dir);	//把一个三维坐标转换为一个四元素
			
			//从自己的角度渐渐旋转到目标的角度
			//从一个方向到另一个方向进行渐进旋转，即有一个过度的效果
			transform.rotation = Quaternion.Slerp(transform.rotation, wantRotation, rotateSpeed * Time.deltaTime); 	
		}
		
		if (dist < attackRange) 
		{
			weapon.Shoot();
		}
	}

	void searchEnemy()
	{
		Collider[] colls = Physics.OverlapSphere (transform.position, searchRange, enemyLayer);

		if (colls.Length > 0) 
		{
			int minIndex = 0;
			float minDist = Vector3.Distance (colls[0].transform.position, transform.position);
			for (int i=1; i<colls.Length; i++)
			{
				float curDist = Vector3.Distance (colls[i].transform.position, transform.position);
				if (curDist < minDist)
				{
					minIndex = i;
					minDist = curDist;
				}
			} // for
			enemy = colls[minIndex].gameObject;
		}//if
		else 
		{
			nma.ResetPath();
		}
	}//searchEnemy

	IEnumerator Timer()
	{
		while (enabled) 
		{
			searchEnemy();
			yield return new WaitForSeconds(changeRangeTime);
		}
	}



//	public float changeRangeTime;
//	private AIMove move;
//	private Enemy enemySearch;
//	private GameObject enemy;
//
//	void Start()
//	{
//		base.Start ();
//		move = GetComponent<AIMove> ();
//		enemySearch = GetComponent<Enemy> ();
//		weapon = GetComponent<AIWeapon>();
//		enemySearch.Init (team);
//		weapon.Init (team);
//	}
//
//	void Update()
//	{
//		if (null == enemy) {
//			enemy = enemySearch.SearchEnemy ();
//			return;
//		} 
//		else 
//		{
//			Debug.Log(enemy);
//			move.AutoMove (enemy);
//			weapon.Attack (enemy);
//		}
//		StartCoroutine (Timer());
//	}
//
//	IEnumerator Timer()
//	{
//		while (enabled) 
//		{
//			enemy = enemySearch.SearchEnemy();
//			yield return new WaitForSeconds(changeRangeTime);
//		}
//
//	}
}
