using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum Team
{
	Red, Blue
}

public class Unit : MonoBehaviour {

	public float hp;
	public GameObject deadEffect;
	public Slider hpBar;
	public float deadEffectTime;
	public Team team;

	protected  Weapon weapon;

	private float curHp;
	private GameObject gameController;
	private GameController gameOver;

	public void Start () 
	{
		curHp = hp;
		gameController = GameObject.FindGameObjectWithTag (Tags.gameController);
		gameOver = gameController.GetComponent<GameController>();
	}

	void OnMouseEnter()
	{
		if (Team.Blue == team)
			CursorManager._instance.SetAttackCursor ();
	}
	
	void OnMouseExit()
	{
		if (Team.Blue == team)
			CursorManager._instance.SetNormalCursor ();
	}

	public void ApplyDemage(float demage)
	{
		hpBar.gameObject.SetActive (true);
		if (curHp > demage) 
		{
			curHp -= demage;
			hpBar.value = curHp / hp;
		}
		else 
		{
			hpBar.value = 0;
			GetGameResult();
			Destruct();
		}
	}

	void Destruct()
	{
		curHp = 0;

		if (deadEffect) 
		{
			GameObject newDeadEffect = Instantiate (deadEffect, transform.position, transform.rotation) as GameObject;
			Destroy(newDeadEffect ,deadEffectTime);
		}
		Destroy(gameObject);
	}

	void GetGameResult()
	{
		//主角都死了，当然GameOver啦
		if (gameObject.tag == Tags.player || gameObject.tag == Tags.redBaseBuilding || gameObject.tag == Tags.blueBaseBuilding) 
		{
			gameOver.SetGameOver();
			gameOver.DisplayCanvas();
			if (gameObject.tag == Tags.blueBaseBuilding)
			{
				gameOver.DisplayWinText();
			}
			else
			{
				gameOver.DisplayLoseText();
			}
		}
	}	
}
