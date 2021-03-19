using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public void goTitle()
    {
        SceneManager.LoadScene("Start");
    }
    public void goSite()
    {
        Application.OpenURL("https://m.place.naver.com/place/1100586279/home");
    }
}
