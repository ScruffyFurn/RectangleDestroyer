using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

    public float moveSpeed = 15;
		
	void Update () 
	{
        float moveInput = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        transform.position += new Vector3(moveInput, 0, 0);
		
		float moveInputMouse = Input.GetAxis ("Mouse X");// * Time.deltaTime * moveSpeed;
		transform.position += new Vector3(moveInputMouse, 0, 0);
		
        float max = 16.0f;
        if (transform.position.x <= -max || transform.position.x >= max)
        {
            float xPos = Mathf.Clamp(transform.position.x, -max, max); //Clamp between min -5 and max 5
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        }
	}

    void OnCollisionExit(Collision collisionInfo ) 
	{
		audio.clip = BreakoutGame.SP.PaddleSound;
		audio.Play();
		
        //Add X velocity..otherwise the ball would only go up&down
        Rigidbody rigid = collisionInfo.rigidbody;
        float xDistance = rigid.position.x - transform.position.x;
        rigid.velocity = new Vector3(rigid.velocity.x + xDistance/2, rigid.velocity.y, rigid.velocity.z);
    }
}
