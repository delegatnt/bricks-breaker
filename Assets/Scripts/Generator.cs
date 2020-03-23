using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Level
{
    public int Id;
    public List<Vector3> Blocks;
}

public class Generator : MonoBehaviour
{
    public Text TextId;

    public void Save()
    {
        GameObject BlocksParent = GameObject.Find("Blocks");

        Level level = new Level();
        level.Id = int.Parse(TextId.text);
        level.Blocks = new List<Vector3>();

        foreach (Transform block in BlocksParent.transform)
        {
            level.Blocks.Add(block.transform.position);            
        }

        string jsonData = JsonUtility.ToJson(level, true);
        string jsonPath = Application.dataPath + "/Levels/level" + level.Id.ToString() + ".json";
        File.WriteAllText(jsonPath, jsonData);
    }
}
