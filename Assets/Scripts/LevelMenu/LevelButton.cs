using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private int level;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("Button component not found!");
            return;
        }

        button.interactable = false;
    }

    void Start()
    {
        CurrentLevel currentLevel = CurrentLevel.Instance;
        if (currentLevel != null && currentLevel.LevelNumber >= level)
        {
            button.interactable = true;
        }
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        Debug.Log("Selected Level: " + level.ToString());
        SceneManager.LoadSceneAsync("Level" + level.ToString());
    }
}
