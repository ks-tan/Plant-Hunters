using UnityEngine;
using System.Collections;

public class UIEnergyBar : MonoBehaviour {
    public Rect m_Rectangle = new Rect(0, 0, 50, 4);
    public Vector2 m_Offset = new Vector2(-25, -25);

    Texture2D m_Foreground;

    ComponentEnergy m_Energy;

    // Use this for initialization
    void Start()
    {
       
        m_Foreground = new Texture2D(1, 1);
        m_Foreground.SetPixel(0, 0, Color.blue);
        m_Foreground.Apply();

        m_Energy = GetComponent<ComponentEnergy>();
    }

    void OnGUI()
    {
        m_Rectangle.x = Camera.main.WorldToScreenPoint(transform.position).x + m_Offset.x;
        m_Rectangle.y = Screen.height - Camera.main.WorldToScreenPoint(transform.position).y + m_Offset.y;

        Rect partialRect = m_Rectangle;
        partialRect.width = m_Rectangle.width * (m_Energy.FractionEnergy);
        partialRect.x = m_Rectangle.x;

        GUI.DrawTexture(partialRect, m_Foreground);
    }
}
