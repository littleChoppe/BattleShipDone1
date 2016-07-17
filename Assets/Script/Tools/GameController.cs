/// <summary>
/// Game controller.
/// 监听游戏结束
/// 返回游戏情况
/// 设置游戏情况
/// 对不同的游戏结束显示不同的界面
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public Canvas cav;
	public Text winText;
	public Text loseText;

	private bool isGameOver = false;

	public bool GameIsOver()
	{
		return isGameOver;
	}

	public void SetGameOver()
	{
		isGameOver = true;
	}

	public void DisplayCanvas()
	{
		cav.gameObject.SetActive (true);
	}
	public void DisplayWinText()
	{
		winText.gameObject.SetActive (true);
	}

	public void DisplayLoseText()
	{
		loseText.gameObject.SetActive (true);
	}

	public void OnNewGameBtnClick()
	{
		Application.LoadLevel (1);
	}
}
