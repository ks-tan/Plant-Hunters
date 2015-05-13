using UnityEngine;
using System.Collections;

public class UIFloatingText : MonoBehaviour {
    public GameObject textObj;
    public static UIFloatingText current;
    public float offsetY = 2f;
    public int fontSize = 20;
    public float textLifetime = 2f;
    void Awake()
    {
        current = this;
    }

	public void Show(Vector3 position, string content, Color c) {
        position.y = position.y + offsetY;
        GameObject txt = Instantiate(textObj, position,
            Quaternion.identity) as GameObject ;
        TextMesh guiText = txt.GetComponent<TextMesh>();
        guiText.fontSize = fontSize;

        int amt;
        bool isNumeric = int.TryParse(content, out amt);
        if(isNumeric) {
            guiText.text = content;
            guiText.color = c;
            Destroy(txt, textLifetime);
        }
        else
        {
            guiText.text = content;
            guiText.color = Color.black;
            Destroy(txt, 1f);
        }
        
  
    }
	

}
