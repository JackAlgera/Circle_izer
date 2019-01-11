using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public Transform circleHolder;
    public GameObject circle;

    public float screenSizeX;
    public float screenSizeY;

    public int nbrOfCirclesX;
    public int nbrOfCirclesY;

    public Color[,] initColorList;
    public Texture2D image;

	// Use this for initialization
	void Awake () {
		if(instance == null)
        {
            instance = this;
        }

        screenSizeX = 13.4f;
        screenSizeY = 10f;

        initColorList = new Color[nbrOfCirclesX, nbrOfCirclesY];

        SetInitColorList();
        FlipInitColorList();
        /*
        for (int i = 0; i < nbrOfCirclesY; i++)
        {
            for (int j = 0; j < nbrOfCirclesX; j++)
            {
                initColorList[i,j] = new Color((float)i / nbrOfCirclesY, 0, 0);
            }
        }
        */

        GameObject newCircle = Instantiate(circle, transform.position, Quaternion.identity, circleHolder);
        newCircle.transform.GetChild(0).GetComponent<Circle>().InitSetup(nbrOfCirclesX, nbrOfCirclesY, initColorList, screenSizeX, screenSizeY);
        StartCoroutine(MouseOver());
    }

    public void SetInitColorList()
    {
        float PPCircleWidth= (float)image.width / (float)nbrOfCirclesX;
        float PPCircleHeight = (float)image.height / (float)nbrOfCirclesY;

        float totWidth = 0f;
        float totHeight = 0f; 

        for (int i = 0; i < nbrOfCirclesY; i++)
        {
            totWidth = 0f;
            for (int j = 0; j < nbrOfCirclesX; j++)
            {
                float R = 0f;
                float G = 0f;
                float B = 0f;

                Color[] currentPixels = image.GetPixels(Mathf.FloorToInt(totWidth), Mathf.FloorToInt(totHeight), Mathf.FloorToInt(PPCircleWidth), Mathf.FloorToInt(PPCircleHeight));
                foreach(Color pixel in currentPixels)
                {
                    R += pixel.r;
                    G += pixel.g;
                    B += pixel.b;
                }

                R /= currentPixels.Length;
                G /= currentPixels.Length;
                B /= currentPixels.Length;

                initColorList[j, i] = new Color(R, G, B);

                totWidth += PPCircleWidth;
            }
            totHeight += PPCircleHeight;
        }
    }

    IEnumerator MouseOver()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos.x = (mousePos.x / Screen.width)*screenSizeX - screenSizeX / 2;
        mousePos.y = (mousePos.y / Screen.height) * screenSizeY - screenSizeY / 2;

        Collider2D[] circlesToPop = Physics2D.OverlapCircleAll(mousePos, 0.25f);

        foreach(Collider2D circle in circlesToPop)
        {
            circle.transform.GetComponent<Circle>().DivideIntoFour();
        }

        yield return new WaitForEndOfFrame();
        StartCoroutine(MouseOver());
    }

    public void FlipInitColorList()
    {
        Color[,] newInitColorList = new Color[nbrOfCirclesX, nbrOfCirclesY];
        for (int i = 0; i < nbrOfCirclesY; i++)
        {
            for (int j = 0; j < nbrOfCirclesX; j++)
            {
                newInitColorList[j, i] = initColorList[j, nbrOfCirclesY - i - 1];
            }
        }

        initColorList = newInitColorList;
    }
}
