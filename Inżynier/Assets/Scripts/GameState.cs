using UnityEngine;
using System.Collections;

public enum States { GAMEPLAY, EQUIPMENT, PICKING_UP };

public class GameState : Singleton<GameState> {

	protected GameState() {}

	public States currentState;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
