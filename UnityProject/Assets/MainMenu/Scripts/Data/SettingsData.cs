using UnityEngine;

[System.Serializable]
public class SettingsData
{
    [Header("Resolution")]
    public int selectedWidth;
    public int selectedHeight;

    [Header("Quality")]
    public int selectedQualityLevel;
    public int selectedDetailLevel;
    public int selectedShadowLevel;
    public int selectedTextureLevel;
    public int selectedParticleLevel;
    public int selectedAntiAlias;

    [Header("Shadows")]
    public ShadowmaskMode shadowMaskMode;
    public ShadowQuality shadowQuality;
    public ShadowResolution shadowResolution;
    public ShadowProjection shadowProjection;
    public int shadowDistance;
    public int shadowCascades;

    [Header("Volume")]
    public int MasterVolume;
    public int MainMenuVolume;
    public int GameMusicVolume;
    public int SoundVolume;
    public int VoiceActingVolume;

    [Header("Other")]
    public int maxFps;
    public bool fullScreen;
    public bool vSync;
    public SettingsData()
    {
        // Quality
        selectedQualityLevel = 3; // high is default
        selectedDetailLevel = 3;
        selectedShadowLevel = 3;
        selectedTextureLevel = 1;
        selectedParticleLevel = 3;
        selectedAntiAlias = 2;

        //Shadows
        shadowMaskMode = ShadowmaskMode.DistanceShadowmask;
        shadowQuality = ShadowQuality.All;
        shadowResolution = ShadowResolution.VeryHigh;
        shadowProjection = ShadowProjection.StableFit;
        shadowDistance = 80;
        shadowCascades = 4;

        //Volume
        MasterVolume = 50;
        MainMenuVolume = 100;
        GameMusicVolume = 100;
        SoundVolume = 100;
        VoiceActingVolume = 100;

        // Other
        maxFps = -1;
        fullScreen = true;
        vSync = false;

    }
}
