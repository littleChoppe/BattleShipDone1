/// <summary>
/// 1、点击左键时，从屏幕上发出一条射线碰撞场景中带有碰撞器的物体，获取点击的位置
/// 2、判断点击位置是否在地面，在点击处产生点击效果
/// 3、使人物的本地Z轴朝向点击处
/// </summary>

using UnityEngine;
using System.Collections;

public class PlayerDir : MonoBehaviour {

	public GameObject clickEffectPrefabs;
	public Vector3 targetPos; 	//朝向的目标位置

	private bool isChangedDir = false;	//鼠标是否按下
	private PlayerMove move;
	
	void Start()
	{
		move = GetComponent<PlayerMove> ();
		targetPos = transform.position;
	}

	public void ChangeDir()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			isChangedDir = true;
			Vector3 hitPoint;
			if (isClickGround(out hitPoint))
			{
				showClickEffect(hitPoint);
			}
		}
		
		if (Input.GetMouseButtonUp (0))
			isChangedDir = false;
		
		if (isChangedDir) {
			Vector3 hitPoint;
			if (isClickGround (out hitPoint))
				LookAt (hitPoint);
		} else if (move.isMoving)
			LookAt (targetPos);
	}

	//判断是否点击到地面，返回场景中点击位置
	bool isClickGround(out Vector3 hitPoint)
	{
		//从屏幕鼠标的位置发出一条射线
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		//保存射线碰撞信息
		RaycastHit hitInfor;
		//发出射线，看是否能碰到东西
//		bool isCollider = Physics.Raycast (ray, out hitInfor, Mathf.Infinity,1 << LayerMask.NameToLayer("Ground"));
		bool isCollider = Physics.Raycast (ray, out hitInfor);
		hitPoint = hitInfor.point;

		if (isCollider && hitInfor.collider.tag == Tags.ground)
			return true;
		else
			return false;
	}

	//在点击位置生成点击效果
	void showClickEffect(Vector3 hitPoint)
	{
		hitPoint = new Vector3 (hitPoint.x, hitPoint.y + 1f, hitPoint.z);
		Instantiate (clickEffectPrefabs, hitPoint, Quaternion.identity);
	}

	//使人物的本地Z轴朝向点击的位置
	void LookAt(Vector3 hitPoint)
	{
		targetPos = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
		transform.LookAt (targetPos);
	}
}
