using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController1 : MonoBehaviour
{
    public GameObject creditsPanel;
    private Animator creditsPanelAnimator;
    
    public GameObject settingsPanel;
    private Animator settingsPanelAnimator;
    Resolution[] resolutions;
    public Dropdown settingsResolutionDropdown;

    public GameObject howToPlayPanel;
    private Animator howToPlayPanelAnimator;
    private int howToPlayPageIndex;
    [SerializeField] private string[] howToPlayText;
    [SerializeField] private TextMeshProUGUI howToPlayCurrentText;
    [SerializeField] private TextMeshProUGUI howToPlayPageCounter;

    
    void Start()
    {
        resolutions = Screen.resolutions;
        if (creditsPanel != null) {
            creditsPanelAnimator = creditsPanel.GetComponent<Animator>();
            creditsPanelAnimator.SetBool("_isOpen", false);
        }
        if (settingsPanel != null) {
            settingsPanelAnimator = settingsPanel.GetComponent<Animator>();
            settingsPanelAnimator.SetBool("_isOpen", false);
            settingsResolutionDropdown.ClearOptions();
            List<string> options = new List<string>();
            for (int i = 0; i < resolutions.Length; i++) {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);
            }
            settingsResolutionDropdown.AddOptions(options);
        }
        if (howToPlayPanel != null) {
            howToPlayPanelAnimator = howToPlayPanel.GetComponent<Animator>();
            howToPlayPanelAnimator.SetBool("_isOpen", false);
            howToPlayPageIndex = 0;
            SetHowToPlay();
        }

        
    }

    //Credits related things go here:
    public void OpenCredits() {
        if (creditsPanelAnimator.GetBool("_isOpen")) {
            creditsPanelAnimator.SetBool("_isOpen", false);
        } else {
            creditsPanelAnimator.SetBool("_isOpen", true);
        }
    }

    //Settings go here
    public void OpenSettings() {
        if (settingsPanelAnimator.GetBool("_isOpen")) {
            settingsPanelAnimator.SetBool("_isOpen", false);
        } else {
            settingsPanelAnimator.SetBool("_isOpen", true);
        }
    }

    public void SettingFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    //How to play functions go here
    public void OpenHowToPlay() {
        if (howToPlayPanelAnimator.GetBool("_isOpen")) {
            howToPlayPanelAnimator.SetBool("_isOpen", false);
        } else {
            howToPlayPageIndex = 0;
            SetHowToPlay();
            howToPlayPanelAnimator.SetBool("_isOpen", true);
        }
    }
    private void SetHowToPlay() {
        howToPlayPageCounter.text = howToPlayPageIndex + 1 + "/" + howToPlayText.Length;
        howToPlayCurrentText.text = howToPlayText[howToPlayPageIndex];
    }

    public void NextPageHowToPlay() {
        if (howToPlayPageIndex < howToPlayText.Length - 1) {
            howToPlayPageIndex++;
            SetHowToPlay();
        }
    }

    public void PreviousPageHowToPlay() {
        if (howToPlayPageIndex > 0) {
            howToPlayPageIndex--;
            SetHowToPlay();
        }
    }

    //Quit game
    public void QuitGame() {
        Application.Quit();
    }
}
