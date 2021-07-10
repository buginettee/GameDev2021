using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
	// for Scoring system
	public int currentScore;
	public string currentStatus; //world or dead
	int currentPlayerHealth;
	public bool collectedOrange;
	public bool collectedRed;

	// for Reset values
	Vector3 gombaSpawnPointStart = new Vector3(2.5f, -0.45f, 0); // hardcoded location
																 // .. other reset values 
	
	// for Consume.cs
	public int consumeTimeStep = 10;
	public int consumeLargestScale = 4;

	// for Break.cs
	public int breakTimeStep = 30;
	public int breakDebrisTorque = 10;
	public int breakDebrisForce = 10;

	// for SpawnDebris.cs
	public int spawnNumberOfDebris = 10;

	// for Rotator.cs
	public int rotatorRotateSpeed = 6;

	// for testing
	public int testValue;

	// for PlayerController.cs
	public float startX = -3.25f;
	public float startY = -3.45f;
	public float maxSpeed = 8;
	public float speed = 20;
	public float upSpeed = 16;
	public bool isAlive = true;

	// for enemy controller in prefab
	public float maxOffset = 3.5f;
	public float enemyPatrolTime = 2.0f;
	public float groundSurface = -3.6f;
}
