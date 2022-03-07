using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    private Rigidbody _handRb;
    private float _handSpeed;
    public GameObject handSpeedTextObj;
    private Text _handSpeedText;
    // Start is called before the first frame update
    void Start()
    {
        _handRb = this.gameObject.GetComponent<Rigidbody>();
        _handSpeedText = handSpeedTextObj.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _handSpeed = _handRb.velocity.magnitude;
        _handSpeedText.text = _handSpeed.ToString();
    }
}
