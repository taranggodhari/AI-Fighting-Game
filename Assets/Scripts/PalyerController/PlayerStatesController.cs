using UnityEngine;
using System.Collections;

public class PlayerStatesController : MonoBehaviour {

	public enum playerStates{
		idle=0,
		left,
		right,
		jump,
		landing,
		fire,
		slide,
		die,
		born,
		resurrect,
		_stateCount
	}
	public static float[] stateDelayTimer = new float[(int)PlayerStatesController.playerStates._stateCount];
	public delegate void playerStateHandler(PlayerStatesController.playerStates newState);
	public static event playerStateHandler onStateChange; 
	void LateUpdate(){
		float horizontal = Input.GetAxis("Horizontal");
		float jump = Input.GetAxis("Jump"); 
		float firing=Input.GetAxis("Fire1");
		if(firing > 0.0f) 
		{ 
			if(onStateChange != null)onStateChange(PlayerStatesController.playerStates.fire); 
		}
		if(jump > 0.0f) 
		{ 
			if(onStateChange != null)onStateChange(PlayerStatesController.playerStates.jump); 
		}
		if(horizontal!=0){
			if(horizontal<0){
				if(onStateChange != null )onStateChange(PlayerStatesController.playerStates.left);

			}else{
				if(onStateChange != null )onStateChange(PlayerStatesController.playerStates.right);
			}
		}else{
			if(onStateChange != null )onStateChange(PlayerStatesController.playerStates.idle);
		}

	}
	public void DoJump()
	{
	
		if(onStateChange != null )onStateChange(PlayerStatesController.playerStates.fire);
	}

}