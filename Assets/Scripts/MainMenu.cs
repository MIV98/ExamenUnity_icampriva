using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject recordsPanel, menuPanel;
    [SerializeField] TMPro.TextMeshProUGUI textRecord;



    public void PlayButton()
    {
        SceneManager.LoadScene("Nivel1");
        audioSource.Play();
    }


    public void ShowRecords()
    {
        if (menuPanel.activeSelf)
        {
            menuPanel.SetActive(false);
            recordsPanel.SetActive(true);
        }

        textRecord.text = "";

        int[] records = SaveManager.LoadRecord();

        for (int i = 1; i <= records.Length; i++)
        {
            textRecord.text = textRecord.text + i.ToString() + ": " + records[i-1].ToString() + "\n";   
        }
    
        // textRecord.text = "1: " + SaveManager.LoadRecord().ToString();
    }

    public void ShowMenu()
    {
        if (recordsPanel.activeSelf)
        {
            recordsPanel.SetActive(false);
            menuPanel.SetActive(true);
        }
    }
}
