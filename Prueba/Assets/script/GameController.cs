using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	GameObject player, player2;
	GameObject time;
	GameObject P1LifeBar, P1LifeBarCarcass, P1LifeBarDecoration, P1Name;
	GameObject P2LifeBar, P2LifeBarCarcass;
	GameObject mainCamera;
	GameObject FightBanner;
	float battleTime = 100.0f;
	bool endOfCombat= false;
//	Sprite[] timeSprites = new Sprite[10];

	Sprite[] timeSprites; 
	Player playerScript;
	CpuPlayer cpuPlayerScript;

	TextMesh timeTextMesh;
	bool player1RightOrientation = true;
	// Use this for initialization
	void Start () {

		player = GameObject.Find("PlayerController"); //Buscamos al player
		player2 = GameObject.Find("Player2Controller"); //Buscamos al player
		mainCamera = GameObject.Find ("Main Camera");
		time = GameObject.Find ("Time");
		P1LifeBar = GameObject.Find ("P1LifeBar");
		P1LifeBarCarcass = GameObject.Find ("P1LifeBarCarcass");
		P1LifeBarDecoration = GameObject.Find ("P1LifeBarDecoration");
		P1Name = GameObject.Find ("P1Name");
		P2LifeBar = GameObject.Find ("P2LifeBar");
		P2LifeBarCarcass = GameObject.Find ("P2LifeBarCarcass");
		FightBanner = GameObject.Find ("FightBanner");
		playerScript = player.GetComponent<Player>();
		cpuPlayerScript = player2.GetComponent<CpuPlayer> ();
		timeTextMesh =time.GetComponent<TextMesh> ();
		//player2 = Instantiate(prefab, new Vector3(i * 2.0F, 0, 0), Quaternion.identity) as Transform;
		initializeTimeSprites ();
		setHudNames ();
	}


	public void setHudNames()
	{
		P1Name.GetComponent<GUIText> ().text = playerScript.getName ();
	}
	public void initializeTimeSprites()
	{
		/*for (int i = 0; i<10; i++) 
		{
			timeSprites [i] = (Sprite)Resources.Load ("sprites/hud/TimeNumbers_" + i);
		}*/
		timeSprites = Resources.LoadAll<Sprite>("sprites/hud/TimeNumbers");
	
	}
	public void checkOrientation()
	{
		if (player.transform.localPosition.x < player2.transform.localPosition.x)
		{
			if (!player1RightOrientation)
			{
				this.changeFightersOrientation();
			}
			this.player1RightOrientation = true;

		}
		else
		{
			if (player1RightOrientation)
			{
				this.changeFightersOrientation();
			}
			this.player1RightOrientation = false;
		}
	}
	
	public void updateLifeBarDecoration()
	{
		if (P1LifeBarDecoration.transform.position.x > P1LifeBar.transform.position.x)
		{
			P1LifeBarDecoration.transform.position = new Vector3(P1LifeBarDecoration.transform.position.x-8f,P1LifeBarDecoration.transform.position.y,P1LifeBarDecoration.transform.position.z);
		}
		else
		{
			P1LifeBarDecoration.transform.position = new Vector3(P1LifeBarDecoration.transform.position.x+0.10f,P1LifeBarDecoration.transform.position.y,P1LifeBarDecoration.transform.position.z);
		}
	}
	public void printLife()
	{
		P1LifeBar.transform.position = new Vector3 (mainCamera.transform.position.x-0.23f, P1LifeBar.transform.position.y, P1LifeBar.transform.position.z);
		P1LifeBar.transform.localScale = new Vector3 (playerScript.getLife () * 0.01f, P1LifeBar.transform.localScale.y, P1LifeBar.transform.localScale.z);
		P1LifeBarCarcass.transform.position = new Vector3 (mainCamera.transform.position.x, P1LifeBarCarcass.transform.position.y, P1LifeBarCarcass.transform.position.z);

		P2LifeBar.transform.position = new Vector3 (mainCamera.transform.position.x+0.23f, P2LifeBar.transform.position.y, P2LifeBar.transform.position.z);
		P2LifeBar.transform.localScale = new Vector3 (cpuPlayerScript.getLife () *- 0.01f, P2LifeBar.transform.localScale.y, P2LifeBar.transform.localScale.z);
		P2LifeBarCarcass.transform.position = new Vector3 (mainCamera.transform.position.x, P2LifeBarCarcass.transform.position.y, P2LifeBarCarcass.transform.position.z);


		updateLifeBarDecoration ();
	}
	public void printKO()
	{
		Sprite KOSprite =  Resources.Load<Sprite> ("sprites/hud/ko");
		GameObject KOBanner = new GameObject();
		KOBanner.name = "KO Banner";
		KOBanner.AddComponent<SpriteRenderer> ();
		KOBanner.GetComponent<SpriteRenderer>().sprite = KOSprite;

		endOfCombat = true;
	}

	public void printBattleTime()
	{
		float timeLeft = battleTime - Time.time;
		float secondsLeft = 0;

	/*	if (timeLeft > 0) {
						print ("TIME LEFT: " + timeLeft);
				} else {
						print ("TIME OUT");
				}
*/
		time.transform.position = new Vector3 (mainCamera.transform.position.x, time.transform.position.y, time.transform.position.z);

	/*	if (timeLeft < 10) { GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[0]; secondsLeft=timeLeft;}
		else if (timeLeft < 20) { GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[1]; secondsLeft=timeLeft-10;}
		else if (timeLeft < 30) { GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[2]; secondsLeft=timeLeft-20;}
		else if (timeLeft < 40) { GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[3]; secondsLeft=timeLeft-30;}
		else if (timeLeft < 50) { GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[4]; secondsLeft=timeLeft-40;}
		else if (timeLeft < 60) { GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[5]; secondsLeft=timeLeft-50;}
		else if (timeLeft < 70) { GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[6]; secondsLeft=timeLeft-60;}
		else if (timeLeft < 80) { GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[7]; secondsLeft=timeLeft-70;}
		else if (timeLeft < 90) { GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[8]; secondsLeft=timeLeft-80;}
		else if (timeLeft < 100) { GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[9]; secondsLeft=timeLeft-90;}
*/
		if (timeLeft >90) { GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[9]; secondsLeft=timeLeft-90;}
		else if (timeLeft > 80) { GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[8]; secondsLeft=timeLeft-80;}
		else if (timeLeft > 70)	{ GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[7]; secondsLeft=timeLeft-70;}
		else if (timeLeft > 60)	{ GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[6]; secondsLeft=timeLeft-60;}
		else if (timeLeft > 50)	{ GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[5]; secondsLeft=timeLeft-50;}
		else if (timeLeft > 40)	{ GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[4]; secondsLeft=timeLeft-40;}
		else if (timeLeft > 30) { GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[3]; secondsLeft=timeLeft-30;}
		else if (timeLeft > 20)	{ GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[2]; secondsLeft=timeLeft-20;}
		else if (timeLeft > 10)	{ GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[1]; secondsLeft=timeLeft-10;}
		else { GameObject.Find ("Tens").GetComponent<SpriteRenderer>().sprite = timeSprites[0]; secondsLeft=timeLeft;}

/*		if (secondsLeft <1){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [0];}
		else if(secondsLeft <2){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [1];}
		else if(secondsLeft <3){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [2];}
		else if(secondsLeft <4){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [3];}
		else if(secondsLeft <5){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [4];}
		else if(secondsLeft <6){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [5];}
		else if(secondsLeft <7){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [6];}
		else if(secondsLeft <8){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [7];}
		else if(secondsLeft <9){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [8];}
		else if(secondsLeft <10){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [9];}

*/
		if (secondsLeft > 9){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [9];}
		else if (secondsLeft > 8){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [8];}
		else if (secondsLeft > 7){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [7];}
		else if (secondsLeft > 6){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [6];}
		else if (secondsLeft > 5){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [5];}
		else if (secondsLeft > 4){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [4];}
		else if (secondsLeft > 3){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [3];}
		else if (secondsLeft > 2){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [2];}
		else if (secondsLeft > 1){GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [1];}
		else {GameObject.Find ("Units").GetComponent<SpriteRenderer> ().sprite = timeSprites [0];}

	}

	public void setFightBannerFalse()
	{
		Object.Destroy (FightBanner);
	}
	public void changeFightersOrientation()
	{
		player.transform.localScale = new Vector3(player.transform.localScale.x*-1,player.transform.localScale.y,player.transform.localScale.z);
		player2.transform.localScale = new Vector3(player2.transform.localScale.x*-1,player2.transform.localScale.y,player2.transform.localScale.z);
	}

	// Update is called once per frame
	void Update () {

		if ((playerScript.getLife () <= 0 || cpuPlayerScript.getLife () <= 0) && !endOfCombat)
		{
			printKO ();
		}
		printLife ();
		printBattleTime ();
		checkOrientation ();
	
	}
}
