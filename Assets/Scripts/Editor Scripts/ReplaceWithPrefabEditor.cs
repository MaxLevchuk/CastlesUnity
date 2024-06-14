using UnityEngine;
using UnityEditor;

public class ReplaceWithPrefabEditor : EditorWindow
{
    public GameObject prefab; // Префаб, на который будем заменять
    public string objectTag; // Тег объектов, которые нужно заменить

    [MenuItem("Tools/Replace With Prefab")]
    public static void ShowWindow()
    {
        GetWindow<ReplaceWithPrefabEditor>("Replace With Prefab");
    }

    void OnGUI()
    {
        GUILayout.Label("Settings", EditorStyles.boldLabel);

        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);
        objectTag = EditorGUILayout.TextField("Tag", objectTag);

        if (GUILayout.Button("Replace"))
        {
            ReplaceObjects();
        }
    }

    void ReplaceObjects()
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab is not assigned.");
            return;
        }

        GameObject[] objectsToReplace = GameObject.FindGameObjectsWithTag(objectTag);

        foreach (GameObject obj in objectsToReplace)
        {
            Vector3 position = obj.transform.position;
            Quaternion rotation = obj.transform.rotation;
            Transform parent = obj.transform.parent;

            GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parent);
            newObject.transform.position = position;
            newObject.transform.rotation = rotation;

            DestroyImmediate(obj);
        }

        Debug.Log($"{objectsToReplace.Length} objects replaced with {prefab.name}.");
    }
}
