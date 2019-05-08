using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public PlayerStatesController.playerStates currentPlayerState =  PlayerStatesController.playerStates.idle;
	public GameObject playerObject = null; 
	public float cameraTrackingSpeed = 0.2f; 
	private Vector3 lastTargetPosition = Vector3.zero; 
	private Vector3 currTargetPosition = Vector3.zero; 
	private float currLerpDistance = 0.0f; 

	// Use this for initialization
	void Start () {
		Vector3 playerPos = playerObject.transform.position;        
		Vector3 cameraPos = transform.position;       
		Vector3 startTargPos = playerPos;        

		//Set the Z to the same as the camera so it does not move       
		startTargPos.z = cameraPos.z;       
		lastTargetPosition = startTargPos;        
		currTargetPosition = startTargPos;        
		currLerpDistance = 1.0f; 
	}
	void OnEnable()
	{
		PlayerStatesController.onStateChange +=onPlayerStateChange;
	}
	void onDisable()
	{
		PlayerStatesController.onStateChange -=onPlayerStateChange;
	}
	void onPlayerStateChange(PlayerStatesController.playerStates newState)
	{
		currentPlayerState = newState;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		onStateCycle();
		currLerpDistance += cameraTrackingSpeed;        
		transform.position = Vector3.Lerp(lastTargetPosition,currTargetPosition, currLerpDistance); 
	}
	void onStateCycle()
	{
		switch(currentPlayerState)
		{
		case PlayerStatesController.playerStates.idle:
			trackPlayer();
			break;
		case PlayerStatesController.playerStates.left:
			trackPlayer();
			break;
		case PlayerStatesController.playerStates.right:
			trackPlayer();
			break;
		case PlayerStatesController.playerStates.jump:
			StopTrackingPlayer ();
			break;

		}
	}
	void trackPlayer()
	{
		Vector3 currCamPosition = transform.position;
		Vector3 currPlayerPosition =playerObject.transform.position;
		if(currCamPosition.x==currPlayerPosition.x && currCamPosition.y==currPlayerPosition.y )
		{
			currLerpDistance = 1f;            
			lastTargetPosition = currCamPosition;            
			currTargetPosition = currCamPosition;            
			return; 
		}
		currLerpDistance=0f;
		currTargetPosition =currPlayerPosition;
		currTargetPosition.z=currCamPosition.z;
	}
	void StopTrackingPlayer()
	{
		Vector3 currCamPosition = transform.position;
		currTargetPosition = currCamPosition;  
		lastTargetPosition = currCamPosition; 

		currLerpDistance=1.0f;

	}
}
