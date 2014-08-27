using UnityEngine;
using System.Collections;
using System.IO;

public class MapReader : MonoBehaviour {


	public string getMap(string file)
	{
		string readText = File.ReadAllText (file);
		return readText;
	}
	public int[] getMapSize(string readText)
	{


		int i = 0;
		int x = 0, y = 0;
		while (readText.Substring(i,1) != ":") 
		{

			if (readText.Substring (i, 1) == ",") 
			{
				x++;
			} 
			else if (readText.Substring (i, 1) == ";") 
			{
				y++;
				x=0;
			}
			i++;
		}
		
		int[] mapSize = new int[]{x+1,y+1};
		return mapSize; 
	}

	public int getTileFromFile(string readText,int[] position)
	{
		int tileNumber = 0;

		int i = 0;
		int x = 0, y = 0;


		bool end = false;
		while (!end)
		{
			string read = readText.Substring (i, 1);

			if (x==position[0] && y==position[1])
			{
				if (read =="," || read==";" || read ==":")
				{
					end=true;
				}
				else
				{
					tileNumber = tileNumber * 10 + int.Parse (read);
				}
			}

			if (read == ",") 
			{
				x++;
			} 
			else if (read == ";") 
			{
				x=0;
				y++;
			}
			i++;
		}	

		return tileNumber; 
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
