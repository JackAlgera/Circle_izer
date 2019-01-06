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
	// Use this for initialization
	void Awake () {
		if(instance == null)
        {
            instance = this;
        }

        screenSizeX = 10f;
        screenSizeY = 10f;

        // TODO:Divide pixels into initColorList list

        initColorList = new Color[nbrOfCirclesX, nbrOfCirclesY];
        for (int i = 0; i < nbrOfCirclesY; i++)
        {
            for (int j = 0; j < nbrOfCirclesX; j++)
            {
                initColorList[i,j] = new Color((float)i / nbrOfCirclesY, 0, 0);
            }
        }

        GameObject newCircle = Instantiate(circle, transform.position, Quaternion.identity, circleHolder);
        newCircle.transform.GetChild(0).GetComponent<Circle>().InitSetup(nbrOfCirclesX, nbrOfCirclesY, initColorList, screenSizeX, screenSizeY);
	}
}
