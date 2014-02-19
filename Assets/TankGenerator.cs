using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TankGenerator : MonoBehaviour {
	public GameObject tank1Prefab, tank2Prefab,tank3Prefab,tank4Prefab,tank5Prefab;
	GameObject newTank;
	GameObject oldTank = null;
	public Material red;
	public Material blue;
	bool assignRed = false;
	//int tankCount = 0;
	Component[] tankParts;
	List<Vector3> tankLocations = new List<Vector3>();
	Vector3 newTankLocation;
	int currentTank = 0;




	void Start () {

		for(int i=0;i<=100;i++)
		{
			newTankLocation = new Vector3( Random.Range (0, 200f), 0f, Random.Range (0f, 200f));
			if (!tankLocations.Contains(newTankLocation)){
				tankLocations.Add(newTankLocation);
			}
			else{
				i--;
			}
		}
		for (int i=0; i< tankLocations.Count;i++) {
			tankLocations[i]=tankLocations[i]*10;
		}
		foreach(Vector3 location in tankLocations){
			float randomNumber = Random.Range (0f, 100f);
			
			if ( randomNumber < 25f)
			{
				
				newTank= Instantiate (tank1Prefab, location, Quaternion.identity ) as GameObject;
			}
			else if(randomNumber < 50f) 
			{
				
				newTank=Instantiate (tank2Prefab, location, Quaternion.identity ) as GameObject;
			}
			else if(randomNumber < 70f) 
			{
				
				newTank=Instantiate (tank3Prefab, location, Quaternion.identity ) as GameObject;
			}
			else if(randomNumber < 90f) 
			{
				
				newTank=Instantiate (tank4Prefab, location, Quaternion.identity ) as GameObject;
			}
			else
			{
				
				newTank=Instantiate (tank5Prefab, location, Quaternion.identity ) as GameObject;
			}
			newTank.transform.localScale = new Vector3(100,100,100);
			
			
			
			if (oldTank!=null){
				Debug.Log("rotate");
				
				newTank.transform.LookAt(oldTank.GetComponent<Transform>());
				
			}
			tankParts = newTank.GetComponentsInChildren<Renderer>();
			if (assignRed==true){
				foreach (Renderer tankPart in tankParts) {
					tankPart.material = red;
				}
			}
			else{
				foreach (Renderer tankPart in tankParts) {
					tankPart.material = blue;
				}
			}
			
			oldTank=newTank;
			assignRed=!assignRed;
			
			
		} 
		currentTank = tankLocations.Count-1;


	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetRotation = (tankLocations[currentTank] - transform.position).normalized;
		Quaternion targetRotationQ =  Quaternion.LookRotation(targetRotation);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotationQ, 1f * Time.deltaTime);

		Vector3 targetPosition = tankLocations[currentTank] - transform.forward * 5;
		targetPosition.y = 15;
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, 50*Time.deltaTime);
		if (transform.position == targetPosition) {
			currentTank--;
		}



	
	}
}
