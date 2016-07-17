/// <summary>
/// Weapon.
/// 控制炮弹的发射
/// </summary>
using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public GameObject cannonball;
	public float shootPower = 10;
	public Transform shootPoint;
	public float coolDown = 1;

	private bool isWeaponReady = true;
	private AudioSource audioSource;
	private LayerMask enemyLayer;
		
	public void Start () 
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void Init(Team team)
	{
		enemyLayer = LayerManager.GetEnemyLayer (team);
	}

	public virtual void Attack(GameObject target)
	{

	}

	public virtual void Attack()
	{
		
	}

	public void Shoot()
	{
		if (!isWeaponReady)
			return;
		GameObject newCannonball = Instantiate (cannonball, shootPoint.position, shootPoint.rotation) as GameObject;
		newCannonball.GetComponent<Cannonball> ().Init (enemyLayer);
		Rigidbody rigid = newCannonball.GetComponent<Rigidbody> ();
		rigid.velocity = shootPoint.forward * shootPower;
		audioSource.Play ();
		isWeaponReady = false;
		StartCoroutine (WeaponCoolDown ());
	}

	IEnumerator WeaponCoolDown()
	{
		yield return new WaitForSeconds (coolDown);
		isWeaponReady = true;
	}

}
