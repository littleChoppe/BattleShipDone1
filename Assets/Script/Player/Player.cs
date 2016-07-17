using UnityEngine;
using System.Collections;

public class Player : Unit {

	private PlayerDir dir;
	private PlayerMove move;

	void Start () 
	{
		base.Start ();
		dir = GetComponent<PlayerDir>();
		move = GetComponent<PlayerMove>();
		weapon = GetComponent<PlayerWeapon>();
		weapon.Init (team);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		dir.ChangeDir ();
		move.Move ();
		weapon.Attack ();
	}
}
