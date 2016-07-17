/// <summary>
/// Cannonball.
/// 炮弹碰撞物体后产生爆炸效果和爆炸力场，并把自身和爆炸效果销毁掉
/// </summary>
using UnityEngine;
using System.Collections;

public class Cannonball : MonoBehaviour {

	public GameObject explosionEffect;
	public float explosionTimeOff;
	public float explosionRadius;
	public float explosionForce;
	public float demage;

	private LayerMask enemy;

	public void Init(LayerMask enemyLayer)
	{
		enemy = enemyLayer;
	}

	void OnCollisionEnter()
	{
		GameObject obj = Instantiate (explosionEffect, transform.position, transform.rotation) as GameObject;
		Destroy (gameObject);
		Destroy (obj, explosionTimeOff);
		//爆炸力场并造成伤害
		Collider[] cols = Physics.OverlapSphere (transform.position, explosionRadius, enemy);

		if (cols.Length > 0) 
		{
			for (int i = 0; i < cols.Length; i++) 
			{
				Rigidbody rigid = cols[i].GetComponent<Rigidbody>();

				//如果碰到没有刚体只有碰撞器的不发生爆炸，例如地面
				if (rigid)
					rigid.AddExplosionForce(explosionForce, transform.position, explosionRadius);

				Unit unit = cols[i].GetComponent<Unit>();
				if (unit)
				{
					Debug.Log("扣血");
					unit.ApplyDemage(demage);
				}
			}
		}
	}
}
