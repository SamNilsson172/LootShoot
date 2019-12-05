using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Serialization //serializes and deserializes savefiles
{
    //followed tutorial for this
    static readonly BinaryFormatter formatter = new BinaryFormatter();
    static readonly string path = Application.persistentDataPath + "/save.file";

    public static void Save(SaveFile saveFile)
    {
        FileStream stream = File.Create(path);

        formatter.Serialize(stream, saveFile);
        stream.Close();
    }

    public static SaveFile Load()
    {
        if (File.Exists(path))
        {
            FileStream stream = File.Open(path, FileMode.Open);
            SaveFile saveFile = (SaveFile)formatter.Deserialize(stream);
            stream.Close();
            return saveFile;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
