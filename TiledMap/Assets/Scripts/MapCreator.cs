using UnityEngine;
using System.Collections;


public class MapCreator : MonoBehaviour {

	Sprite [] sprites;



	Tile [,] tileMap;

	int tileSizeX=32;	//Tamaño horizontal en pixeles de un tile
	int tileSizeY=32;	//Tamaño Vertical en pixeles de un tile

	MapReader mapReader;
	public void loadSprites()
	{
		//CARGAMOS TODOS LOS SPRITES DE LA CARPETA
		sprites = Resources.LoadAll<Sprite>("sprites/ps2Town");
	}

	/*public void createMap()
	{
		map = 	{ 
					new int[] {1,1,1,1}, 
					new int[] {1,9,10,1}, 
					new int[] {1,11,12,1},
					new int[] {1,1,1,1}
				};

//		map [0] = new int[] {1,	1,	1,	1};
//		map [1][0] = 1; 	map [1][1] = 9; 	map [1][2] = 10; 	map [1][3] = 1;
//		map [2][0] = 1; 	map [2][1] = 11; 	map [2][2] = 12; 	map [2][3] = 1;
//		map [3][0] = 1; 	map [3][1] = 1; 	map [3][2] = 1; 	map [3][3] = 1;
		
	}*/

	public void loadMap(string file)
	{
		string map = mapReader.getMap (file);

		int[] size = mapReader.getMapSize (map);
	
		tileMap = new Tile[size[0],size[1]];	//INICIALIZAMOS MATRIZ CON EL MAPA
		for (int i=0; i<size[1];i++)
		{
			for(int j=0;j<size[0];j++)
			{
				tileMap[j,i]= new Tile("tile["+j+"]["+i+"]");

				try
				{
					int spriteNumber = mapReader.getTileFromFile(map,new int[]{j,i});

					try
					{
						Sprite sprite = sprites[spriteNumber];
						tileMap[j,i].setSprite(sprite);
					}
					catch(System.IndexOutOfRangeException e)
					{
						tileMap[j,i].setSprite (null);
						Debug.Log("Failed setting sprite "+spriteNumber+" into Tile["+j+"]["+i+"]. This sprite exists? ");
					}

					tileMap[j,i].setPosition(new Vector3(tileSizeX*j*0.01f,tileSizeY*i*(-0.01f),0f));
				}
				catch(System.ArgumentOutOfRangeException e)
				{
					Debug.Log("Failed to load position ["+j+"]["+i+"] from file. Maybe that position doesn't exist.");
					tileMap[j,i].setSprite (null);
				}
			}
		}
	}
	// Use this for initialization
	void Start () {


		mapReader = new MapReader ();
		loadSprites ();
		loadMap ("Assets/Resources/maps/tm1.map");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
