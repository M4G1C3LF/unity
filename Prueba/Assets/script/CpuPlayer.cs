using UnityEngine;
using System.Collections;

public class CpuPlayer : MonoBehaviour {
	
	
	public GameObject player;
	public Player playerScript;

	static int idleState = Animator.StringToHash("Base Layer.Idle");	//ID de estado de animator. Ponemos primero base y seguido el nombre de la animacion.
	static int movingBackState = Animator.StringToHash("Base Layer.MovingBack");	//ID de estado de animator. Ponemos primero base y seguido el nombre de la animacion.
	static int movingForwardState = Animator.StringToHash("Base Layer.MovingForward");
	static int jumpingState = Animator.StringToHash ("Base Layer.Jumping");

	int life = 100;
	
	public int getLife()
	{
		return life;
	}
	public void setLife(int var)
	{
		life = var;
	}

	string[] punchCombo = new string[10]{"wPunch",null,null,null,null,null,null,null,null,null};
	string[] kickCombo = new string[10]{"wKick",null,null,null,null,null,null,null,null,null};
	string[] hadoukenCombo =  new string[10]{"down","forward","wPunch",null,null,null,null,null,null,null}; 
	string[] shinkuHadoukenCombo = new string[10]{"down","forward","down","forward","wPunch",null,null,null,null,null};
	string[] shoryukenCombo = new string[10]{"forward","down","forward","wPunch",null,null,null,null,null,null};
	string[] hurricaneKickCombo = new string[10]{"down","back","wKick",null,null,null,null,null,null,null};
	
	
	public Animator anim;
	public AnimatorStateInfo currentBaseState;
	
	
	
	public AudioSource audioSource; 
	public AudioClip hadoukenClip;
	public AudioClip shinkuHadouken1Clip, shinkuHadouken2Clip;
	public AudioClip hurricaneKickClip;
	public AudioClip shoryukenClip;


	void OnCollisionEnter(Collision collision){
		Debug.Log("something has hit me");
	} 
	
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
		this.transform.position = new Vector3 (this.transform.position.x + (6f*this.transform.localScale.x), this.transform.position.y, this.transform.position.z);
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
	
	void moveForward()
	{
		anim.SetBool ("isMovingForward", true);
		this.transform.Translate (0.05f*this.transform.localScale.x,0,0);
	}
	void moveBack()
	{
		anim.SetBool ("isMovingBack", true);
		this.transform.Translate (-0.05f*this.transform.localScale.x,0,0);
	}
	void stopMoving()
	{
		anim.SetBool ("isMovingForward", false);
		anim.SetBool ("isMovingBack", false);

	}

	void updateAI()
	{
		if(anim.GetBool("canMove"))
		{
			if (Mathf.Abs(player.transform.position.x - this.transform.position.x ) > 1.8f)
			{
				if (Random.Range(0F,10F) > 9.9)
				{
					stopMoving ();
					if (Random.Range (0F,10F) < 9.7)
					{
						doHadouken();
					}
					else
					{
						doShinkuHadouken();
					}
				}
				else
				{
					if (Random.Range (0F,10F) < 9.7)
					{
						moveForward ();
					}
					else
					{
						doHurricaneKick();
					}
				}
			}
			else
			{ 
				switch(Random.Range (0,4))
				{
				case 0:
					doKick();
					break;
				case 1:
					doPunch();
					break;
				case 2:
					doShoryuken();
					life--;
					break;
				case 3:
					doHurricaneKick();
					break;
				case 4:
					moveBack();
					break;
				}
			}
		}
	}
		// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>(); // Cogemos componente del objeto actual.
		//		hadoukenSound= Resources.Load("audio/sfx/ryushoryuken.wav") as AudioSource;
		audioSource = GetComponent<AudioSource>();
		
		//playerController = new PlayerController ();
		player = GameObject.Find("PlayerController");
		playerScript = player.GetComponent<Player>();
		hadoukenClip = (AudioClip) Resources.Load("audio/sfx/SSFIV_Ryu_Voices/Synth_ryu_28_wav.aax_0000");
		shinkuHadouken1Clip = (AudioClip) Resources.Load("audio/sfx/SSFIV_Ryu_Voices/Synth_ryu_34_1_wav.aax_0000");
		shinkuHadouken2Clip = (AudioClip) Resources.Load("audio/sfx/SSFIV_Ryu_Voices/Synth_ryu_34_2_wav.aax_0000");
		hurricaneKickClip =	(AudioClip) Resources.Load("audio/sfx/SSFIV_Ryu_Voices/Synth_ryu_30_wav.aax_0000");
		shoryukenClip = (AudioClip) Resources.Load("audio/sfx/SSFIV_Ryu_Voices/Synth_ryu_29_wav.aax_0000");
		
		//playerController.setOrientationRight ();
		//setCanMoveTrue ();
	}
	
	// Update is called once per frame
	void Update () {

		//updateAI ();

	}
}
