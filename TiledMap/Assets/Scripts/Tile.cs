using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	GameObject gameObjectTile;

	public Tile(string objectName)
	{
		gameObjectTile = new GameObject ();
		gameObjectTile.name = objectName;
		gameObjectTile.AddComponent<SpriteRenderer>();
	}

	public void setSprite(Sprite sprite)
	{
		gameObjectTile.GetComponent<SpriteRenderer> ().sprite = sprite;
	}
	public void setPosition(Vector3 pos)
	{
		gameObjectTile.transform.position = pos;
	}
	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
