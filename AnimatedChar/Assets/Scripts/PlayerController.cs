using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	GameObject link;
	Animator linkAnimator;

	float verticalAxis; 
	float horizontalAxis;
	Vector3 localScale;


	int moveSideState;


	public float translationSpeed = 0.01f;	//USE THIS VAR TO CONFIG THE SPEED OF THE PLAYER




	public void checkAnimation()
	{

		linkAnimator.SetFloat ("verticalAxis",verticalAxis);

		if (verticalAxis != 0)
		{
			linkAnimator.SetBool("isMoving",true);

			link.transform.localScale = localScale;
		
		}


		if (horizontalAxis != 0)
		{
			linkAnimator.SetBool ("isMoving",true);
			
			
			if(horizontalAxis > 0)
			{
				
				linkAnimator.SetBool ("isMoving",true);
				linkAnimator.SetBool ("movingAtSide",true);
				
				
				link.transform.localScale = localScale;
				
			}
			else if (horizontalAxis < 0)
			{
				linkAnimator.SetBool ("movingAtSide",true);

				
				if (linkAnimator.GetCurrentAnimatorStateInfo(0).nameHash == moveSideState)
				{
					link.transform.localScale = new Vector3 (-localScale.x,localScale.y,localScale.z);
				}
				
			}
		}
		else
		{
			
			linkAnimator.SetBool ("movingAtSide",false);
		}

		
		if (horizontalAxis == 0 && verticalAxis == 0)
		{
			linkAnimator.SetBool ("isMoving",false);

		}

	}
	public void getAxis()
	{
		verticalAxis = Input.GetAxis ("Vertical");
		horizontalAxis = Input.GetAxis ("Horizontal");
	}


	public void checkMovement()
	{
		float x = horizontalAxis*translationSpeed;
		float y = verticalAxis*translationSpeed;

		this.transform.position = new Vector3 (this.transform.position.x + x, this.transform.position.y + y, this.transform.position.z);
	}

	// Use this for initialization
	void Start () {
	
		link = GameObject.Find ("LinkSprite");
		linkAnimator = link.GetComponent<Animator> ();
		localScale =link.transform.localScale;
		moveSideState = Animator.StringToHash("Moves.MoveSide");

	}
	
	// Update is called once per frame
	void Update () {
		getAxis ();
		checkAnimation ();
		checkMovement ();


	}
}
