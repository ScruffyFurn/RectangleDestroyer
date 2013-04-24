using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour 
{
	public AudioClip SelectSound;
	public AudioClip SelectDownSound;
	
	void OnMouseEnter() 
	{
		audio.clip = SelectSound;
		audio.Play(); 
    }
	
	void OnMouseOver() 
	{
		renderer.material.color = Color.cyan;
    }
	void OnMouseExit() 
	{
		renderer.material.color = Color.white;
    }
	void OnMouseDown() 
	{
		audio.clip = SelectDownSound;
		audio.Play();
    }
}
