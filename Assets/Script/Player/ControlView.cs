/// <summary>
/// 控制视野进行人物跟随，
/// 右击进行视野围绕人物进行上下左右旋转
/// 滑动滑轮进行视野的拉近拉远
/// </summary>
using UnityEngine;
using System.Collections;

public class ControlView : MonoBehaviour {

	public float rotateSpeed = 1;
	public float scrollSpeed = 10;
	public GameObject player;

	private Vector3 offset;		//视野距离向量
	private bool isRotating = false;
	private float distance = 0;		//视野的距离
	private float maxDistance = 20;
	private float minDistance = 5;

	void Start () 
	{
		if (null == player)
			return;
		transform.LookAt (player.transform.position);
		offset = transform.position - player.transform.position;
	}
	

	void Update () 
	{
		//如果Player没死
		if (null == player)
			return;
		transform.position = player.transform.position + offset;
		RotateView ();
		ScrollView ();
	}

	void RotateView()
	{
		if (Input.GetMouseButtonDown (1))
			isRotating = true;

		if (Input.GetMouseButtonUp (1))
			isRotating = false;

		if (isRotating)
		{
			transform.RotateAround (player.transform.position, player.transform.up, Input.GetAxis("Mouse X") * rotateSpeed);

			//上下旋转前保存相机的位置信息
			Vector3 originalPos = transform.position;
			Quaternion originalRotation = transform.rotation;
			transform.RotateAround(player.transform.position, transform.right, -Input.GetAxis("Mouse Y") * rotateSpeed);

			//限定上下旋转的范围
			float x = transform.eulerAngles.x;

			if (x < 10 || x > 80)
			{
				transform.position = originalPos;
				transform.rotation = originalRotation;
			}
		}
		offset = transform.position - player.transform.position;
	}

	void ScrollView()
	{
		distance = offset.magnitude;
		distance -= Input.GetAxis ("Mouse ScrollWheel");
		distance = Mathf.Clamp (distance, minDistance, maxDistance);
		offset = offset.normalized * distance;
	}
}
