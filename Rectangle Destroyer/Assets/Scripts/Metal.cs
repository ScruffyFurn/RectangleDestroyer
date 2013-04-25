using UnityEngine;
using System.Collections;

public class Metal : MonoBehaviour
{

	void OnTriggerEnter (Collider other)
	{
		audio.clip = BreakoutGame.SP.BrickSound;
		audio.Play();
		Rigidbody rigid = other.rigidbody;
        float xDistance = rigid.position.x - transform.position.x;
        rigid.velocity = new Vector3(rigid.velocity.x + xDistance/2, rigid.velocity.y, -rigid.velocity.z);
	}
}
