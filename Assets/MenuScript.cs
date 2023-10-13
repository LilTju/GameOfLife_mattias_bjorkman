using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuScript : MonoBehaviour
{
    public Slider cellSlider;
    public Slider mapSlider;
    public TextMeshProUGUI cellNumberText;
    public TextMeshProUGUI mapNumberText;
    public int cellNumber;
    public int mapNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cellNumber = (int)cellSlider.value;
        mapNumber = (int)mapSlider.value;

        cellNumberText.SetText(cellNumber.ToString());
        mapNumberText.SetText(mapNumber.ToString());
    }

    public void StartGame()
    {
        MuleScript.initialCellCount = cellNumber;
        MuleScript.initialMapSize = mapNumber;
        
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
