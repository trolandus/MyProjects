using UnityEngine;
using System.Collections;

public enum States { GAMEPLAY, EQUIPMENT, PICKING_UP };
public enum BackpackLayers { NONE, CHOOSE_ITEM, ITEM_CHOSEN, OBSERVE_ITEM, DRINK, WRITE_ON_SUBITEM }

public class GameState : Singleton<GameState> {

	protected GameState() {}

	public States currentState;
	public BackpackLayers currentBackpackLayer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
