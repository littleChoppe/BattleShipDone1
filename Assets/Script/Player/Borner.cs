/// <summary>
/// Borner.
/// 只要游戏未结束生成一波又一波的ShipAI
/// </summary>
using UnityEngine;
using System.Collections;

public class Borner : MonoBehaviour {

	public GameObject ship;
	public Transform bornPoint;
	public int shipCount;		//一波生成多少个
	public float startWait;
	public float waveWait;	//每一波相隔的时间
	public float countWait;		//生成每一个相隔时间

	private GameObject gameController;
	private GameController gameOver;
	
	void Start () 
	{
		gameController = GameObject.FindGameObjectWithTag (Tags.gameController);
		gameOver = gameController.GetComponent<GameController>();
		StartCoroutine (SpawnWave());
	}
	
	IEnumerator SpawnWave()
	{
		yield return new WaitForSeconds (startWait);

		//只要游戏没结束就一直生成
		while (!gameOver.GameIsOver()) 
		{
			for(int i = 0 ; i < shipCount; i++)
			{
				GameObject  newShip = Instantiate(ship, bornPoint.position + new Vector3(0, 0.2f, 0), bornPoint.rotation) as GameObject;
				newShip.transform .Rotate(Vector3.up * (-90));

				yield return new WaitForSeconds(countWait);
			}

			yield return new WaitForSeconds(waveWait);
		}
	}
	
}
