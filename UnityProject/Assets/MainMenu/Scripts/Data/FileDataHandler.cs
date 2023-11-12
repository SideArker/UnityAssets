using System;
using System.IO;
using UnityEngine;
public class FileDataHandler
{
    [Header("Path")]
    string dataDirectoryPath = "";
    string dataFileName = "";

    [Header("Encryption")]
    bool useEncryption = false;
    string encryptionCodeWord = "MNowicki";
    public FileDataHandler(string dataDirectoryPath, string dataFileName, bool useEncryption)
    {
        this.dataDirectoryPath = dataDirectoryPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public SettingsData Load()
    {
        string finalPath = Path.Combine(dataDirectoryPath, dataFileName);
        SettingsData loadedData = null;

        if (File.Exists(finalPath))
        {
            try
            {
                // Load serialized data
                string dataToLoad = "";
                using (FileStream stream = new FileStream(finalPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (useEncryption) dataToLoad = EncryptDecrypt(dataToLoad);

                // Deserialize Data
                loadedData = JsonUtility.FromJson<SettingsData>(dataToLoad);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error occured when loading player preferences " + finalPath + "\n" + ex);
            }
        }
        return loadedData;
    }



    public void Save(SettingsData data)
    {
        string finalPath = Path.Combine(dataDirectoryPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(finalPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            if (useEncryption) dataToStore = EncryptDecrypt(dataToStore);

            using (FileStream stream = new FileStream(finalPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error occured when saving player preferences " + finalPath + "\n" + ex);
        }
    }

    string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
