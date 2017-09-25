
using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public float moveSpeed;
	public Transform start;
	public Transform end;
	public GameObject mainCamera;

	// Use this for initialization
	int target= 0;
	void Start () {
		//transform.LookAt (start.position);
		mainCamera.transform.localPosition = new Vector3 ( 0, 0, 0 );
		mainCamera.transform.localRotation = Quaternion.Euler (18, 180, 0);
		ChangeView01();
	
	}
	
	// Update is called once per frame
	void Update () {

		
	}

	void FixedUpdate()
	{
		MoveObj ();
		
		if (Input.GetKeyDown (KeyCode.A)) {
			
		}
		
		if (Input.GetKeyDown (KeyCode.S)) {
		//	ChangeView02();
		}
	}
	
	
	void MoveObj() {		
	//	float moveAmount = Time.smoothDeltaTime * moveSpeed;
	//	transform.Translate ( 0f, 0f, moveAmount );
		if (target == 0) {
			float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, start.position, step);
         if(Vector3.Distance(transform.position,start.position) < 0.1f){
				transform.LookAt (end.position);
        	target=1;
        }
		}
		else{

			float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, end.position, step);
        if(Vector3.Distance(transform.position,end.position) < 0.1f){
				transform.LookAt (start.position);
        	target=0;
        }
		}
		}
	



	void ChangeView01() {
		transform.position = new Vector3 (0, 2, 10);
		// x:0, y:1, z:52
		mainCamera.transform.localPosition = new Vector3 ( -8, 2, 0 );
		mainCamera.transform.localRotation = Quaternion.Euler (14, 90, 0);
	}

	void ChangeView02() {
		transform.position = new Vector3 (0, 2, 10);
		// x:0, y:1, z:52
		mainCamera.transform.localPosition = new Vector3 ( 0, 0, 0 );
		mainCamera.transform.localRotation = Quaternion.Euler ( 19, 180, 0 );
		moveSpeed = -20f;
		
	}
	void makeFile(){

	}
}























