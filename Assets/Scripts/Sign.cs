using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Sign : MonoBehaviour
{
    public string HintText;
    public GameObject PressEForHint;
    public GameObject HintBox;
    public GameObject Close;
    public GameObject BackGroundImage;
    public Movement SetOff;
    // Start is called before the first frame update
    void Start()
    {
        HintBox.SetActive(false);
        PressEForHint.SetActive(false);
        Close.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                HintBox.SetActive(true);
                Close.SetActive(true);
                PressEForHint.SetActive(false);
                BackGroundImage.SetActive(true);
                SetOff.Off = true;
                HintBox.GetComponent<TextMeshProUGUI>().text = HintText;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.visible = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        PressEForHint.SetActive(true);

    }
    private void OnTriggerExit(Collider other)
    {
        HintBox.SetActive(false);
        PressEForHint.SetActive(false);
        Close.SetActive(false);
        BackGroundImage.SetActive(false);
        SetOff.Off = false;
    }
    public void CloseButton()
    {
        HintBox.SetActive(false);
        Close.SetActive(false);
        BackGroundImage.SetActive(false);
        SetOff.Off = false;
    }
}
