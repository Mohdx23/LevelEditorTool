 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Swap_Menu 
{
    [MenuItem("LO-TOOL/Level Tools/Replace Selected Objects")]
    public static void ReplaceSelectedObjects()
    {
        Swap_Editor.LaunchEditor();
    }

}
