using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject panel;

    public InputField input;
    public Text hintText;

    public CubeMaker cubeMaker;

    public void ConfirmCount()
    {
        bool parsed = int.TryParse(input.text, out int value);
        if (!parsed)
        {
            hintText.text = "Please Enter Valid Number";
            input.text = "";
            return;
        }
        
        if(value > 100 || value < 1)
        {
            hintText.text = "Enter Value between 1 and 100 (Inclusive)";
            input.text = "";
            return;
        }

        cubeMaker.numberOfCubes = value;
        cubeMaker.BuildMania();

        panel.SetActive(false);
    }
}
