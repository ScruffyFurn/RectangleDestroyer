using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
        BreakoutGame.SP.HitBlock();
        Destroy(gameObject);
		Rigidbody rigid = other.rigidbody;
        float xDistance = rigid.position.x - transform.position.x;
        rigid.velocity = new Vector3(rigid.velocity.x + xDistance/2, rigid.velocity.y, -rigid.velocity.z);
	}
}
