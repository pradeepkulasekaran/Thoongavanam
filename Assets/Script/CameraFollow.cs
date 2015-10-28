using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public Camera mainCam;
	public GameObject[] cameraLocations;
	public Transform playerTransform;
	public float height;
	public float offsetZ,offsetX;
	 
	public int cameraIndex;

	private Vector3 adjustedPos;
	public float followSpeed;

    public float cameraZmin, cameraZmax ,cameraXmin, cameraXmax;
    public float levelNumber;
    Vector3 pPos;
    
	// Use this for initialization
	void Start () 
	{
		mainCam = GameObject.Find("mCamera").GetComponent<Camera>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
		 
		
	}

		
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void LateUpdate()
	{
        if (cameraIndex == 1)
        {
            adjustedPos = playerTransform.position;

            adjustedPos.z = playerTransform.position.z + offsetZ;
            adjustedPos.x = playerTransform.position.x + offsetX;
            adjustedPos.y = playerTransform.transform.position.y + height;// offsetX;	
            //adjustedPos.y += height;
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, adjustedPos, followSpeed * Time.deltaTime * 100);
            mainCam.transform.position = new Vector3(Mathf.Clamp( mainCam.transform.position.x, cameraXmin, cameraXmax) , mainCam.transform.position.y,
                
                Mathf.Clamp(mainCam.transform.position.z, cameraZmin, cameraZmax));
                
        }

         

	}
}
