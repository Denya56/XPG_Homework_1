using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _pauseText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(Axis.SUBMIT))
        {
            Time.timeScale = 1f;
            _pauseText.enabled = false;
        }
    }
}
