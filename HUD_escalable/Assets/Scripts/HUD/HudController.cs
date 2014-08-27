using UnityEngine;
using System.Collections;
using System.Globalization;


public class HudController : MonoBehaviour {


	GameObject camera;			string cameraName 		= "Main Camera";
	GameObject hud;				string hudName			= "HUD";
	GameObject playerText;		string playerTextName	= "PlayerName";
	GameObject lifeBar;			string lifeBarName		= "LifeBar";
	GameObject lifeBarCarcass;	string lifeBarCarcassName = "LifeBarCarcass";
	GameObject scoreNumbers;	string scoreNumbersName = "ScoreNumbers";
	GameObject battleLevel;		string battleLevelName 	= "BattleLevel";
	GameObject timeNumbers;		string timeNumbersName	= "TimeNumbers";

	GUIText playerTextGUITextComponent;
	GUIText scoreNumbersGUITextComponent;
	GUIText battleLevelGUITextComponent;
	GUIText timeNumbersGUITextComponent;

	GUITexture	lifebarGUITextureComponent;

	float lifeBarScaleX;

	int rumblingFrames = 0; //Utilizamos esta variable para controlar la vibracion del hud.

	int blinkingFrames = 0;	//Utilizamos esta variable para controlar el numero de frames que estara activo o inactivo
	int actualBlinkFrame = 0;	//Utilizamos esta variable para controlar el frame en el que nos encontramos para realizar el parpadeo
	bool boolBlink = false;

	public void enable()
	{
		//MUESTRA EL HUD POR PANTALLA
		hud.active = true;
	
	}
	public void disable()
	{
		//DEJA DE MOSTRAR EL HUD POR PANTALLA
		hud.active = false;
	}

	public void setPlayerText(string var)
	{
		//PERMITE MODIFICAR EL TEXTO/NOMBRE DEL JUGADOR

		playerTextGUITextComponent.text = var;
	}

	public void updateLifeBar(int life)
	{
		//ACTUALIZA LA BARRA DE SALUD
		//la vida supongo que es un valor de 0 a 100, si no es asi hay que sacar el porcentaje de la vida.

		lifeBar.transform.localScale = new Vector3 (lifeBarScaleX * (life * 0.01f), lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);
		float pos = (100 - life) * 0.0235f; //para que cuadre la barra de vida hay que desplazar 0.0235 por cada punto de vida. 

		lifeBar.transform.localPosition = new Vector3 (pos, lifeBar.transform.localPosition.y, lifeBar.transform.localPosition.z);
	}
	public void updateScore(int score)
	{
		//ACUTALIZA LA PUNTUACION
		scoreNumbersGUITextComponent.text = score.ToString();

	}
	public void updateBattleLevel(string battleLevel)
	{
		battleLevelGUITextComponent.text = battleLevel;
	}
	public void updateTime(int minutes, int seconds)
	{

		string text = "";

		if (minutes < 10)
		{
			text+="0"+minutes;
		}
		else
		{
			text = minutes.ToString();
		}

		text += " :";

		if (seconds < 10)
		{
			text+="0"+seconds;
		}
		else
		{
			text += seconds.ToString();
		}
		timeNumbersGUITextComponent.text = text;
	}

	public void moveToRandomPosition()
	{
		//Hace temblar el HUD con posicion aleatoria
		this.transform.localPosition = new Vector3 (Random.Range (-0.05F, 0.05F), Random.Range (-0.05F, 0.05F), transform.localPosition.z);
	}
	public void defaultPosition()
	{
		this.transform.localPosition = new Vector3 (0F,0F, transform.localPosition.z);
	}

	public void checkRumbling ()
	{
		if (this.rumblingFrames > 0)
		{
			this.moveToRandomPosition();
			this.rumblingFrames--;
		}
		else
		{
			this.defaultPosition();
		}
	}
	public void rumble(int frames)
	{
		//USAMOS ESTA FUNCION PARA HACER TEMBLAR EL HUD
		this.rumblingFrames = frames;
	}
	public void checkBlinking()
	{
		if(this.boolBlink)
		{
			if (this.actualBlinkFrame < 0)
			{
				lifebarGUITextureComponent.active = false;
			}
			else
			{
				lifebarGUITextureComponent.active = true;
			}
			actualBlinkFrame--;

			if(this.actualBlinkFrame < -blinkingFrames)
			{
				actualBlinkFrame =blinkingFrames;
			}
		}


	}
	public void blinkLifeBar(bool var)
	{
		//USAMOS ESTA FUNCION PARA HACER PARPADEAR LA BARRA DE VIDA
		this.boolBlink = var;
	}
	public void setBlinkFrequency(int frames)
	{
		this.blinkingFrames = frames;
	}

	public void setGameObjects()
	{
		//AQUI CARGAMOS TODOS LOS GAME OBJECTS Y COMPONENETES NECESARIOS
		camera = GameObject.Find (cameraName);
		hud = GameObject.Find (hudName);
		playerText = GameObject.Find (playerTextName);

		lifeBar = GameObject.Find (lifeBarName);
		lifeBarScaleX = lifeBar.transform.localScale.x;


		scoreNumbers = GameObject.Find (scoreNumbersName);
		battleLevel = GameObject.Find (battleLevelName);
		timeNumbers = GameObject.Find (timeNumbersName);


		//playerTextGUITextComponent = playerText.GetComponent<GUIText> ();
		scoreNumbersGUITextComponent = scoreNumbers.GetComponent<GUIText> ();
		battleLevelGUITextComponent = battleLevel.GetComponent<GUIText> ();
		timeNumbersGUITextComponent = timeNumbers.GetComponent<GUIText> ();

		lifebarGUITextureComponent = lifeBar.GetComponent<GUITexture> ();
	}

	// Use this for initialization
	void Start () {
		rumblingFrames = 90;
		setGameObjects ();
		setBlinkFrequency (50);
	}
	
	// Update is called once per frame
	void Update () {
		//setCameraPosition ();
		updateScore (23);
		updateLifeBar (50);
		updateBattleLevel ("Smokin' Sick Style!!!");
		//setPlayerText ("Putilla");
		updateTime (32, 3);
		checkRumbling();
		checkBlinking ();

		if(Input.GetButtonDown("Fire1"))
		{
			this.rumble(30);
		}

		this.blinkLifeBar(true);
	}
}
