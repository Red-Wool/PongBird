using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlChange : MonoBehaviour
{
    [SerializeField] private GameObject selectPanel;

    bool selecting; 
    GameControl selectedControl;

    private void Start()
    {
        selectPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (selecting && Input.anyKeyDown)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) //Copy pasted code from unity questions LETS GO
            {
                if (Input.GetKeyDown(vKey))
                {
                    ControlManager.instance.ControlChange(selectedControl, vKey);

                    selecting = false;
                    selectPanel.SetActive(false);
                }
            }
        }
    }

    public void ChangeControl(GameControl control)
    {
        selectedControl = control;

        selecting = true;
        selectPanel.SetActive(true);
    }
}
