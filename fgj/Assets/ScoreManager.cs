using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public ScoreManager Instance;
    void Awake()
    {
        //Debug.LogError(SceneManager.GetActiveScene().name);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Warning: multiple " + this + " in scene!");
        }
    }

    float runningTime = 0;
    float maxTime = 1000;

    public Text text;

    float GetScore() {
        float time = runningTime;
        if (time < 0) {
            time = maxTime; // :)
        }

        return Mathf.Clamp(maxTime - time, 0, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        runningTime += Time.deltaTime;
        text.text = ((int)GetScore()).ToString();
    }
}
