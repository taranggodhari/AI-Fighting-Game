using UnityEngine;
using System.Collections;

public class PlayerColliderListner : MonoBehaviour {

	public PlayerStatesListner targetStateListner =null;

	void OnTriggerEnter2D(Collider2D collidedObject)
	{
		switch(collidedObject.tag)
		{
		case "platform":
			targetStateListner.onStateChange(PlayerStatesController.playerStates.landing);
			break;
		}
	}
}
