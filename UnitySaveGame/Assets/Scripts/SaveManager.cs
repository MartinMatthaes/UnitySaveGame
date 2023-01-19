using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

#region Savegame
public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public SaveData activeSave;

    public bool hasLoaded = false;
    // Start is called before the first frame update

    public void Awake()
    {
        instance = this;
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.J))
        {
            Save();
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            Load();
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            DeleteSaveData();
        }
    }

    public void Save()
    {
        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);

        serializer.Serialize(stream, activeSave);
        stream.Close();

        Debug.Log("Saved Data");
    }

    public void Load()
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);

            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            hasLoaded = true;
            Debug.Log("Loaded Data");
        }
    }

    public void DeleteSaveData()
    {
        string dataPath = Application.persistentDataPath;


        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            File.Delete(dataPath + "/" + activeSave.saveName + ".save");
        }
        Debug.Log("Deleted SaveData");
    }
}

[System.Serializable]
public class SaveData
{
    public string saveName;

    public Vector3 respawnPosition;

    public bool doorOpen;

    public int lives;
}
#endregion
