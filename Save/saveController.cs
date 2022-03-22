using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class saveController 
{
    public static void savePlayerData (skillsController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saveData.bin";

        FileStream stream = new FileStream(path, FileMode.Create);

        saveData data = new saveData(player);

        formatter.Serialize(stream,data);

        stream.Close();
    }

    public static saveData loadPlayerData ()
    {
        string path = Application.persistentDataPath + "/saveData.bin";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);
            saveData data = formatter.Deserialize(stream) as saveData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save File Not Found");
            return null;
        }
    }

    // public static void saveNPCData()
    // {
    //     BinaryFormatter formatter = new BinaryFormatter();
    //     string path = Application.persistentDataPath + "/NPCData.bin";
    //     FileStream stream = new FileStream(path,FileMode.Append);

    // }
}
