using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Animator))]

public class EnemyStateListner : MonoBehaviour {
	public float enemyRunSpeed=3f;
	private EnemyStateController.enemiesStates currentState= EnemyStateController.enemiesStates.idle;

	void OnEnable()
	{
		EnemyStateController.onStateChange += onStateChange;
	}
	void OnDisable()
	{
		EnemyStateController.onStateChange -= onStateChange;
	}
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		onStateCycle();
	}
	void onStateCycle()
	{
		Vector3 localScale= transform.localScale;
		switch(currentState)
		{

		case EnemyStateController.enemiesStates.idle:
			break;
		case EnemyStateController.enemiesStates.run:
			transform.Translate(new Vector3(enemyRunSpeed * Time.deltaTime, 0.0f, 0.0f));
			if(localScale.x < 0.0f)
			{
				localScale.x *= -1.0f;
				transform.localScale  = localScale;
			}
			break;
		case EnemyStateController.enemiesStates.die:
			break;
		}

	}
	void onStateChange(EnemyStateController.enemiesStates newState)
	{
		if(newState == currentState)
			return;
		switch(newState)
		{
		case EnemyStateController.enemiesStates.idle:
			break;
		case EnemyStateController.enemiesStates.run:
			break;
		case EnemyStateController.enemiesStates.die:
			break;

		}
		currentState = newState;
	}
}
