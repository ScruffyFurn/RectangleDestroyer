using UnityEngine;
using System.Collections;

public enum BreakoutGameState { wait, playing, won, lost };

public class BreakoutGame : MonoBehaviour
{
    public static BreakoutGame SP;
	
	public GUIStyle style;

    public Transform ballPrefab;
	
	public AudioClip BrickSound;
	public AudioClip MetalSound;
	public AudioClip PaddleSound;
	
	//public AudioSource audioSource;

    private int totalBlocks;
    private int blocksHit;
    private BreakoutGameState gameState;
	private int points = 0;
	private int lives = 3;
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
		
    }
	
	void Update()
	{
		if(Input.anyKeyDown && gameState == BreakoutGameState.wait)
		{
			gameState = BreakoutGameState.playing;
		}
		
		if(gameState == BreakoutGameState.wait)
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
				  "Hit: " + blocksHit + "/" + totalBlocks,
				  style);
		
		GUI.Label(new Rect(Screen.width * 0.48f, Screen.height * 0.12f,100,100),
				  "Score: " + points,
				  style);
		
		GUI.Label(new Rect(Screen.width * 0.69f, Screen.height * 0.12f,100,100),
				  "Lives: " + lives,
				  style);
		
		GUILayout.BeginArea(new Rect(Screen.width * 0.5f,Screen.height * 0.5f,100,100));

        if (gameState == BreakoutGameState.lost)
        {
            GUILayout.Label("You Lost!");
            if (GUILayout.Button("Try again"))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        else if (gameState == BreakoutGameState.won)
        {
            GUILayout.Label("You won!");
            if (GUILayout.Button("Play again"))
            {
                Application.LoadLevel(Application.loadedLevel);
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

        
        if (blocksHit >= totalBlocks)
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
}
