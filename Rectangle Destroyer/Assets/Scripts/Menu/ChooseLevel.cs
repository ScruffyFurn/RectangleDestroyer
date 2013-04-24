using UnityEngine;
using System.Collections;

public class ChooseLevel : MonoBehaviour
{
	private GameObject[] Coin;
	public string LevelName;
	public AudioClip SelectSound;
	public AudioClip SelectDownSound;
	
	void Start () 
	{
		LevelName = GetComponent<TextMesh>().text;
	}
	
	void Update()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			RaycastHit hit = new RaycastHit();
			for (int i = 0; i < Input.touchCount; ++i)
			{
				if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
				{
					Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
					if (Physics.Raycast(ray, out hit))
					{
						if (hit.transform.gameObject == gameObject)
						{
							audio.clip = SelectDownSound;
							audio.Play();
							
							Application.LoadLevel(LevelName);
						}
					}
				}
			}
		}
	}
	
	void OnMouseEnter() 
	{
		audio.clip = SelectSound;
		audio.Play(); 
    }
	
	void OnMouseOver() 
	{		
		renderer.material.color = Color.green;
    }
	void OnMouseExit() 
	{
		renderer.material.color = Color.white;
    }
	void OnMouseDown() 
	{
		audio.clip = SelectDownSound;
		audio.Play();
		
		Application.LoadLevel(LevelName);
    }
}
