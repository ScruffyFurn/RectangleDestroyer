using UnityEngine;
using System.Collections;

public class SmoothLookAt : MonoBehaviour 
{
public Transform target;
public float damping= 6.0f;
public bool smooth= true;

	void  Start ()
	{
   		if (rigidbody)
		{
			rigidbody.freezeRotation = true;
		}
	}
	
	void  LateUpdate ()
	{
		if (target) 
		{
			if (smooth)
			{
				Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
			}
			else
			{
			    transform.LookAt(target);
			}
		}
	}
}