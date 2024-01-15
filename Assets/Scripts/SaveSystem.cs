using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static string m_playerFilePath = Application.persistentDataPath + "/player.dat";

    public static void SavePlayer(PlayerData playerData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(m_playerFilePath, FileMode.Open);

        formatter.Serialize(stream, playerData);

        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        if(File.Exists(m_playerFilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream s = new FileStream(m_playerFilePath, FileMode.Open);

            if(s.Length == 0)
                return null;

            PlayerData data = formatter.Deserialize(s) as PlayerData;

            s.Close();

            return data;
        }
        
        FileStream stream = File.Create(m_playerFilePath);

        return new PlayerData();
    }
    
}
