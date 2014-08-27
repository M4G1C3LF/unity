using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


	GameObject player;
	Player playerScript;
	Vector3 position;

	public void checkPosition()
	{
		if (this.position == transform.localPosition)
		{
			this.position = transform.localPosition;
		}
		else
		{
			transform.localPosition = this.position;
		}

	}

	public void moveIt(float x)
	{
		//transform.localPosition = new Vector3(transform.localPosition.x+Input.GetAxis ("Horizontal")*0.05f,transform.localPosition.y+Input.GetAxis ("Vertical")*0.05f,transform.localPosition.z);
		transform.localPosition = new Vector3(x,transform.localPosition.y,transform.localPosition.z);
	}

	public void rumble()
	{
		transform.localPosition = new Vector3 (Random.Range(-0.5F, 0.5F), Random.Range(-0.5F, 0.5F), transform.localPosition.z);

	}

	// Use this for initialization
	void Start () {
		//player = GetComponent<Player>();
		player = GameObject.Find("PlayerController"); //Buscamos al player
		playerScript = player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {

		if (player.transform.localPosition.x > -4 && player.transform.localPosition.x < 4)
		{
			moveIt (player.transform.localPosition.x);
		}
	//	moveIt (transform.localPosition.x + Input.GetAxis ("Horizontal") * 0.05f, 0f);

	

		if (playerScript.anim.GetBool("shinkuHadouken"))
		{
			this.rumble ();
		}

		
	}
}
