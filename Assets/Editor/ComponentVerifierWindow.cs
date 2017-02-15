using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections.Generic;

public class ComponentVerifierWindow : EditorWindow
{

    private bool _shouldRemoveDuplicates;
    private const string WindowTitle = "Component Verifier";

    [MenuItem("Window/Component Verifier")]
    public static void Open()
    {
        EditorWindow.GetWindow(typeof(ComponentVerifierWindow), false, WindowTitle);
    }

    private void OnGUI()
    {
        GUILayout.Label(WindowTitle, EditorStyles.boldLabel);
        _shouldRemoveDuplicates = EditorGUILayout.Toggle("Remove Duplicates", _shouldRemoveDuplicates);
        if (GUILayout.Button("Verify", GUILayout.Width(200f)))
        {
            RunDuplicateCheck();
            Debug.Log("Component Verifier Complete");
        }
    }

    private void RunDuplicateCheck()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene != null)
        {
            GameObject[] rootObjects = scene.GetRootGameObjects();
            foreach (GameObject root in rootObjects)
            {
                CheckDuplicateComponent(root);
                GameObject[] children = root.GetComponentsInChildren<Transform>(true).Select(x => x.gameObject).ToArray();
                foreach(GameObject child in children)
                    CheckDuplicateComponent(child);
            }
        }
    }

    private void CheckDuplicateComponent(GameObject go)
    {
        bool duplicate = false;
        Component[] components = go.GetComponents(typeof(Component));
        HashSet<Type> objectTypes = new HashSet<Type>();
        foreach(Component component in components)
        {
            duplicate = !objectTypes.Add(component.GetType());
            if (duplicate)
            {
                Debug.LogWarning("Found duplicate component " + component.GetType().ToString() + " on " + go.name);
                if (_shouldRemoveDuplicates)
                    DestroyImmediate(component);
            }
        }
    }

}
