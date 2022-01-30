using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public void LoadScene(int id) //Better way to make this a global? Caul/Canvas uses this too. Feels out of place
    {
        SceneManager.LoadScene(id);
    }
}
