using UnityEngine;
using System.Collections;

public class PlayerBulletController : MonoBehaviour {
	public GameObject playerObject=null;
	public float knifeSpeed;

	private float selfDestructTimer = 1f;
	public float selfDestruct_Time;
	
	void Update()
	{
		if(selfDestructTimer > 0.0f)
		{
			if(selfDestructTimer < Time.time)
				Destroy(gameObject);
		}
	}
	public void launchKnife()
	{
		float minScale=playerObject.transform.localScale.x;
		Vector2 knifeForce;
	
		if(minScale<0.0f)
		{

			knifeForce=new Vector2(knifeSpeed * -1,0.0f);
		
		}else
		{

			knifeForce=new Vector2(knifeSpeed,0.0f);
		}
		GetComponent<Rigidbody2D>().velocity = knifeForce; 
		selfDestructTimer = Time.time + selfDestruct_Time;
	}
}
