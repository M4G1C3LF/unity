using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	GameObject gameObjectTile;

	public int altitude; 


	public int getAltitude()
	{
		return this.altitude;
	}

	public void setAltitude(int var)
	{
		this.altitude = var;

	}
	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
