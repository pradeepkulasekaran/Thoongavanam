using UnityEngine;
using System.Collections;

public class TopDownCamera : MonoBehaviour 
{
 	public GameObject player;
	//The offset of the camera to centrate the player in the X axis
	public float offsetX = 0;
	//The offset of the camera to centrate the player in the Z axis
	public float offsetZ = -7;
	//The offset of the camera to centrate the player in the Y axis
	public float offsetY = 3;
	//The maximum distance permited to the camera to be far from the player, its used to     make a smooth movement
	public float maximumDistance = 2;
	//The velocity of your player, used to determine que speed of the camera
	public float playerVelocity = 10;
	 
	 

	private Animator cameraAnim;
	private AnimatorStateInfo animStateInfo;
	
	private float _movementX;
	private float _movementZ;
	private float _movementY;

	private Transform initialCameraTransform;

	 
	private Quaternion initalRot;
	 
	void Awake()
	{
		cameraAnim = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");

		 
		{
			initalRot = this.transform.rotation;
			//cameraAnim.enabled = true;
			//cameraAnim.Play("cameraAnim");

		}
	 


	}


	/*public void PlayCameraAnim()
	{
		if(GlobalVariables.levelNumber==1)
		{
			cameraAnim.enabled = true;
			Debug.Log("Playing camera anim");

		}
	}*/

	void Start()
	{
		//PlayCameraAnim();

	}
	
	
	// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	void FixedUpdate()
	{
		//if(GlobalVariables.isCameraAnimationDone)
		{ 
		//	if(!GlobalVariables.isPlayerSpotted)
			{ 
				_movementX = ((player.transform.position.x + offsetX - this.transform.position.x))/maximumDistance;
				_movementZ = ((player.transform.position.z + offsetZ - this.transform.position.z))/maximumDistance;
				_movementY = ((player.transform.position.y + offsetY - this.transform.position.y))/maximumDistance;
				
				// Next position of camera
				Vector3 targetPos = this.transform.position + new Vector3((_movementX * playerVelocity * Time.deltaTime),
				                                                          _movementY,
				                                                          (_movementZ * playerVelocity * Time.deltaTime));
				// Distance between old position and new position
				float distanceVec = Vector3.Distance(this.transform.position, targetPos);
				// Linearly interpolates between two vectors.
				this.transform.position = Vector3.Lerp(this.transform.position, targetPos, distanceVec);

			}

		}
	}


	 
	void Update()
	{
		//animStateInfo = cameraAnim.GetCurrentAnimatorStateInfo(0);
 	// or give a skip button
/*
		if(GlobalVariables.levelNumber==1)
		{
			if(!GlobalVariables.isCameraAnimationDone)
			{ 
				if(animStateInfo.normalizedTime>0.99)
				{
					
					this.transform.rotation = initalRot;
					GlobalVariables.isCameraAnimationDone =  true;
					cameraAnim.enabled = false;
					level1HudManager.SkipCameraAnimation();
				}
			}
			else
			{
				
				this.transform.rotation = initalRot;
				
				//Vector3 finalRot = new Vector3( initialCameraTransform.eulerAngles.x, initialCameraTransform.eulerAngles.y, initialCameraTransform.eulerAngles.z);
				//this.transform.eulerAngles = finalRot;
				cameraAnim.enabled = false;
				level1HudManager.SkipCameraAnimation();
			}
		}*/
	

	}
}
