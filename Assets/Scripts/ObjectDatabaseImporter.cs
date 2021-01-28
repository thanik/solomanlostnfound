using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Rendering;

public class ObjectDatabaseImporter
{
    [MenuItem("Tools/Import Object Database")]
    private static void ImportDB()
    {
        string path = EditorUtility.OpenFilePanelWithFilters("Open database tsv file", "", new string[] {"Tab-seperated file","tsv"});
        if (path.Length != 0)
        {
            var fileContent = File.ReadAllText(path);
            try
            {
                ObjectDatabase asset = ScriptableObject.CreateInstance<ObjectDatabase>();

                AssetDatabase.CreateAsset(asset, "Assets/ScriptableObjects/ObjectDatabase.asset");
                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();

                Selection.activeObject = asset;
                Debug.Log("Database imported successfully!");
            }
            catch (Exception exception)
            {
                Debug.LogError("Database import failed!");
                Debug.LogException(exception);
                throw;
            }
            
        }
    }
}
