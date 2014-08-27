using UnityEngine;
using System.Collections;


public class Player : Fighter {

	GameObject player;

	public void checkMovement()
	{
		if(anim.GetFloat("currentDirectionH") != 0 && anim.GetBool ("canMove") == true)
		{
			if (transform.localPosition.x < -10)	//Creacion de "Jaula"
			{
				transform.localPosition = new Vector3(-9.9999f,transform.localPosition.y,transform.localPosition.z);
			}
			else if (transform.localPosition.x > 10)
			{
				transform.localPosition = new Vector3(9.9999f,transform.localPosition.y,transform.localPosition.z);
			}
			anim.SetBool ("isMoving", true);
			this.transform.Translate (playerController.getHAxis()*0.05f,0,0);
			
			
		}
		else
		{
			anim.SetBool("isMoving", false);
		}


		if(anim.GetFloat("currentDirectionV") > 0)
		{
			anim.SetBool("isJumping", true);	//SALTO
		}
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>(); // Cogemos componente del objeto actual.
		animation = GetComponent<Animation> ();
		//		hadoukenSound= Resources.Load("audio/sfx/ryushoryuken.wav") as AudioSource;
		audioSource = GetComponent<AudioSource>();

		//playerController = new PlayerController ();
		playerController = GetComponent<PlayerController>();

		player = GameObject.Find("PlayerController.Player"); //Buscamos al player

		hadoukenClip = (AudioClip) Resources.Load("audio/sfx/SSFIV_Ryu_Voices/Synth_ryu_28_wav.aax_0000");
		shinkuHadouken1Clip = (AudioClip) Resources.Load("audio/sfx/SSFIV_Ryu_Voices/Synth_ryu_34_1_wav.aax_0000");
		shinkuHadouken2Clip = (AudioClip) Resources.Load("audio/sfx/SSFIV_Ryu_Voices/Synth_ryu_34_2_wav.aax_0000");
		hurricaneKickClip =	(AudioClip) Resources.Load("audio/sfx/SSFIV_Ryu_Voices/Synth_ryu_30_wav.aax_0000");
		shoryukenClip = (AudioClip) Resources.Load("audio/sfx/SSFIV_Ryu_Voices/Synth_ryu_29_wav.aax_0000");

		//col.gameObject = GameObject.Find ("Player2Controller");
		//playerController.setOrientationRight ();
		//setCanMoveTrue ();
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log(comboStack[0]+" "+comboStack[1]+" "+comboStack[2]+" "+comboStack[3]+" "+comboStack[4]+" "+comboStack[5]+" "+comboStack[6]+" "+comboStack[7]+" "+comboStack[8]+" "+comboStack[9]+" ");
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0); // Coge el estado actual del animator del objeto actual

	/*	if (playerController.getFrameMoveStack() == playerController.getMaxFrameMoveStack()) 
		{
			playerController.clearComboStack();
			print("clearComboStack");
		}
*/


		playerController.updateAxisState(); //Actualizamos el valor de los ejes..
		this.checkInvertedOrientation ();	//Comprobamos hacia donde mira nuestro personaje

		anim.SetFloat("currentDirectionH",playerController.getHAxis()); //Cogemos el valor del eje horizontal.
		anim.SetFloat ("currentDirectionV", playerController.getVAxis());//Cogemos el valor del eje vertical.

		//anim.SetBool ("button1", Input.GetButton("Fire1"));



		this.checkMovement ();


	



		playerController.updateComboStack();

		//if (this.comboStack[0] == "down" && this.comboStack[1] == "right" && this.comboStack[2] == "fire1")

		//if (playerController.comboStack[0] == "down" && playerController.comboStack[1] == "right" && playerController.comboStack[2] == "fire1")

		if(playerController.checkComboStack (punchCombo))
		{
			doPunch();
			this.setLife(this.getLife()-10);
		}
		if(playerController.checkComboStack (kickCombo))
		{
			doKick ();
		}
		else if(playerController.checkComboStack(hadoukenCombo))
		{
			doHadouken();
		}
		else if (playerController.checkComboStack (shinkuHadoukenCombo))
		{

			doShinkuHadouken();
		}
		else if (playerController.checkComboStack(shoryukenCombo))
		{
			doShoryuken();
		}
		else if (playerController.checkComboStack(hurricaneKickCombo))
		{
			doHurricaneKick();
		}


		playerController.setFrameMoveStack(playerController.getFrameMoveStack()+1);



	//	if (currentBaseState.nameHash == movingBackState) {
	//		this.transform.Translate(-0.1f,0,0);
	//	}
	}
}
