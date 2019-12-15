using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

enum SpeedLevel
{
    Slow,
    Fast,
    Dangerous,
}
public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;

    public static GameManager I
    {
        get
        {
            if (m_instance != null)
            {
                return m_instance; // 已經註冊的Singleton物件
            }

            m_instance = FindObjectOfType<GameManager>();
            //尋找已經在Scene的Singleton物件:
            if (m_instance != null)
            {
                return m_instance;
            }

            GameObject go = new GameObject("GameManager");
            m_instance = go.AddComponent<GameManager>(); // 實時創建Singleton物件
            return m_instance;
        }
    }

    [SerializeField]
    private int m_speed = 6000;

    public int Speed
    {
        get { return m_speed; }
        set
        {
            m_speed = value;
            SetTimeScale();
        }
    }

    public int FaildCount = 0;

    [SerializeField]
    private int m_score = 0;

    [SerializeField]
    private Image m_speedBG;
    [SerializeField]
    private Text m_speedText;
    [SerializeField]
    private Text m_scoreText;

    [SerializeField]
    private Material m_roadMaterial;

    private void Start()
    {
        SetTimeScale();
    }

    private void SetTimeScale()
    {
        Time.timeScale = m_speed / 10000f;
        var level = SpeedLevel.Slow;
        if (m_speed > 10000)
            level = SpeedLevel.Fast;
        if (m_speed > 20000)
            level = SpeedLevel.Dangerous;
        
        DisplaySpeed(level);
        DisplayScore();
    }

    private void Update()
    {
        DisplayRoad();
    }

    private void DisplayRoad()
    {
        float offset = Time.time * Time.timeScale;
        m_roadMaterial.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }

    private void DisplaySpeed(SpeedLevel level)
    {
        m_speedText.text = m_speed.ToString();
        if (level == SpeedLevel.Slow)
        {
            m_speedBG.color = Color.white;
        }
        
        if (level == SpeedLevel.Fast)
        {
            m_speedBG.color = Color.yellow;
        }
        
        else if (level == SpeedLevel.Dangerous)
        {
            m_speedBG.color = Color.red;
        }
    }
    
    private void DisplayScore()
    {
        m_scoreText.text = m_score.ToString();
    }
    
    public void SetSpeed(int rpm)
    {
        m_score += (int)(Mathf.Abs(rpm) * Time.timeScale);
        Speed += rpm;
    }
}