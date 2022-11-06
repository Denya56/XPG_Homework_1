using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    //[SerializeField] private TMP_InputField _field;
    /*private void Update()
    {
        //Debug.Log(_field.text);
        if( _field.text.Equals(""))
        {
            Debug.Log("nope");
            _field.image.color = Color.green;
        }
            
    }*/

    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene(SceneConst.LEVEL1);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    public void Exit()
    {
        Application.Quit();
    }
}
