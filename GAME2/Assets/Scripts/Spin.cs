using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUI.PickerWheelUI;
using UnityEngine.UI;

public class Spin : MonoBehaviour
{
    // Start is called before the first frame update
    public Button btnSpin;
    public Text txtSpinBtn;
    public PickerWheel picker;
    void Start()
    {
        btnSpin.onClick.AddListener(() =>
        {
            btnSpin.interactable = false;
            txtSpinBtn.text = "spinning";
            picker.OnSpinStart(() =>
            {
                Debug.Log("Onspin");
               
            }
            );       
            picker.OnSpinEnd( picker =>
            {
                Debug.Log("spin end : Label :"+ picker.Label + " ,Amount: " + picker.Amount);
                btnSpin.interactable = true;
                txtSpinBtn.text = "spin";
            }
            );
            picker.Spin();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
