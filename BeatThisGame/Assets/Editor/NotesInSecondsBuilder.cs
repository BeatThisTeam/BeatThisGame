using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScenePrototypeManager))]
public class NotesInSecondsBuilder : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        ScenePrototypeManager script = (ScenePrototypeManager)target;
        if(GUILayout.Button("Create Array of Notes in Seconds")) {
            script.setNotesInSeconds();
        }
    }
}

//[CustomEditor(typeof(ObjectBuilderScript))]
//public class ObjectBuilderEditor : Editor {
//    public override void OnInspectorGUI() {
//        DrawDefaultInspector();

//        ObjectBuilderScript myScript = (ObjectBuilderScript)target;
//        if (GUILayout.Button("Build Object")) {
//            myScript.BuildObject();
//        }
//    }
//}