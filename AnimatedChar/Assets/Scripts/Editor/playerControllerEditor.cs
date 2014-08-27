using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor (typeof(PlayerController))]
public class PlayerController : Editor
{
	PlayerController playerController;

	public void OnEnable()
	{
		playerController = (PlayerController)target;
	}

	public override void OnInspectorGUI()
	{
		GUILayout.Label ("Translation Speed");

	}
}
