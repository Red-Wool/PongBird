using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlButton : MonoBehaviour
{
    [SerializeField] private ControlChange change;
    [SerializeField] private GameControl control;
    private KeyCode resetKey;

    [SerializeField] private TMP_Text controlDisplay;

    // Start is called before the first frame update
    void Start()
    {
        resetKey = ControlManager.instance.GetKey(control);
    }

    public void ResetKey()
    {
        ControlManager.instance.ControlChange(control, resetKey);
    }

    public void SetKey()
    {
        change.ChangeControl(control);
    }

    // Update is called once per frame
    void Update()
    {
        controlDisplay.text = ControlManager.instance.GetKey(control).ToString();
    }
}
