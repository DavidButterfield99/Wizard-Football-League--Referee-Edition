using UnityEngine;

public class MenuController1 : MonoBehaviour
{
    public GameObject creditsPanel;
    private Animator creditsPanelAnimator;
    public GameObject settingsPanel;
    private Animator settingsPanelAnimator;

    void Start()
    {
        if (creditsPanel != null) {
            creditsPanelAnimator = creditsPanel.GetComponent<Animator>();
            creditsPanelAnimator.SetBool("_isOpen", false);
        }
        if (settingsPanel != null) {
            settingsPanelAnimator = settingsPanel.GetComponent<Animator>();
            settingsPanelAnimator.SetBool("_isOpen", false);
        }
    }


    public void OpenCredits() {
        if (creditsPanelAnimator.GetBool("_isOpen")) {
            creditsPanelAnimator.SetBool("_isOpen", false);
        } else {
            creditsPanelAnimator.SetBool("_isOpen", true);
        }
    }

    public void OpenSettings() {
        if (settingsPanelAnimator.GetBool("_isOpen")) {
            settingsPanelAnimator.SetBool("_isOpen", false);
        } else {
            settingsPanelAnimator.SetBool("_isOpen", true);
        }
    }
}
