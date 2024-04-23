using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MesgBar : MonoBehaviour
{
	public static MesgBar instance;
    [Header("Holder")]
	public GameObject holder;

	[Header("Mesg Bar")]
	public Image mesgBar;
	public Text mesgBarText;
	public Vector3 mesgBarPos;
	public GameObject overlay;


	public Color errorColor;
	public Color mesgColor;

    public static MesgBar Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MesgBar>();

                if (instance == null)
                {
                    GameObject mesgBar = new GameObject(typeof(MesgBar).Name);
                    instance = mesgBar.AddComponent<MesgBar>();
                }
            }
            return instance;
        }
    }
    void Awake()
	{
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        holder.SetActive(false);
		overlay.SetActive(false);
		hide();
	}

	private void Start()
	{
		mesgBarPos = mesgBar.gameObject.transform.localPosition;
	}

	public void show(string mesg,bool isError = false)
	{
		holder.SetActive(true);
		mesgBar.gameObject.SetActive(true);
		mesgBarText.text = mesg;
		mesgBar.gameObject.transform.localPosition = new Vector3(mesgBarPos.x, mesgBarPos.y + 100, mesgBarPos.z);
		LeanTween.moveLocalY(mesgBar.gameObject, mesgBarPos.y, .8f).setEase(LeanTweenType.easeOutExpo);

		if (isError)
			mesgBar.color = errorColor;
		else
			mesgBar.color = mesgColor;

		Invoke("hide",3);
	}
	public void hide()
	{
		if (mesgBar.gameObject.activeInHierarchy)
		{
			mesgBar.gameObject.SetActive(false);
			mesgBarText.text = "";
		}

		if(holder.activeInHierarchy)
			holder.SetActive(false);
	}
}