using UnityEngine;

public class MenuController1 : MonoBehaviour
{
    public GameObject creditsPanel;
    private Animator creditsPanelAnimator;

    void Start()
    {
        {
            if (creditsPanel != null) {
                creditsPanelAnimator = creditsPanel.GetComponent<Animator>();
                creditsPanelAnimator.SetBool("_isOpen", false);
            }
        }
    }

    public void OpenCredits() {
        if (creditsPanelAnimator.GetBool("_isOpen")) {
            creditsPanelAnimator.SetBool("_isOpen", false);
        } else {
            creditsPanelAnimator.SetBool("_isOpen", true);
        }
    }
}
