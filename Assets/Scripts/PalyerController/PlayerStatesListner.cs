using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Animator))]

public class PlayerStatesListner : MonoBehaviour {
	public float playerWalkSpeed = 3f;
	public float playerJumpForceVertical = 500f;
	public float playerJumpForceHorizontal = 500f;
	private Animator playerAnimator = null;
	public bool playerHasLanded=true;
	int throwHash = Animator.StringToHash("Throw");

	public GameObject knifePrefb=null;
	public Transform knifeSpawnPoint=null;
	private PlayerStatesController.playerStates previousState = PlayerStatesController.playerStates.idle;
	private PlayerStatesController.playerStates currentState = PlayerStatesController.playerStates.idle;
	void OnEnable()
	{
		PlayerStatesController.onStateChange += onStateChange;
	}
	
	void OnDisable()
	{
		PlayerStatesController.onStateChange -= onStateChange;
	}
	void Start()
	{
		playerAnimator= GetComponent<Animator>();
		PlayerStatesController.stateDelayTimer[(int)PlayerStatesController.playerStates.jump]=1.0f;
		PlayerStatesController.stateDelayTimer[(int)PlayerStatesController.playerStates.fire]=1.0f;
	}
	void LateUpdate()
	{
		
		onStateCycle();
	}

	void onStateCycle()
	{
		Vector3 localScale = transform.localScale;
		switch(currentState)
		{
		case PlayerStatesController.playerStates.idle:
			GetComponent<Rigidbody2D>().velocity=-Vector3.zero;
			break;
		case PlayerStatesController.playerStates.left:
			GetComponent<Rigidbody2D>().velocity=-Vector3.right * playerWalkSpeed;
			//transform.Translate(new Vector3((playerWalkSpeed * -1.0f) * Time.deltaTime, 0.0f, 0.0f));
			if(localScale.x > 0.0f)
			{
				localScale.x *= -1.0f;
				transform.localScale  = localScale;
			}
			break;
		case PlayerStatesController.playerStates.right:
			GetComponent<Rigidbody2D>().velocity=Vector3.right * playerWalkSpeed;
			//transform.Translate(new Vector3(playerWalkSpeed * Time.deltaTime, 0.0f, 0.0f));
			if(localScale.x < 0.0f)
			{
				localScale.x *= -1.0f;
				transform.localScale  = localScale;
			}
			break;
		case PlayerStatesController.playerStates.jump:

			break;
		case PlayerStatesController.playerStates.landing:
			break;
		case PlayerStatesController.playerStates.fire:
			break;
		case PlayerStatesController.playerStates.slide:
			break;
		case PlayerStatesController.playerStates.die:
			break;
		case PlayerStatesController.playerStates.resurrect:
			break;
		}
	}
	public void onStateChange(PlayerStatesController.playerStates newState)
	{
		if(newState == currentState)
			return;

		if(checkIfAbortOnStateCondition(newState))
			return;
		
		
		// Check if the current state is allowed to transition into this state. If it's not, abort.
		if(!checkForValidStatePair(newState))
			return;  
			

		switch(newState)
		{
		case PlayerStatesController.playerStates.idle:
			playerAnimator.SetBool("run",false);
			playerAnimator.SetBool("throw",false);


			break;
		case PlayerStatesController.playerStates.left:
			playerAnimator.SetBool("run",true);

			break;
		case PlayerStatesController.playerStates.right:
			playerAnimator.SetBool("run",true);

			break;
		case PlayerStatesController.playerStates.jump:
			if(playerHasLanded)
			{
			
				playerAnimator.SetBool("jump",true);
				float jumpDirection;
				if(currentState==PlayerStatesController.playerStates.left)
					jumpDirection=-1.0f;
				else if(currentState==PlayerStatesController.playerStates.right)
					jumpDirection=1.0f;
				else
					jumpDirection=0.0f;
				GetComponent<Rigidbody2D>().AddForce(new Vector2(playerJumpForceHorizontal * jumpDirection,playerJumpForceVertical));
				playerHasLanded=false;
				PlayerStatesController.stateDelayTimer[(int)PlayerStatesController.playerStates.jump]=0.0f;
			}
			break;
		case PlayerStatesController.playerStates.landing:
			playerAnimator.SetBool("jump",false);
		
			playerHasLanded=true;
			PlayerStatesController.stateDelayTimer[(int)PlayerStatesController.playerStates.jump]=Time.time + 0.1f;
			break;
		case PlayerStatesController.playerStates.fire:
			Vector3 localScale = transform.localScale;
			playerAnimator.SetTrigger(throwHash);
			Debug.Log("we are in fire mode");
			GameObject newKnife=(GameObject)Instantiate(knifePrefb);
			newKnife.transform.position=knifeSpawnPoint.position;
			if(localScale.x<0f)
			newKnife.transform.Rotate(new Vector3(1,0,0),180f);
			PlayerBulletController knifeCon = newKnife.GetComponent<PlayerBulletController>();
			knifeCon.playerObject=gameObject;
			knifeCon.launchKnife();
			PlayerStatesController.stateDelayTimer[(int)PlayerStatesController.playerStates.fire]=Time.time + 1f;
			onStateChange(previousState);
			break;
		case PlayerStatesController.playerStates.slide:
			break;
		case PlayerStatesController.playerStates.die:
			break;
		case PlayerStatesController.playerStates.born:
			break;
		case PlayerStatesController.playerStates.resurrect:
			break;
		}
		previousState = currentState;
		currentState = newState;
	}
	bool checkIfAbortOnStateCondition(PlayerStatesController.playerStates newState)
	{
		bool returnVal=false;
		switch(newState)
		{
		case PlayerStatesController.playerStates.idle:

			break;
		case PlayerStatesController.playerStates.left:

			break;
		case PlayerStatesController.playerStates.right:

			break;
		case PlayerStatesController.playerStates.jump:
			float nextJumpAllow=PlayerStatesController.stateDelayTimer[(int)PlayerStatesController.playerStates.jump];
			if(nextJumpAllow==0.0f||nextJumpAllow>Time.time)
				returnVal =true;
			break;
		case PlayerStatesController.playerStates.landing:
			break;
		case PlayerStatesController.playerStates.fire:
			if(PlayerStatesController.stateDelayTimer[(int)PlayerStatesController.playerStates.fire]>Time.time)
			{

				returnVal=true;
			}
			
			break;
		case PlayerStatesController.playerStates.slide:
			break;
		case PlayerStatesController.playerStates.die:
			break;
		case PlayerStatesController.playerStates.born:
			break;
		case PlayerStatesController.playerStates.resurrect:
			break;
		}
		return returnVal;
	}
	bool checkForValidStatePair(PlayerStatesController.playerStates newState)
	{
		bool returnVal=false;
		switch(currentState)
		{
		case PlayerStatesController.playerStates.idle:
			returnVal = true;
			break;
		case PlayerStatesController.playerStates.left:
			returnVal = true;
			break;
		case PlayerStatesController.playerStates.right:
			returnVal = true;
			break;
		case PlayerStatesController.playerStates.jump:
			if(newState==PlayerStatesController.playerStates.landing||
			   newState==PlayerStatesController.playerStates.die||
			   newState==PlayerStatesController.playerStates.fire)
				returnVal=true;
			else
				returnVal=false;
			break;
		case PlayerStatesController.playerStates.landing:
			if(newState==PlayerStatesController.playerStates.idle||
			   newState==PlayerStatesController.playerStates.left||
			   newState==PlayerStatesController.playerStates.right||
			   newState==PlayerStatesController.playerStates.fire
			   )
			{
				returnVal=true;
			}else
				returnVal=false;
			break;
		case PlayerStatesController.playerStates.fire:

			returnVal = true;
			break;
		case PlayerStatesController.playerStates.slide:
			break;
		case PlayerStatesController.playerStates.die:
			if(newState==PlayerStatesController.playerStates.idle)
				returnVal=true;
			else
				returnVal=false;
			break;
		case PlayerStatesController.playerStates.born:
			if(newState==PlayerStatesController.playerStates.idle)
				returnVal=true;
			else
				returnVal=false;
			break;
		case PlayerStatesController.playerStates.resurrect:
			if(newState==PlayerStatesController.playerStates.idle)
				returnVal=true;
			else
				returnVal=false;
			break;

		}
		return returnVal;
	}

}
