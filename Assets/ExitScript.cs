using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
    public void ExitGame ()
    {
        SceneManager.LoadScene("Menu");
    }
}
