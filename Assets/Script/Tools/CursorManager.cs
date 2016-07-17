using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour
{
	public static CursorManager _instance;
	public Texture2D cursorNormal;
	public Texture2D cursorAttack;

	private Vector2 hotspot = Vector2.zero;
	private CursorMode cursormode = CursorMode.Auto;

	void Start()
	{
		_instance = this;
	}

	public void SetNormalCursor()
	{
		//SetCursor (cursorNormal, hotspot, cursormode);
		//cursorNormal 是 Texture2D 的图片
		//hotspot 是指定图片的哪个点为焦点
		//cursormode 是指定鼠标指针是哪一个
		Cursor.SetCursor (cursorNormal, hotspot, cursormode);
	}

	public void SetAttackCursor()
	{
		Cursor.SetCursor (cursorAttack, hotspot, cursormode);
	}
}

