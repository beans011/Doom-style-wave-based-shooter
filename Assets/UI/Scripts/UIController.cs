using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject reloadText;

    //ranks
    public GameObject fRank;
    public GameObject dRank;
    public GameObject cRank;
    public GameObject bRank;
    public GameObject aRank;
    public GameObject sRank;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //displays text in specified component with TMP text component
    public void DisplayText(string textBox, string text)
    {
        //ensure the game object has a TMP text component
        GameObject textBoxObject = GameObject.Find(textBox);

        if (textBoxObject != null)
        {
            TextMeshProUGUI textMeshPro = textBoxObject.GetComponent<TextMeshProUGUI>();

            if (textMeshPro == null)
            {
                Debug.Log("No text box found.");
            }

            //set the text in the text component
            textMeshPro.text = text;
        }
    }

    public void SetReloadText(bool answer)
    {
        reloadText.SetActive(answer);
    }

    public void SetFRank()
    {
        fRank.SetActive(true);
        dRank.SetActive(false);
        cRank.SetActive(false);
        bRank.SetActive(false);
        aRank.SetActive(false);
        sRank.SetActive(false);
    }

    public void SetDRank()
    {
        fRank.SetActive(false);
        dRank.SetActive(true);
        cRank.SetActive(false);
        bRank.SetActive(false);
        aRank.SetActive(false);
        sRank.SetActive(false);
    }

    public void SetCRank()
    {
        fRank.SetActive(false);
        dRank.SetActive(false);
        cRank.SetActive(true);
        bRank.SetActive(false);
        aRank.SetActive(false);
        sRank.SetActive(false);
    }

    public void SetBRank()
    {
        fRank.SetActive(false);
        dRank.SetActive(false);
        cRank.SetActive(false);
        bRank.SetActive(true);
        aRank.SetActive(false);
        sRank.SetActive(false);
    }

    public void SetARank()
    {
        fRank.SetActive(false);
        dRank.SetActive(false);
        cRank.SetActive(false);
        bRank.SetActive(false);
        aRank.SetActive(true);
        sRank.SetActive(false);
    }

    public void SetSRank()
    {
        fRank.SetActive(false);
        dRank.SetActive(false);
        cRank.SetActive(false);
        bRank.SetActive(false);
        aRank.SetActive(false);
        sRank.SetActive(true);
    }
}
