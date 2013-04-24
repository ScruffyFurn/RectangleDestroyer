using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	private GameObject camera;
	public GameObject chooseLevel;
	public GameObject controls;
	public GameObject credit;
	public bool Play = false;
	public bool ChooseLevel = false;
	public bool Controls = false;
	public bool Credit = false;
	public bool Quit = false;
	public AudioClip SelectSound;
	public AudioClip SelectDownSound;

		
	void Start()
	{
		camera = GameObject.Find("Main Camera");
		chooseLevel = GameObject.Find("ChooseLevel");
		controls = GameObject.Find("Controls");
		credit = GameObject.Find("Credit");
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
							
					        if (Play) 
							{
								Application.LoadLevel("Level 1");
							}
							else if (ChooseLevel) 
							{
								RotateCameraChooseLevel();
							}
							else if (Controls) 
							{
								RotateCameraControls();
							}
							else if (Credit) 
							{
								RotateCameraCredit();
							} 
							else if (Quit) 
							{
								Application.Quit();
							}
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
		if (Play) 
		{
			renderer.material.color = Color.blue;
		}
		else if (ChooseLevel) 
		{
			renderer.material.color = Color.green;
		}
		else if (Controls) 
		{
			renderer.material.color = Color.yellow;
		}
		else if (Credit) 
		{
			renderer.material.color = Color.cyan;
		}
		else if (Quit) 
		{
			renderer.material.color = Color.red;
		}
    }
	void OnMouseExit() 
	{
        if (Play) 
		{
			renderer.material.color = Color.white;
		}
		else if (ChooseLevel) 
		{
			renderer.material.color = Color.white;
		}
		else if (Controls) 
		{
			renderer.material.color = Color.white;
		}
		else if (Credit) 
		{
			renderer.material.color = Color.white;
		} 
		else if (Quit) 
		{
			renderer.material.color = Color.white;
		}
    }
	void OnMouseDown() 
	{
		audio.clip = SelectDownSound;
		audio.Play();
		
        if (Play) 
		{
			Application.LoadLevel("Level 1");
		}
		else if (ChooseLevel) 
		{
			RotateCameraChooseLevel();
		}
		else if (Controls) 
		{
			RotateCameraControls();
		}
		else if (Credit) 
		{
			RotateCameraCredit();
		} 
		else if (Quit) 
		{
			Application.Quit();
		}
    }
	
	void RotateCameraChooseLevel()
	{
		camera.GetComponent<SmoothLookAt>().target = chooseLevel.transform;
	}
	void RotateCameraControls()
	{
		camera.GetComponent<SmoothLookAt>().target = controls.transform;
	}
	void RotateCameraCredit()
	{
		camera.GetComponent<SmoothLookAt>().target = credit.transform;
	}
}
