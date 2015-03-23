using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// used for navmesh
	NavMeshAgent myNMA;
	// used for player location
	Transform player;
	// array for waypoints
	public Transform[] waypoints;
	// x used for random selection of waypoint
	public int x;
	// mode used to select random behaviour
	public int mode;
	// timer used so different behaviours are only active so long
	public float timer;
	// used to track distance between player and ghost
	public float distance;
	// clock for display
	public int clock;
	// clock in float, to be converted to int
	float clok;
	// different behaviour states and starting state
	public enum States
	{
		Begin,
		Blinky, //Red
		Pinky,  //Pink
		Inky,   //Cyan
		Clyde,  //Orange
	}
	States currentState = States.Begin;
	
	void Start ()
	{
		//get the navmesh from the scene and get players transform
		myNMA = transform.GetComponent<NavMeshAgent>();
		player = GameObject.FindWithTag("Player").transform;
	}
		
	// Gui for displaying timer on how long the player has lasted and distance between player and ghost
	void OnGUI()
	{
		GUILayout.Label( "Survived = " + clock);
		GUILayout.Label( "Distance = " + distance);
	}
	
	void Update ()
	{
		// Start timer counting up
		timer += Time.deltaTime;
		// Declare state machine
		switch(currentState)
		{
			case States.Begin:
				Begin();
				break;
			case States.Blinky:
				Blinky();
				break;
			case States.Pinky:
				Pinky();
				break;
			case States.Inky:
				Inky();
				break;
			case States.Clyde:
				Clyde();
				break;
		}
		// Gets distance between player and ghost. If player is 0.5 or less away restart game
		distance = Vector3.Distance(player.transform.position, transform.position);
		if (distance <= 0.5f)
		{
			Application.LoadLevel(Application.loadedLevelName);
		}
		// Clock started in float and cast to int for display
		clok += Time.deltaTime;
		clock = (int)clok;
		
		//Debug.Log (clock);
	}
	
	// Opening state, set to act as blinky with extended time limit, can also become blinky at time limit
	void Begin()
	{
		// Colour set to red
		renderer.material.color = Color.red;
		// Set to go to player
		myNMA.destination = player.position;
		// When timer reaches 10 will pick a random ghost personality
		// x used to pick random waypoint from array
		if (timer >= 10f)
		{
			timer = 0f;
			mode = Random.Range(1,5);
			if (mode == 1)
			{
				Debug.Log ("Blinky");
				currentState = States.Blinky;
			}
			else if (mode == 2)
			{
				Debug.Log ("Pinky");
				x = Random.Range (0,3);
				currentState = States.Pinky;
			}
			else if (mode == 3)
			{
				Debug.Log ("Inky");
				currentState = States.Inky;
			}
			else if (mode == 4)
			{
				Debug.Log ("Clyde");
				x = Random.Range (0,7);
				currentState = States.Clyde;
			}
			else
			{
				Debug.Log ("Problem Start");
			}
		}
	}
	
	// State for when Blinky is active
	void Blinky()
	{
		// Colour set to red
		renderer.material.color = Color.red;
		// Set to go to player
		myNMA.destination = player.position;
		// When timer reaches 5 will pick a different random ghost personality
		// x used to pick random waypoint from array
		if (timer >= 5f)
		{
			timer = 0f;
			mode = Random.Range(1,4);
			if (mode == 1)
			{
				Debug.Log ("Clyde");
				x = Random.Range (0,7);
				currentState = States.Clyde;
			}
			else if (mode == 2)
			{
				Debug.Log ("Pinky");
				x = Random.Range (0,3);
				currentState = States.Pinky;
			}
			else if (mode == 3)
			{
				Debug.Log ("Inky");
				x = Random.Range (0,7);
				currentState = States.Inky;
			}
			else
			{
				Debug.Log ("Problem Blinky");
			}
		}
	}
	// State for when Pinky is active
	void Pinky()
	{
		// Colour set to pink
		renderer.material.color = new Color(1,0.549f,1);
		// Set to go to randomly chosen waypoint, but only ones at the four corners of the map
		myNMA.destination = waypoints[x].position;
		// If gets within 4 of the player will ignore waypoint and switch to chasing player
		if (distance <= 4f)
		{
			myNMA.destination = player.position;
		}
		// When timer reaches 5 will pick a different random ghost personality
		// x used to pick random waypoint from array
		if (timer >= 5f)
		{
			timer = 0f;
			mode = Random.Range(1,4);
			if (mode == 1)
			{
				Debug.Log ("Clyde");
				x = Random.Range (0,7);
				currentState = States.Clyde;
			}
			else if (mode == 2)
			{
				Debug.Log ("Blinky");
				currentState = States.Blinky;
			}
			else if (mode == 3)
			{
				Debug.Log ("Inky");
				x = Random.Range (0,7);
				currentState = States.Inky;
			}
			else
			{
				Debug.Log ("Problem Pinky");
			}
		}
	}
	// State for when Inky is active
	void Inky()
	{
		// Colour set to cyan
		renderer.material.color = Color.cyan;
		// Set to go to randomly chosen waypoint
		myNMA.destination = waypoints[x].position;
		// If gets within 4 of the player will ignore waypoint and switch to chasing player
		if (distance <= 4f)
		{
			myNMA.destination = player.position;
		}
		// When timer reaches 5 will pick a different random ghost personality
		// x used to pick random waypoint from array
		if (timer >= 5f)
		{
			timer = 0f;
			mode = Random.Range(1,4);
			if (mode == 1)
			{
				Debug.Log ("Blinky");
				currentState = States.Blinky;
			}
			else if (mode == 2)
			{
				Debug.Log ("Pinky");
				x = Random.Range (0,3);
				currentState = States.Pinky;
			}
			else if (mode == 3)
			{
				Debug.Log ("Clyde");
				x = Random.Range (0,7);
				currentState = States.Clyde;
			}
			else
			{
				Debug.Log ("Problem Inky");
			}
		}
	}
	// State for when Clyde is active
	void Clyde()
	{
		// Colour set to orange
		renderer.material.color = new Color(1,0.573f,0);
		// Set to go to randomly chosen waypoint
		myNMA.destination = waypoints[x].position;
		// When timer reaches 5 will pick a different random ghost personality
		// x used to pick random waypoint from array
		if (timer >= 5f)
		{
			timer = 0f;
			mode = Random.Range(1,4);
			if (mode == 1)
			{
				Debug.Log ("Blinky");
				currentState = States.Blinky;
			}
			else if (mode == 2)
			{
				Debug.Log ("Pinky");
				x = Random.Range (0,3);
				currentState = States.Pinky;
			}
			else if (mode == 3)
			{
				Debug.Log ("Inky");
				x = Random.Range (0,7);
				currentState = States.Inky;
			}
			else
			{
				Debug.Log ("Problem Clyde");
			}
		}
	}
}