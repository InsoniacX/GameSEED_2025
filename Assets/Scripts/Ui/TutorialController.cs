using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameObject page1;
    [SerializeField] private GameObject page2;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button closeButton;

    void Start()
    {
        // Pastikan hanya halaman pertama yang aktif di awal
        ShowPage1();
        Time.timeScale = 0;


        nextButton.onClick.AddListener(ShowPage2);
        prevButton.onClick.AddListener(ShowPage1);
        closeButton.onClick.AddListener(CloseTutorial);
    }

    void ShowPage1()
    {
        page1.SetActive(true);
        page2.SetActive(false);
    }

    void ShowPage2()
    {
        page1.SetActive(false);
        page2.SetActive(true);
    }

    void CloseTutorial()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}