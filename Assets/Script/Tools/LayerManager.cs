using UnityEngine;
using System.Collections;

public class LayerManager : MonoBehaviour {

	public static int redLayer = 10;
	public static int blueLayer = 11;

	public static LayerMask GetEnemyLayer(Team team)
	{
		LayerMask mask = 0;
		switch (team) 
		{
		case Team.Blue:
			mask = 1 << redLayer;
			break;

		case Team.Red:
			mask = 1 << blueLayer;
			break;
		}

		Debug.Log (mask.value);
		return mask;
	}

}
