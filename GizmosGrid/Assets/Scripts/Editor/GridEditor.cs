using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(Grid))]
public class GridEditor : Editor
{
	Grid grid;

	public void OnEnable()
	{
		grid = (Grid)target;
		SceneView.onSceneGUIDelegate = GridUpdate;
	}

	public override void OnInspectorGUI()
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label(" Grid Width ");
		grid.width = EditorGUILayout.FloatField(grid.width, GUILayout.Width(50));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label(" Grid Height ");
		grid.height = EditorGUILayout.FloatField(grid.height, GUILayout.Width(50));
		GUILayout.EndHorizontal();

		SceneView.RepaintAll();
	}
	
	void GridUpdate(SceneView sceneview)
	{
		Event e = Event.current;

		Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
		Vector3 mousePos = r.origin;
		
		if (e.isKey && e.character == 'a')	//Clona el objeto seleccionado en la posicion del raton, centrado en el grid.
		{
			GameObject obj;
			Object prefab = EditorUtility.GetPrefabParent(Selection.activeObject);
			
			if (prefab)
			{

				//Undo.IncrementCurrentEventIndex(); //NO RECONOCE ESTE METODO
				obj = (GameObject)EditorUtility.InstantiatePrefab(prefab);
				Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x/grid.width)*grid.width + grid.width/2.0f,
				                              Mathf.Floor(mousePos.y/grid.height)*grid.height + grid.height/2.0f, 0.0f);
				obj.transform.position = aligned;
				Undo.RegisterCreatedObjectUndo(obj, "Create " + obj.name); 
			}
		}
		else if (e.isKey && e.character == 'd')	//Elimina el objeto seleccionado
		{
			//Undo.IncrementCurrentEventIndex(); //NO RECONOCE ESTE METODO
			Undo.RegisterSceneUndo("Delete Selected Objects");
			foreach (GameObject obj in Selection.gameObjects)
				DestroyImmediate(obj);
		}
	}
}