using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SettingsController : MonoBehaviour, ISettingsDataHandler
{
    // Optimise this later

    [Header("Main")]
    SettingsData currentData;

    [Header("Resolution")]
    [SerializeField] GameObject resolutionSelectionGameObject;

    [Header("Quality")]

    Dictionary<string, int> settingValues = new Dictionary<string, int>()
    {
        {"Quality", 3},
        {"Details", 3},
        {"Shadows", 3},
        {"Textures", 1},
        {"Particles", 3},
        {"AntiAlias", 2},
        {"MaxFps", -1},
        {"Master", 50},
        {"MainMenu", 100 },
        {"GameMusic", 100 },
        {"Sounds", 100 },
        {"VoiceActing", 100 }
    };

    string currentSetting = "";

    readonly float[] lodValuePerLevel = new float[] { 0.2f, 0.4f, 0.7f, 1f, 1.5f, 2f };
    readonly int[] particleValuePerLevel = new int[] { 4, 8, 64, 256, 2048, 4096 };

    // Functions

    public void ChangeCurrentSetting(string setting)
    {
        currentSetting = setting;
    }

    public void ChangeQualitySettingsLevel(int settingValue)
    {
        settingValues[currentSetting] = settingValue;

        if (currentSetting == "Shadows") UpdateValuesOnChange.UpdateShadows(currentData, settingValue);
        SettingsDataHandler.instance.SaveData();
    }

    public void ChangeSliderValue(Slider slider)
    {
        settingValues[currentSetting] = (int)slider.value;
        SettingsDataHandler.instance.SaveData();
    }
    public void ChangeBoolean(int boolIndex) // goes by indexes of booleans 0 - fullscreen, 1 - vsync
    {
        bool currentBoolean = false;

        switch (boolIndex)
        {
            case 0:
                currentData.fullScreen = !currentData.fullScreen; currentBoolean = currentData.fullScreen; break;
            case 1:
                currentData.vSync = !currentData.vSync; currentBoolean = currentData.vSync; break;
        }

        GameObject button = EventSystem.current.currentSelectedGameObject;
        if (button == null) return;
        button.transform.Find("Toggle").GetComponent<Toggle>().isOn = currentBoolean;
        SettingsDataHandler.instance.SaveData();
    }

    public void LoadData(SettingsData data)
    {
        currentData = data;
        settingValues["Quality"] = currentData.selectedQualityLevel;
        settingValues["Details"] = currentData.selectedDetailLevel;
        settingValues["Shadows"] = currentData.selectedShadowLevel;
        settingValues["Textures"] = currentData.selectedTextureLevel;
        settingValues["Particles"] = currentData.selectedParticleLevel;
        settingValues["AntiAlias"] = currentData.selectedAntiAlias;
        settingValues["MaxFps"] = currentData.maxFps;
        settingValues["MasterVolume"] = currentData.MasterVolume;
        settingValues["MainMenu"] = currentData.MainMenuVolume;
        settingValues["GameMusic"] = currentData.GameMusicVolume;
        settingValues["Sounds"] = currentData.SoundVolume;
        settingValues["VoiceActing"] = currentData.VoiceActingVolume;
        AssignValuesOnStart();
        UpdateGraphics();
    }
    public void SaveData(SettingsData data)
    {
        currentData.selectedQualityLevel = settingValues["Quality"];
        currentData.selectedDetailLevel = settingValues["Details"];
        currentData.selectedShadowLevel = settingValues["Shadows"];
        currentData.selectedTextureLevel = settingValues["Textures"];
        currentData.selectedParticleLevel = settingValues["Particles"];
        currentData.selectedAntiAlias = settingValues["AntiAlias"];
        currentData.maxFps = settingValues["MaxFps"];
        currentData.MasterVolume = settingValues["Master"];
        currentData.MainMenuVolume = settingValues["MainMenu"];
        currentData.GameMusicVolume = settingValues["GameMusic"];
        currentData.SoundVolume = settingValues["Sounds"];
        currentData.VoiceActingVolume = settingValues["VoiceActing"];
        UpdateGraphics();
    }

    void UpdateShadows()
    {
        QualitySettings.shadowmaskMode = currentData.shadowMaskMode;
        QualitySettings.shadows = currentData.shadowQuality;
        QualitySettings.shadowResolution = currentData.shadowResolution;
        QualitySettings.shadowProjection = currentData.shadowProjection;
        QualitySettings.shadowDistance = currentData.shadowDistance;
        QualitySettings.shadowCascades = currentData.shadowCascades;
    }

    public void UpdateGraphics()
    {
        QualitySettings.SetQualityLevel(currentData.selectedQualityLevel);
        QualitySettings.antiAliasing = currentData.selectedAntiAlias;
        QualitySettings.SetLODSettings(lodValuePerLevel[currentData.selectedDetailLevel], 0);
        if (currentData.selectedTextureLevel > 3) QualitySettings.globalTextureMipmapLimit = 3; else QualitySettings.globalTextureMipmapLimit = currentData.selectedTextureLevel;
        QualitySettings.streamingMipmapsActive = currentData.selectedTextureLevel == 4 ? QualitySettings.streamingMipmapsActive = true : QualitySettings.streamingMipmapsActive = false;
        QualitySettings.particleRaycastBudget = particleValuePerLevel[currentData.selectedParticleLevel];

        // Configure shadow settings
        UpdateShadows();

        // Change resolution
        Screen.SetResolution(currentData.selectedWidth, currentData.selectedHeight, currentData.fullScreen);

        // Enable/Disable Vsync
        if (currentData.vSync) QualitySettings.vSyncCount = 1;
        else QualitySettings.vSyncCount = 0;

        // Set Max Fps
        Application.targetFrameRate = currentData.maxFps;
    }

    void AssignValuesOnStart()
    {
        PanelInfo[] Panels = FindObjectsOfType<PanelInfo>(true).Where(x => x.assignValues).ToArray();

        string[] qualityLevels = { "Very Low", "Low", "Medium", "High", "Very High", "Ultra" };

        foreach (PanelInfo panel in Panels)
        {
            switch (panel.settingToAssign)
            {
                case "AntiAlias":
                    {
                        GameObject background = panel.transform.GetChild(0).gameObject;
                        if (settingValues["AntiAlias"] == 0) panel.defaultButton = background.transform.Find("Disabled").gameObject;
                        else if (settingValues["AntiAlias"] == 1) panel.defaultButton = background.transform.Find("2x").gameObject;
                        else if (settingValues["AntiAlias"] == 2) panel.defaultButton = background.transform.Find("4x").gameObject;
                        else if (settingValues["AntiAlias"] == 3) panel.defaultButton = background.transform.Find("8x").gameObject;
                        break;
                    }
                case "Textures":
                    {
                        break;
                    }
                case "MaxFps" or "Master" or "MainMenu" or "GameMusic" or "Sounds" or "VoiceActing":
                    {
                        Slider slider = panel.transform.GetChild(0).Find("Slider").GetComponent<Slider>();
                        slider.value = settingValues[panel.settingToAssign];
                        if (settingValues[panel.settingToAssign] == -1) slider.value = slider.maxValue;
                        break;
                    }
                default:
                    {
                        panel.defaultButton = panel.transform.GetChild(0).Find(qualityLevels[settingValues[panel.settingToAssign]]).gameObject;
                        break;
                    }
            }
        }
    }


    void Start()
    {
        Resolution[] resolutions;

        resolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == 60).ToArray(); ;

        for (int i = 0; i < resolutions.Length; i++)
        {
            GameObject currentObject;
            int objectResolutionWidth = resolutions[i].width;
            int objectResolutionHeight = resolutions[i].height;

            if (i == 0) currentObject = resolutionSelectionGameObject;
            else
            {
                currentObject = Instantiate(resolutionSelectionGameObject, resolutionSelectionGameObject.transform.parent);
            }
            currentObject.GetComponent<TMP_Text>().text = $"{objectResolutionWidth}x{objectResolutionHeight}";

            Button currentBtn = currentObject.GetComponent<Button>();

            currentBtn.onClick.AddListener(() =>
            {

                currentData.selectedWidth = objectResolutionWidth;
                currentData.selectedHeight = objectResolutionHeight;
                UpdateGraphics();
            });
        }
    }
}
