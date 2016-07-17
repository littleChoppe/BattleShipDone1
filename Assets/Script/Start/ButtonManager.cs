/// <summary>
/// 管理开始场景中各种各样的界面按钮触发的事件
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;


public class ButtonManager : MonoBehaviour {

	public Image GamePanel;
	public Image LoginPanel;
	public InputField userName;
	public InputField password;
	public Text massgae;
	
	public void OnLoginButtonClick()
	{
		if (userName.text == "小鹿" && password.text == "xiaolu")
			MovePanel ();
		else 
			ShowMassage ();

	}

	public void OnExitButtonClick()
	{
		Application.Quit ();
	}

	public void OnStartButtonClick()
	{
		Application.LoadLevel (1);
	}

	public void OnSettingButtonClick()
	{

	}

	void MovePanel()
	{
//		LoginPanel.rectTransform.DOMoveX (1000, 1f);
//		GamePanel.rectTransform.DOMoveX (620, 1f).SetDelay(1f);
		LoginPanel.rectTransform.DOLocalMoveX (610, 1f);
		GamePanel.rectTransform.DOLocalMoveX (243, 1f).SetDelay(1f);
	}

	void ShowMassage()
	{
		massgae.gameObject.SetActive (true);
		StartCoroutine (MassageDisappear());
	}

	IEnumerator MassageDisappear()
	{
		yield return new WaitForSeconds (2);
		massgae.gameObject.SetActive (false);
	}

}
