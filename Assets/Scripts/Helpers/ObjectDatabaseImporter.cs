using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;

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

                asset.objects = new List<ObjectDefinition>();
                asset.colorMappings = new ColorMappingDict();
                asset.propertiesValues = new PropertiesValuesPoolDict();
                foreach (ObjectProperty property in Enum.GetValues(typeof(ObjectProperty)))
                {
                    asset.propertiesValues[property] = new PropertyDetail(PropertyType.RANDOM_STRING, new List<string>());
                }

                // parsing file
                bool header = false;
                foreach (var line in fileContent.Split('\n'))
                {
                    if (header)
                    {
                        string[] splittedLine = line.Split('\t');
                        if (splittedLine.Length >= 9)
                        {
                            ObjectDefinition newObjDef = new ObjectDefinition
                            {
                                name = splittedLine[0],
                                properties = new PropertiesDict()
                            };

                            // color
                            string[] elements = splittedLine[1].Split(',');
                            for (var i = 0; i < elements.Length; i++)
                            {
                                elements[i] = elements[i].Trim();

                                if (!asset.propertiesValues[ObjectProperty.COLOR].values.Contains(elements[i]))
                                    asset.propertiesValues[ObjectProperty.COLOR].values.Add(elements[i]);
                            }
                            newObjDef.properties[ObjectProperty.COLOR] = new PropertyDetail(PropertyType.RANDOM_STRING, elements.ToList());

                            // weight
                            elements = Regex.Replace(splittedLine[2], @"\b to \b", ",").Split(',');
                            for (var i = 0; i < elements.Length; i++)
                            {
                                elements[i] = elements[i].Trim();
                            }
                            newObjDef.properties[ObjectProperty.WEIGHT] = new PropertyDetail(PropertyType.RANGE, elements.ToList());

                            // height
                            elements = Regex.Replace(splittedLine[3], @"\b to \b", ",").Split(',');
                            for (var i = 0; i < elements.Length; i++)
                            {
                                elements[i] = elements[i].Trim();
                            }
                            newObjDef.properties[ObjectProperty.HEIGHT] = new PropertyDetail(PropertyType.RANGE, elements.ToList());

                            // sex
                            elements = splittedLine[4].Split(',');
                            for (var i = 0; i < elements.Length; i++)
                            {
                                elements[i] = elements[i].Trim();
                                if (elements[i] != "N/A" && !asset.propertiesValues[ObjectProperty.SEX].values.Contains(elements[i]))
                                    asset.propertiesValues[ObjectProperty.SEX].values.Add(elements[i]);
                            }
                            newObjDef.properties[ObjectProperty.SEX] = new PropertyDetail(PropertyType.RANDOM_STRING, elements.ToList());

                            // species
                            elements = splittedLine[5].Split(',');
                            for (var i = 0; i < elements.Length; i++)
                            {
                                elements[i] = elements[i].Trim();
                                if (elements[i] != "N/A" && !asset.propertiesValues[ObjectProperty.SPECIES].values.Contains(elements[i]))
                                    asset.propertiesValues[ObjectProperty.SPECIES].values.Add(elements[i]);
                            }
                            newObjDef.properties[ObjectProperty.SPECIES] = new PropertyDetail(PropertyType.RANDOM_STRING, elements.ToList());

                            // edible
                            elements = splittedLine[6].Split(',');
                            for (var i = 0; i < elements.Length; i++)
                            {
                                elements[i] = elements[i].Trim();
                                if (elements[i] != "N/A" && !asset.propertiesValues[ObjectProperty.EDIBLE].values.Contains(elements[i]))
                                    asset.propertiesValues[ObjectProperty.EDIBLE].values.Add(elements[i]);
                            }
                            newObjDef.properties[ObjectProperty.EDIBLE] = new PropertyDetail(PropertyType.RANDOM_STRING, elements.ToList());

                            // age
                            elements = Regex.Replace(splittedLine[7], @"\b to \b", ",").Split(',');
                            for (var i = 0; i < elements.Length; i++)
                            {
                                elements[i] = elements[i].Trim();
                            }
                            newObjDef.properties[ObjectProperty.AGE] = new PropertyDetail(PropertyType.RANGE, elements.ToList());

                            // origin
                            elements = splittedLine[8].Split(',');
                            for (var i = 0; i < elements.Length; i++)
                            {
                                elements[i] = elements[i].Trim();
                                if (elements[i] != "N/A" && !asset.propertiesValues[ObjectProperty.ORIGIN].values.Contains(elements[i]))
                                    asset.propertiesValues[ObjectProperty.ORIGIN].values.Add(elements[i]);
                            }
                            newObjDef.properties[ObjectProperty.ORIGIN] = new PropertyDetail(PropertyType.RANDOM_STRING, elements.ToList());

                            asset.objects.Add(newObjDef);
                        }
                    }
                    else
                    {
                        header = true;
                        continue;
                    }
                }


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
