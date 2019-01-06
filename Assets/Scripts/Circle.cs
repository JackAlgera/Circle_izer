using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {
    
    public Color currentColor;

    public Color[,] colorList;
    public float sizeCircleX;
    public float sizeCircleY;
    public int sizeListX;
    public int sizeListY;
    public bool canDivide;
    
    public void InitSetup(int sizeListX, int sizeListY, Color[,] colorList, float sizeCircleX, float sizeCircleY)
    {
        if ((colorList.GetLength(0) * colorList.GetLength(1)) >= 4)
        {
            canDivide = true;
        }
        else
        {
            canDivide = false;
        }
        this.sizeListX = sizeListX;
        this.sizeListY = sizeListY;
        this.colorList = colorList;

        this.sizeCircleX = sizeCircleX;
        this.sizeCircleY = sizeCircleY;

        AverageColor();
    }

    private void OnMouseOver()
    {
        if (canDivide)
        {
            DivideIntoFour();
        }
    }

    private void OnMouseEnter()
    {
    }

    public void AverageColor()
    {
        float R = 0;
        float G = 0;
        float B = 0;
        for (int i = 0; i < colorList.GetLength(0); i++)
        {
            for (int j = 0; j < colorList.GetLength(1); j++)
            {
                Color currentPixel = colorList[i, j];
                R += currentPixel.r;
                G += currentPixel.g;
                B += currentPixel.b;
            }
        }

        R /= colorList.GetLength(0) * colorList.GetLength(1);
        G /= colorList.GetLength(0) * colorList.GetLength(1);
        B /= colorList.GetLength(0) * colorList.GetLength(1);

        currentColor = new Color(R, G, B);
        gameObject.GetComponent<SpriteRenderer>().color = currentColor;
    }

    public void DivideIntoFour()
    {
        TopLeft();
        TopRight();
        BotLeft();
        BotRight();

        Destroy(gameObject);
    }

    public void TopLeft()
    {
        Color[,] newColorList = new Color[sizeListX/2, sizeListY / 2];

        for (int i = 0; i < sizeListY / 2; i++)
        {
            for (int j = 0; j < sizeListX / 2; j++)
            {
                newColorList[i,j] = colorList[i,j];
            }
        }

        Vector2 circlePos = transform.position;
        circlePos.x -= sizeCircleX / 4;
        circlePos.y += sizeCircleY / 4;
        GameObject circ = Instantiate(GameController.instance.circle, circlePos, Quaternion.identity, GameController.instance.circleHolder);
        circ.transform.GetChild(0).GetComponent<Circle>().InitSetup(sizeListX / 2, sizeListY / 2, newColorList, sizeCircleX / 2, sizeCircleY / 2);

        Vector3 temp = transform.localScale;
        temp.x /= 2;
        temp.y /= 2;
        temp.z /= 2;

        circ.transform.GetChild(0).localScale = temp;
    }

    public void TopRight ()
    {
        Color[,] newColorList = new Color[sizeListX / 2, sizeListY / 2];

        for (int i = 0; i < sizeListY / 2; i++)
        {
            for (int j = 0; j < sizeListX / 2; j++)
            {
                newColorList[i, j] = colorList[i, j + sizeListX / 2];
            }
        }

        Vector2 circlePos = transform.position;
        circlePos.x += sizeCircleX / 4;
        circlePos.y += sizeCircleY / 4;
        GameObject circ = Instantiate(GameController.instance.circle, circlePos, Quaternion.identity, GameController.instance.circleHolder);
        circ.transform.GetChild(0).GetComponent<Circle>().InitSetup(sizeListX / 2, sizeListY / 2, newColorList, sizeCircleX / 2, sizeCircleY / 2);

        Vector3 temp = transform.localScale;
        temp.x /= 2;
        temp.y /= 2;
        temp.z /= 2;

        circ.transform.GetChild(0).localScale = temp;
    }

    public void BotLeft()
    {
        Color[,] newColorList = new Color[sizeListX / 2, sizeListY / 2];

        for (int i = 0; i < sizeListY / 2; i++)
        {
            for (int j = 0; j < sizeListX / 2; j++)
            {
                newColorList[i, j] = colorList[i + sizeListY / 2, j];
            }
        }

        Vector2 circlePos = transform.position;
        circlePos.x -= sizeCircleX / 4;
        circlePos.y -= sizeCircleY / 4;
        GameObject circ = Instantiate(GameController.instance.circle, circlePos, Quaternion.identity, GameController.instance.circleHolder);
        circ.transform.GetChild(0).GetComponent<Circle>().InitSetup(sizeListX / 2, sizeListY / 2, newColorList, sizeCircleX / 2, sizeCircleY / 2);

        Vector3 temp = transform.localScale;
        temp.x /= 2;
        temp.y /= 2;
        temp.z /= 2;

        circ.transform.GetChild(0).localScale = temp;
    }

    public void BotRight()
    {
        Color[,] newColorList = new Color[sizeListX / 2, sizeListY / 2];

        for (int i = 0; i < sizeListY / 2; i++)
        {
            for (int j = 0; j < sizeListX / 2; j++)
            {
                newColorList[i, j] = colorList[i + sizeListY / 2, j + sizeListX / 2];
            }
        }

        Vector2 circlePos = transform.position;
        circlePos.x += sizeCircleX / 4;
        circlePos.y -= sizeCircleY / 4;
        GameObject circ = Instantiate(GameController.instance.circle, circlePos, Quaternion.identity, GameController.instance.circleHolder);
        circ.transform.GetChild(0).GetComponent<Circle>().InitSetup(sizeListX / 2, sizeListY / 2, newColorList, sizeCircleX / 2, sizeCircleY / 2);

        Vector3 temp = transform.localScale;
        temp.x /= 2;
        temp.y /= 2;
        temp.z /= 2;

        circ.transform.GetChild(0).localScale = temp;
    }
}
