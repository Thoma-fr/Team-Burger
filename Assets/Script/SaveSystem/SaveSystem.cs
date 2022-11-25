using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveGameData(float mainVolume, float musicVolume, float sfxVolume, float ambienceVolume)
    {
        string path = Application.persistentDataPath + "/Game.save";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream strem = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData(mainVolume, musicVolume, sfxVolume, ambienceVolume);

        formatter.Serialize(strem, data);
        strem.Close();
        Debug.Log("Game Saved Successfully");
    }

    public static SaveData LoadGameData()
    {
        string path = Application.persistentDataPath + "/Game.save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;

            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return new SaveData(0, 0, 0, 0);
        }
    }
}
