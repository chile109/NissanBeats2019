
using UnityEngine;
using  UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void JumpScene()
    {
        SceneManager.LoadScene("main");
    }
}
