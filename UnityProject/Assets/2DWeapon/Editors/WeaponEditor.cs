#region Editor
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
    SerializedProperty m_bullet;
    SerializedProperty m_shootingStyle;
    SerializedProperty m_damage;
    SerializedProperty m_fireRate;
    SerializedProperty m_bulletSpeed;
    SerializedProperty m_bulletRange;
    SerializedProperty m_burstCount;
    SerializedProperty m_spreadRange;
    SerializedProperty m_timeBetweenBurstShots;
    SerializedProperty m_burstRestTime;
    SerializedProperty m_endLag;
    SerializedProperty m_cameraShake;
    SerializedProperty m_shakeIntensity;
    SerializedProperty m_shakeTime;
    SerializedProperty m_enableKnockback;
    SerializedProperty m_knockback;
    SerializedProperty m_selfKnockback;
    SerializedProperty m_canPierce;
    SerializedProperty m_pierceCount;
    private void OnEnable()
    {
        m_bullet = serializedObject.FindProperty("bullet");
        m_shootingStyle = serializedObject.FindProperty("ShootingStyle");
        m_damage = serializedObject.FindProperty("damage");
        m_fireRate = serializedObject.FindProperty("fireRate");
        m_bulletSpeed = serializedObject.FindProperty("bulletSpeed");
        m_bulletRange = serializedObject.FindProperty("bulletRange");
        m_burstCount = serializedObject.FindProperty("burstCount");
        m_spreadRange = serializedObject.FindProperty("spreadRange");
        m_timeBetweenBurstShots = serializedObject.FindProperty("timeBetweenBurstShots");
        m_burstRestTime = serializedObject.FindProperty("burstRestTime");
        m_endLag = serializedObject.FindProperty("endLag");
        m_cameraShake = serializedObject.FindProperty("cameraShake");
        m_shakeIntensity = serializedObject.FindProperty("shakeIntensity");
        m_shakeTime = serializedObject.FindProperty("shakeTime");
        m_enableKnockback = serializedObject.FindProperty("enableKnockback");
        m_knockback = serializedObject.FindProperty("knockback");
        m_selfKnockback = serializedObject.FindProperty("selfKnockback");
        m_canPierce = serializedObject.FindProperty("enablePierce");
        m_pierceCount = serializedObject.FindProperty("pierceCount");
    }

    public override void OnInspectorGUI()
    {

        serializedObject.Update();
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.LabelField("Main", EditorStyles.boldLabel);

        m_bullet.objectReferenceValue = EditorGUILayout.ObjectField("Bullet", m_bullet.objectReferenceValue, typeof(GameObject), false) as GameObject;

        if (!m_bullet.objectReferenceValue)
        {
            GUIStyle red = new(EditorStyles.boldLabel);
            red.normal.textColor = Color.red;
            EditorGUILayout.LabelField("WARNING. Bullet prefab is not set!", red);
        }

        m_shootingStyle.enumValueIndex = (int)(ShootingStyle)EditorGUILayout.EnumPopup("Weapon Shooting Style", (ShootingStyle) m_shootingStyle.enumValueIndex);

        EditorGUILayout.Space();

        switch ((ShootingStyle) m_shootingStyle.enumValueIndex)
        {
            case ShootingStyle.Normal:
                EditorGUILayout.LabelField("Normal Weapon", EditorStyles.boldLabel);
                m_damage.floatValue = EditorGUILayout.FloatField("Damage", m_damage.floatValue);
                m_fireRate.floatValue = EditorGUILayout.FloatField("Fire Rate", m_fireRate.floatValue);
                m_bulletSpeed.floatValue = EditorGUILayout.FloatField("Bullet Speed", m_bulletSpeed.floatValue);
                m_bulletRange.intValue = EditorGUILayout.IntField("Bullet Range", m_bulletRange.intValue);
                break;
            case ShootingStyle.Burst:
                EditorGUILayout.LabelField("Burst Weapon", EditorStyles.boldLabel);
                m_burstCount.intValue = EditorGUILayout.IntField("Burst Shot Count", m_burstCount.intValue);
                m_spreadRange.intValue = EditorGUILayout.IntSlider("Shot Spread Range", m_spreadRange.intValue, 1, 360);
                m_timeBetweenBurstShots.floatValue = EditorGUILayout.FloatField("Time Between Shots", m_timeBetweenBurstShots.floatValue);
                m_burstRestTime.floatValue = EditorGUILayout.FloatField("Rest Time", m_burstRestTime.floatValue);
                break;

        }
        m_endLag.floatValue = EditorGUILayout.FloatField("End Lag", m_endLag.floatValue);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Camera Shake", EditorStyles.boldLabel);
        m_cameraShake.boolValue = EditorGUILayout.Toggle("Enable Camera Shake", m_cameraShake.boolValue);
        if (m_cameraShake.boolValue)
        {
            m_shakeIntensity.floatValue = EditorGUILayout.FloatField("Shake Intensity", m_shakeIntensity.floatValue);
            m_shakeTime.floatValue = EditorGUILayout.FloatField("Shake Time", m_shakeTime.floatValue);
        }
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Other", EditorStyles.boldLabel);

        m_enableKnockback.boolValue = EditorGUILayout.Toggle("Knockback", m_enableKnockback.boolValue);

        if (m_enableKnockback.boolValue)
        {
            m_knockback.intValue = EditorGUILayout.IntField("Knockback", m_knockback.intValue);
            m_selfKnockback.intValue = EditorGUILayout.IntField("Self Knockback", m_selfKnockback.intValue);
        }

        m_canPierce.boolValue = EditorGUILayout.Toggle("Enable Pierce", m_canPierce.boolValue);


        if (m_canPierce.boolValue)
        {
            m_pierceCount.intValue = EditorGUILayout.IntField("Pierce Count", m_pierceCount.intValue);
        }

        EditorGUI.EndChangeCheck();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif

#endregion
