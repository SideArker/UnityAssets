using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] GameObject currentPanel;

    [Header("Selection")]

    [SerializeField] GameObject currentIndicator;
    [SerializeField] GameObject currentObject;

    [SerializeField] Color ButtonColor;
    [SerializeField] Color ButtonColorWhenSelected;

    [Header("Scene Changing")]
    [SerializeField] GameObject sceneChangeAnimation;

    EventSystem m_EventSystem;

    void Start()
    {
        m_EventSystem = EventSystem.current;
        m_EventSystem.SetSelectedGameObject(currentObject);
    }


    private void Update()
    {
        if (!m_EventSystem.currentSelectedGameObject) m_EventSystem.SetSelectedGameObject(currentObject);

        // Check for all movement keys
        if (
            Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            if (m_EventSystem.currentSelectedGameObject == currentObject) return;

            ActivateIndicatorAnimation(m_EventSystem.currentSelectedGameObject);
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            ChangePanel(currentPanel.GetComponent<PanelInfo>().parentPanel);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeScene(int sceneIndex)
    {
        Animator anim = sceneChangeAnimation.GetComponent<Animator>();
        sceneChangeAnimation.SetActive(true);
        anim.Play("SceneChangeAnim");
        anim.SetInteger("SceneIndex", sceneIndex);
    }

    public void ChangePanel(GameObject panel)
    {
        PanelInfo parentPanelInfo = currentPanel.GetComponent<PanelInfo>();
        parentPanelInfo.parentPanel.SetActive(false);
        parentPanelInfo.lastButton = m_EventSystem.currentSelectedGameObject;

        currentPanel.SetActive(false);

        panel.SetActive(true);
        currentPanel = panel;

        currentObject.GetComponent<TMP_Text>().color = ButtonColor;
        currentObject.transform.GetChild(0).gameObject.SetActive(false);

        PanelInfo newPanelInfo = currentPanel.GetComponent<PanelInfo>();

        if (newPanelInfo.lastButton != null) currentObject = newPanelInfo.lastButton;
        else currentObject = newPanelInfo.defaultButton;

        m_EventSystem.SetSelectedGameObject(currentObject);
        ActivateIndicatorAnimation(currentObject);
    }

    void ActivateIndicatorAnimation(GameObject gameObject)
    {
        // Activates selection indicator
        GameObject indicator = gameObject.transform.GetChild(0).gameObject;
        currentIndicator.SetActive(false);
        indicator.SetActive(true);
        currentIndicator = indicator;

        // If button change button color

        if (currentObject.GetComponent<Button>())
        {
            // change font to normal
            TMP_Text currentBtnText = currentObject.GetComponent<TMP_Text>();
            currentBtnText.fontStyle = FontStyles.Normal;
            currentBtnText.color = ButtonColor;
        }
        if (gameObject.GetComponent<Button>())
        {
            TMP_Text btnText = gameObject.GetComponent<TMP_Text>();
            btnText.color = ButtonColorWhenSelected;
            btnText.fontStyle = FontStyles.Italic;
        }
        currentObject = gameObject;
    }


    public void OnHoverEnter(GameObject self)
    {
        if (currentObject == self) return;

        // Change current selected game object so you can easily use your keyboard for the menu
        m_EventSystem.SetSelectedGameObject(currentObject);

        ActivateIndicatorAnimation(self);
    }
}