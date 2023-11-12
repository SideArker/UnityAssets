using UnityEngine;

public class UpdateValuesOnChange : MonoBehaviour
{
    // Just so the code doesn't get cluttered in SettingsController.cs
    public static void UpdateShadows(SettingsData currentData, int shadowLevel)
    {
        switch (shadowLevel)
        {
            case 0:
                {
                    currentData.shadowMaskMode = ShadowmaskMode.Shadowmask;
                    currentData.shadowQuality = ShadowQuality.Disable;
                    currentData.shadowResolution = ShadowResolution.Low;
                    currentData.shadowProjection = ShadowProjection.CloseFit;
                    currentData.shadowDistance = 20;
                    currentData.shadowCascades = 0;
                    break;
                }
            case 1:
                {
                    currentData.shadowMaskMode = ShadowmaskMode.Shadowmask;
                    currentData.shadowQuality = ShadowQuality.HardOnly;
                    currentData.shadowResolution = ShadowResolution.Medium;
                    currentData.shadowProjection = ShadowProjection.CloseFit;
                    currentData.shadowDistance = 20;
                    currentData.shadowCascades = 0;
                    break;
                }
            case 2:
                {
                    currentData.shadowMaskMode = ShadowmaskMode.DistanceShadowmask;
                    currentData.shadowQuality = ShadowQuality.All;
                    currentData.shadowResolution = ShadowResolution.High;
                    currentData.shadowProjection = ShadowProjection.StableFit;
                    currentData.shadowDistance = 40;
                    currentData.shadowCascades = 2;
                    break;
                }
            case 3:
                {
                    currentData.shadowMaskMode = ShadowmaskMode.DistanceShadowmask;
                    currentData.shadowQuality = ShadowQuality.All;
                    currentData.shadowResolution = ShadowResolution.VeryHigh;
                    currentData.shadowProjection = ShadowProjection.StableFit;
                    currentData.shadowDistance = 80;
                    currentData.shadowCascades = 4;
                    break;
                }
            case 4:
                {
                    currentData.shadowMaskMode = ShadowmaskMode.DistanceShadowmask;
                    currentData.shadowQuality = ShadowQuality.All;
                    currentData.shadowResolution = ShadowResolution.VeryHigh;
                    currentData.shadowProjection = ShadowProjection.StableFit;
                    currentData.shadowDistance = 150;
                    currentData.shadowCascades = 4;
                    break;
                }
        }
    }


}
