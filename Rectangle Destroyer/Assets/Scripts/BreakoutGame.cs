using UnityEngine;
using System.Collections;

public enum BreakoutGameState { wait, playing, won, lost, pause };

public class BreakoutGame : MonoBehaviour
{
    public static BreakoutGame SP;
	public bool snapView = false;
	public GUIStyle style;

    public Transform ballPrefab;
	
	public AudioClip BrickSound;
	public AudioClip MetalSound;
	public AudioClip PaddleSound;

    private int totalBlocks;
    private int blocksHit;
    private BreakoutGameState gameState;
	private int points = 0;
	private int lives = 3;
	private int levelCounter;
	GameObject paddle; 


    void Awake()
    {
        SP = this;
        blocksHit = 0;
        gameState = BreakoutGameState.wait;
        totalBlocks = GameObject.FindGameObjectsWithTag("Pickup").Length;
        Time.timeScale = 1.0f;
        paddle = GameObject.FindGameObjectWithTag("Paddle");
		SpawnBall();
		levelCounter = Application.loadedLevel;
		
    }
	
	void Update()
	{
		if(snapView)
		{
			gameState = BreakoutGameState.pause;
		}
		
		if(gameState == BreakoutGameState.pause && !snapView)
		{
			if(Input.anyKeyDown)
			{
				gameState = BreakoutGameState.wait;
			}
		}
		
		if((Input.anyKeyDown || 
		   (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Horizontal") != 0)) 
			&& gameState == BreakoutGameState.wait)
		{
			gameState = BreakoutGameState.playing;
		}
		
		if(gameState == BreakoutGameState.wait || gameState == BreakoutGameState.pause)
		{
			Time.timeScale = 0.0f; //Pause game
		}
		else
		{
			Time.timeScale = 1.0f;
		}
	}

    void SpawnBall()
    {
        Instantiate(ballPrefab,
					new Vector3(
								paddle.transform.position.x,
								paddle.transform.position.y,
								paddle.transform.position.z + 1), 
					Quaternion.identity);
    }

    void OnGUI(){
    
		GUI.Label(new Rect(Screen.width * 0.25f,Screen.height * 0.12f,200,100),
				  "Hit: " + blocksHit + " " + " of " + " " + (int)(totalBlocks * 0.75f),
				  style);
		
		GUI.Label(new Rect(Screen.width * 0.48f, Screen.height * 0.12f,100,100),
				  "Score: " + points,
				  style);
		
		GUI.Label(new Rect(Screen.width * 0.69f, Screen.height * 0.12f,100,100),
				  "Lives: " + lives,
				  style);
		
		GUILayout.BeginArea(new Rect(Screen.width * 0.5f,Screen.height * 0.5f,160,400));

        if (gameState == BreakoutGameState.lost)
        {
            GUILayout.Label("You Lost",style);
            if (GUILayout.Button("Try again"))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        else if (gameState == BreakoutGameState.won)
        {
			
            GUILayout.Label("You won",style);
            if (GUILayout.Button("Next Level"))
            {
				levelCounter++;
                Application.LoadLevel(levelCounter);
            }
        }
		
		GUILayout.EndArea();
    }

    public void HitBlock()
    {	
        blocksHit++;
		points += 100;
		
        //For fun:
        if (blocksHit%10 == 0) //Every 10th block will spawn a new ball
        {
            SpawnBall();
        }

        
        if (blocksHit >= totalBlocks * 0.75)
        {
            WonGame();
        }
    }

    public void WonGame()
    {
        Time.timeScale = 0.0f; //Pause game
        gameState = BreakoutGameState.won;
		
    }

    public void LostBall()
    {
        int ballsLeft = GameObject.FindGameObjectsWithTag("Player").Length;
        if(ballsLeft<=1)
		{
            //Was the last ball..
            LoseLive();
        }
    }
	
	public void LoseLive()
	{
		lives--;
		gameState = BreakoutGameState.wait;
		
		if(lives < 1)
		{
        	SetGameOver();
		}
		else
		{
			SpawnBall();
		}
		
	}

    public void SetGameOver()
    {
		
        Time.timeScale = 0.0f; //Pause game
        gameState = BreakoutGameState.lost;
    }

	public static void Quit()
	{
		Application.Quit();
	}
}
