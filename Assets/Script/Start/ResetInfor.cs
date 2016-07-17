/// <summary>
/// 登录界面上的输入框里点击清除按钮进行内容清除
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResetInfor : MonoBehaviour {

	public InputField inputContent;

	public void OnClearButtonClick()
	{
		inputContent.text = "";
	}
}
