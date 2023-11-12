using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Hover")]
    [SerializeField] Color hoverColor;
    [SerializeField] Color baseColor;
    [SerializeField] FontStyles fontStyleOnHover;
    public void OnHover(GameObject hoverTarget)
    {
        TMP_Text targetText = hoverTarget.GetComponent<TMP_Text>();
        targetText.color = hoverColor;
        targetText.fontStyle = fontStyleOnHover;


    }

    public void OnHoverExit(GameObject hoverTarget)
    {
        TMP_Text targetText = hoverTarget.GetComponent<TMP_Text>();
        targetText.color = baseColor;
        targetText.fontStyle = FontStyles.Normal;
    }
}
