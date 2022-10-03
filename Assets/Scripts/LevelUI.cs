using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelUI : MonoBehaviour
{
    private int level;
    public GameObject levelDisplay;
    private void Awake()
    {
        level = SceneManager.GetActiveScene().buildIndex - 3;
    }
    void Start()
    {
        levelDisplay.GetComponent<Text>().text = "Floor " + level;
    }
}
