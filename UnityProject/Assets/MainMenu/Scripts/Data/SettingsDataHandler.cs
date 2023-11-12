using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SettingsDataHandler : MonoBehaviour
{
    [Header("File Saving Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private SettingsData settingsData;
    public static SettingsDataHandler instance { get; private set; }
    private IList<ISettingsDataHandler> settingsDataHandlers;
    private FileDataHandler dataHandler;


    private void Awake()
    {
        if (instance != null) Debug.Log("Too many instances of this object");
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.settingsDataHandlers = FindAllSettingDataHandlers();
        LoadData();
    }

    public void NewData()
    {
        this.settingsData = new SettingsData();
        this.settingsData.selectedWidth = Screen.currentResolution.width;
        this.settingsData.selectedHeight = Screen.currentResolution.height;
        dataHandler.Save(settingsData);
    }

    public void LoadData()
    {
        this.settingsData = dataHandler.Load();

        if (this.settingsData == null)
        {
            Debug.Log("No data found, creating new settings data");
            NewData();

            // Push all loaded data to other scripts
        }

        foreach (var handler in this.settingsDataHandlers)
        {
            handler.LoadData(settingsData);
        }
        Debug.Log("Loaded");
    }

    public void SaveData()
    {
        foreach (var handler in this.settingsDataHandlers)
        {
            handler.SaveData(settingsData);
        }
        Debug.Log("Saved");

        dataHandler.Save(settingsData);
    }

    private List<ISettingsDataHandler> FindAllSettingDataHandlers()
    {
        IEnumerable<ISettingsDataHandler> settingsDataHandlers = FindObjectsOfType<MonoBehaviour>()
            .OfType<ISettingsDataHandler>();

        return new List<ISettingsDataHandler>(settingsDataHandlers);
    }


}
