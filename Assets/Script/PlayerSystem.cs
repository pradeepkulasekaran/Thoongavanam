using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using System.Linq;

public class PlayerSystem : MonoBehaviour
{

	public AudioClip footSteps;
	public AudioClip keyCardPickup;
	public AudioClip[] punchClip, kickClip;


	//public AudioClip shoutingClip;      // Audio clip of the player shouting.
	public float turnSmoothing = 15f;   // A smoothing value for turning the player.
	public float speedDampTime = 0.1f;  // The damping for the speed parameter
	public float enemyRange;
	
	private Animator anim;              // Reference to the animator component.
	      
	public AnimatorStateInfo animStateInfo;
	private NavMeshAgent playerNavAgent;

	public float playerMoveSpeed;
	public float h , v;
	 
	public static bool isAttack;

	string layerName;
	
	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent<Animator> ();
	 

		playerNavAgent = GetComponent<NavMeshAgent> ();
		 
		
		// Set the weight of the shouting layer to 1.
		//anim.SetLayerWeight(1, 1f);
	}


	void Start ()
	{

		 
	}
	
	void FixedUpdate ()
	{
		//if (!GlobalVariables.isPlayerSpotted) 
		{

		 
			// Cache the inputs.
			h = CrossPlatformInputManager.GetAxis ("Horizontal");
			v = CrossPlatformInputManager.GetAxis ("Vertical");
			/*#if UNITY_EDITOR
			GlobalVariables.sneak = Input.GetButton("Sneak");
			GlobalVariables.punch = Input.GetButton("Punch");
			GlobalVariables.kick = Input.GetButton("Kick");
			#endif
	*/
		
			MovementManagement (h, v, false);
		} 
		/*else 
		{
			anim.SetFloat ("Speed", 0);
		}*/
	}

	

	public void PunchEnemy()
	{
		anim.SetTrigger ("Punch");
		int r = Random.Range (0, 4);
		anim.SetInteger ("PunchRandom",r);
	}
	 
	
	void MovementManagement (float horizontal, float vertical, bool sneaking)
	{
		// Set the sneaking parameter to the sneak input.
		anim.SetBool ("Sneaking", sneaking);
		
		// If there is some axis input...
		if (horizontal != 0f || vertical != 0f) {
			// ... set the players rotation and set the speed parameter to 5.5f.
			Rotating (horizontal, vertical);
			 
			anim.SetFloat ("Speed",playerMoveSpeed, speedDampTime, Time.deltaTime);
		 
		} else
			// Otherwise set the speed parameter to 0.
			anim.SetFloat ("Speed", 0);
	}
	
	
	void Rotating (float horizontal, float vertical)
	{
		// Create a new vector of the horizontal and vertical inputs.
		Vector3 targetDirection = new Vector3 (horizontal, 0f, vertical);
		
		// Create a rotation based on this new vector assuming that up is the global y axis.
		Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);
		
		// Create a rotation that is an increment closer to the target rotation from the player's rotation.
		Quaternion newRotation = Quaternion.Lerp (GetComponent<Rigidbody> ().rotation, targetRotation, turnSmoothing * Time.deltaTime);
		
		// Change the players rotation to this new rotation.
		GetComponent<Rigidbody> ().MoveRotation (newRotation);
	}


	 
	 

	void Update ()
	{
		animStateInfo = anim.GetCurrentAnimatorStateInfo (0);
		if(Input.GetKeyDown (KeyCode.X))
		{
			PunchEnemy ();
		}
		 
		if(isAttack)
		{
			PunchEnemy ();
		}
		if(CrossPlatformInputManager.GetButton ("Jump"))
		{
			PunchEnemy ();
		}
		
	}


	void OnTriggerEnter (Collider other)
	{
		layerName = LayerMask.LayerToName (other.gameObject.layer);

		 
	}



 

}
