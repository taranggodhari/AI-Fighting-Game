using UnityEngine;
using System.Collections;

public class ScrollingBackgrounds : MonoBehaviour {
	public float backgroundSize;
	public float parallexSpeed;
	private Transform CameraTransform;
	private float lastCameraX;
	private Transform[] layers;
	private float ViewZone=10;
	private int leftIndex;
	private int rightIndex;



	// Use this for initialization
	void Start () {
		CameraTransform=Camera.main.transform;
		lastCameraX=CameraTransform.position.x;
		layers=new Transform[transform.childCount];
		for(int i=0;i<transform.childCount;i++)
		{
			layers[i]=transform.GetChild(i);
		}
		leftIndex=0;

		rightIndex=layers.Length-1;

	}

	// Update is called once per frame
	void Update () {

		float deltaX= CameraTransform.position.x - lastCameraX;
		transform.position += Vector3.right * (deltaX * parallexSpeed);
		lastCameraX =CameraTransform.position.x;
	if(CameraTransform.position.x < (layers[leftIndex].position.x + ViewZone))
			ScrollLeft();
	if(CameraTransform.position.x > (layers[rightIndex].position.x + ViewZone))
			ScrollRight();
	}

	void ScrollLeft()
	{
		int lastRight =rightIndex;
		layers[rightIndex].position=Vector3.right * (layers[leftIndex].position.x - backgroundSize);
		leftIndex=rightIndex;
		rightIndex--;
		if(rightIndex<0)
			rightIndex=layers.Length-1;
	}

	void ScrollRight()
	{
		int lastLeft =leftIndex;
		layers[leftIndex].position=Vector3.right * (layers[rightIndex].position.x + backgroundSize);
		rightIndex=leftIndex;
		leftIndex++;
		if(leftIndex==layers.Length)
			leftIndex=0;
	}
}
