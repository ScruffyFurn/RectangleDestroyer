using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{

	void OnTriggerEnter (Collider other)
	{
		audio.clip = BreakoutGame.SP.BrickSound;
		audio.Play();
        BreakoutGame.SP.HitBlock();
		DestroyObject(gameObject, 0.15f);
		Rigidbody rigid = other.rigidbody;
        float xDistance = rigid.position.x - transform.position.x;
        rigid.velocity = new Vector3(rigid.velocity.x + xDistance/2, rigid.velocity.y, -rigid.velocity.z);
	}
	
	IEnumerator StoppedTime()
	{
		yield return new WaitForSeconds(2);
	}
}
