using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour {

	public Collision col;
	
	public PlayerController playerController;
	
	static int idleState = Animator.StringToHash("Base Layer.Idle");	//ID de estado de animator. Ponemos primero base y seguido el nombre de la animacion.
	static int movingBackState = Animator.StringToHash("Base Layer.MovingBack");	//ID de estado de animator. Ponemos primero base y seguido el nombre de la animacion.
	static int movingForwardState = Animator.StringToHash("Base Layer.MovingForward");
	static int jumpingState = Animator.StringToHash ("Base Layer.Jumping");
	
	int life = 100;
	
	string name = "Raulin toca el violin";
	
	public string getName()
	{
		return name;
	}
	public void setName(string var)
	{
		name = var;
	}
	
	public string[] punchCombo = new string[10]{"wPunch",null,null,null,null,null,null,null,null,null};
	public string[] kickCombo = new string[10]{"wKick",null,null,null,null,null,null,null,null,null};
	public string[] hadoukenCombo =  new string[10]{"down","forward","wPunch",null,null,null,null,null,null,null}; 
	public string[] shinkuHadoukenCombo = new string[10]{"down","forward","down","forward","wPunch",null,null,null,null,null};
	public string[] shoryukenCombo = new string[10]{"forward","down","forward","wPunch",null,null,null,null,null,null};
	public string[] hurricaneKickCombo = new string[10]{"down","back","wKick",null,null,null,null,null,null,null};
	
	
	public Animator anim;
	public Animation animation;
	public AnimatorStateInfo currentBaseState;
	

	
	
	public AudioSource audioSource; 
	public AudioClip hadoukenClip;
	public AudioClip shinkuHadouken1Clip, shinkuHadouken2Clip;
	public AudioClip hurricaneKickClip;
	public AudioClip shoryukenClip;
	
	//////////////////////////////////////////////COMBOS//////////////////////////////////////////////////////// 
	
	public void doPunch()
	{
		anim.SetBool ("punch",true);
		anim.SetBool("canMove", false);
	}
	public void doKick()
	{
		anim.SetBool ("kick",true);
		anim.SetBool("canMove", false);
	}
	public void doHadouken()
	{
		audioSource.clip = hadoukenClip;
		anim.SetBool("hadouken", true);
		anim.SetBool("canMove", false);
		//	playerController.changeOrientation();
		print ("Hadouken!");
	}
	public void doShinkuHadouken()
	{
		audioSource.clip = shinkuHadouken1Clip;
		anim.SetBool("shinkuHadouken", true);
		anim.SetBool("canMove", false);
		print ("Shinku Hadouken!");
	}
	public void doShoryuken()
	{
		audioSource.clip = shoryukenClip;
		anim.SetBool ("canMove", false);
		anim.SetBool ("shoryuken",true);
		print ("Shoryuken!");
	}
	public void doHurricaneKick()
	{
		audioSource.clip = hurricaneKickClip;
		anim.SetBool ("canMove", false);
		anim.SetBool ("hurricaneKick",true);
		print ("Hurricane Kick!");
	}
	
	void OnTriggerEnter2D(Collider2D col){
		
		//	print ("idleState: " + idleState + "\tmovingForwardState: " + movingForwardState+"\nCurrentState: "+anim.GetCurrentAnimatorStateInfo(1));
		
		if (!anim.GetBool("canMove"))
		{
			
			if (col.gameObject.name == "P2LowCollider")
			{
				print ("Hostia en las patas");
			}
			else if (col.gameObject.name == "P2MidCollider")
			{
				print ("Hostia en el pechote");
			}
			else if (col.gameObject.name == "P2HighCollider")
			{
				print ("Hostia en la jeta");
			}
		}
	} 
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	public int getLife()
	{
		return life;
	}
	public void setLife(int var)
	{
		life = var;
	}
	
	public void setIsJumpingFalse()
	{
		anim.SetBool ("isJumping", false);
	}
	public void setCanMoveTrue()
	{
		anim.SetBool ("canMove", true);
	}
	public void SetHadoukenFalse(){
		anim.SetBool ("hadouken", false);
	}
	public void setPunchFalse()
	{
		anim.SetBool ("punch", false);
	}
	public void setKickFalse()
	{
		anim.SetBool ("kick", false);
	}
	public void setShinkuHadoukenFalse()
	{
		anim.SetBool ("shinkuHadouken", false);
	}
	public void setShoryukenFalse()
	{
		anim.SetBool ("shoryuken", false);
		this.transform.position = new Vector3 (this.transform.position.x + (1f*this.transform.localScale.x), this.transform.position.y, this.transform.position.z);
	}
	public void setHurricaneKickFalse()
	{
		anim.SetBool ("hurricaneKick", false);
		//this.transform.position = new Vector3 (this.transform.position.x + (6f*this.transform.localScale.x), this.transform.position.y, this.transform.position.z);
		
	}
	
	
	public void playSound(){
		//hadoukenSound.clip = (AudioClip) Resources.Load("hadouken");
		audioSource.Play();
	}
	
	public void changeShinkuHadoukenSound()
	{
		audioSource.clip = shinkuHadouken2Clip;
	}
	public bool checkInvertedOrientation()
	{
		if (transform.localScale.x < 0)
		{
			anim.SetBool ("invertedOrientation", true);
			return true;
		}
		else
		{
			anim.SetBool ("invertedOrientation", false);
			return false;
		}
		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}
}
