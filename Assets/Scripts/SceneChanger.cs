using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private Fade fade;

    public void ChangeScene(string sceneName)
    {
        fade.FadeIn(() => SceneManager.LoadScene(sceneName));
    }
}
