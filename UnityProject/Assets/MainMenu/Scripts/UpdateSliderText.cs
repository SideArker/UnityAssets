using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSliderText : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] bool showInfText;
    public void UpdateText()
    {
        TMP_Text text = GetComponent<TMP_Text>();

        text.text = Mathf.Round(slider.value).ToString();

        if (slider.value == slider.maxValue && showInfText) text.text = "INF";
    }

    public void ShowPopup()
    {
        transform.parent.gameObject.SetActive(true);
    }

    public void HidePopup()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
