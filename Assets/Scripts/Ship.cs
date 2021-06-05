using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
	[SerializeField] private Transform cannon;
	[SerializeField] private GameObject[] shots = {};

	private KeyCode[] keyCodes = {
		KeyCode.Alpha0,
		KeyCode.Alpha1,
		KeyCode.Alpha2,
		KeyCode.Alpha3,
		KeyCode.Alpha4,
		KeyCode.Alpha5,
		KeyCode.Alpha6,
		KeyCode.Alpha7,
		KeyCode.Alpha8,
		KeyCode.Alpha9,
	};

	private bool shooting;

	private GameObject actualShot;

    // Start is called before the first frame update
    void Start()
    {
      actualShot = shots[0];
    }

    // Update is called once per frame
    void Update()
    {
    	var bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
    	var topRight = Camera.main.ScreenToWorldPoint(
    		new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight)
		);
		
		var cameraRect = new Rect(
			bottomLeft.x,
			bottomLeft.y,
			topRight.x - bottomLeft.x,
			topRight.y - bottomLeft.y
		);
    		
    	
    	Vector3 position = transform.position;
    	float x = Input.GetAxis("Horizontal");
    	float y = Input.GetAxis("Vertical");
    	
    	position.x += x * 5.0f * Time.deltaTime;
    	position.y += y * 5.0f * Time.deltaTime;
    	
    	transform.position = position;
    	
    	float clampX = Mathf.Clamp(transform.position.x, cameraRect.xMin, cameraRect.xMax);
    	float clampY = Mathf.Clamp(transform.position.y, cameraRect.yMin, cameraRect.yMax);
    	
    	transform.position = new Vector3(clampX, clampY, transform.position.z);
    	
			

			for (int i = 0 ; i < keyCodes.Length; i ++ ){
				if(Input.GetKeyDown(keyCodes[i])){
						actualShot = shots[i];
				}
			}

			if (Input.GetKey(KeyCode.Space)) Shot(actualShot);
    	
    }
    
    void Shot (GameObject shotObject) 
    {
    	if (shooting) return;
    	shooting = true;
    
    	GameObject newShot = Instantiate(shotObject, cannon.position, cannon.rotation);
    	newShot.TryGetComponent(out Shot shot);
    	StartCoroutine(Cooldown(shot.cooldown));
    }
    
    IEnumerator Cooldown (float time) 
    {
    	yield return new WaitForSeconds(time);
    	shooting = false;
    }
};
