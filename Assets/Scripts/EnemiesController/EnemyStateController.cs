using UnityEngine;
using System.Collections;

public class EnemyStateController : MonoBehaviour {

	public enum enemiesStates{
		idle=0,
		run,
		die,
		_stateCount
	}
	public delegate void EnemyStateHandler(EnemyStateController.enemiesStates newState);
	public static event EnemyStateHandler onStateChange;
	void LateUpdate()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float jump = Input.GetAxis("Jump"); 
		float firing=Input.GetAxis("Fire1");
		if(horizontal > 0.0f) 
		{ 
			if(onStateChange != null)onStateChange(EnemyStateController.enemiesStates.run); 
		}else
		{
			if(onStateChange != null)onStateChange(EnemyStateController.enemiesStates.idle); 
		}
	}

}
