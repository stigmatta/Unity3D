using UnityEngine;

public class MenuScript : MonoBehaviour
{
    private GameObject content;
    void Start()
    {
        content = transform.Find("Content").gameObject;
        Hide();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(content.activeInHierarchy)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }
    private void Show() 
    {
        content.SetActive(true);
        Time.timeScale = 0;
    }
    private void Hide()
    {
        content.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnValueChanged(float volume)
    {
        GameState.musicVolume = volume;
    }
    public void OnMuteChanged(bool muted)
    {
    }
}
