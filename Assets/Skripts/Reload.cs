using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    public void ReloadingScenes()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
