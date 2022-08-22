using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Swap_Editor : EditorWindow
{
    #region Variables
    int CurrentSelectionCount = 0;
    GameObject wantedObject;
    #endregion 

    #region Builtin Methods
    public static void LaunchEditor()
    {
        var editorWin = GetWindow<Swap_Editor>("Replace Objects");
        editorWin.Show();
    }

    private void OnGUI()
    {
        //Check the amount of Selected Objects
        GetSelection();

        EditorGUILayout.BeginVertical(); //Vert layout, //start&end
        EditorGUILayout.Space();


        EditorGUILayout.LabelField("Selection Count:" + CurrentSelectionCount.ToString(), EditorStyles.boldLabel);
        EditorGUILayout.Space();

        wantedObject = (GameObject)EditorGUILayout.ObjectField("Replace Object: ", wantedObject, typeof(GameObject), true);
        if(GUILayout.Button("Replace Selected Objects", GUILayout.ExpandWidth(true), GUILayout.Height(40)))
        {
            ReplaceSelectedObjects();
        }


        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();

        Repaint(); //allows for updates to happen without clicking mouse.
    }
    #endregion

    #region Custom Methods
    void GetSelection()
    {
        CurrentSelectionCount = 0;
        CurrentSelectionCount = Selection.gameObjects.Length; //We do .Lenght because it returns an array at first but this gives a # of objects selected.
    }

    void ReplaceSelectedObjects()
    {
        //check for selection count
        if(CurrentSelectionCount == 0)
        {
            CustomDialog("At least one object needs to be selected to replace with!");
            return;
        }

        //Check for replaced objects 
        if (!wantedObject)
        {
            CustomDialog("The Replace Object is empty, please assign something!"); 
            return;
        }

        //Replace Objects
        GameObject[] selectedObjects = Selection.gameObjects;
        for(int i = 0; i < selectedObjects.Length; i++)
        {
            Transform selectTransform = selectedObjects[i].transform; //transform info from selected object, we use the index currrently on.
            GameObject newObject = Instantiate(wantedObject, selectTransform.position, selectTransform.rotation);//Now we instantiate our wanted object at the same position and rotation of the current selected object
            newObject.transform.localScale = selectTransform.localScale; //we set the scale aswell

            DestroyImmediate(selectedObjects[i]); //destroy immediate for editor while destroy is for runtime.
        }
    }


    void CustomDialog(string aMessage)
    {
        EditorUtility.DisplayDialog("Replace Objects Warning", aMessage, "OK");

    }
    #endregion
}
