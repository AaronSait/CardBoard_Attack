using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicFacilitator : MonoBehaviour
{
    void Start()
    {
       
    }

    //work out what number of elements should be required and new enemy timer
    public static void generateEnemy(int foeNo, Enemy enemy)
    {
        GameSupport.ElementType[] types;
        types = enemy.getRequiredTypes();
        if (foeNo < 2)
        {
            types[0] = getRandomElement();
            enemy.resetMaxTimer(30f);

            if (foeNo == 0)
                enemy.setEnemyID(0);
            else
                enemy.setRandomEnemyID();
        }
        else if (foeNo < 4)
        {
            setupList(types, 2);
            enemy.resetMaxTimer(20f);
            enemy.setRandomEnemyID();
        }
        else
        {
            setupList(types, Random.Range(1, 5));
            float t = 10f - ((foeNo - 4f) / 2f);
            enemy.resetMaxTimer((t > 3f)? t : 3f);
            enemy.setRandomEnemyID();
        }
    }

    //set number of elements required and populate list
    private static void setupList(GameSupport.ElementType[] types, int maxLength)
    {
        for (int i = 0; i < maxLength; i++)
        {
            types[i] = getRandomElement();
        }
    }

    //get random element
    private static GameSupport.ElementType getRandomElement()
    {
        //not including enum element 0 as that is NULL
        return (GameSupport.ElementType)Random.Range(1, System.Enum.GetValues(typeof(GameSupport.ElementType)).Length);
    }

    //score calculations
    public static int calculateScore(int chainNo)
    {
        return chainNo * 10;
    }

    public static float progressToPercent(float val, float maxProgress)
    {
        return (1f / maxProgress) * val;
    }

    public static Vector3 interpolateLocation(float progress, Vector3 startPoint, Vector3 endPoint)
    {
        return startPoint + ((endPoint - startPoint) * progress);
    }

    public static float interpolateLocation(float progress, float startPoint, float endPoint)
    {
        return startPoint + ((endPoint - startPoint) * progress);
    }

    public static string convertIntToFont(int num)//to convert numbers to string that can be used with a font
    {
        char[] tempNum = num.ToString().ToCharArray();
        for (int i = 0; i < tempNum.Length; i++)
        {
            switch (tempNum[i])
            {
                case '0':
                    tempNum[i] = '9';
                    break;
                case '1':
                    tempNum[i] = '0';
                    break;
                case '2':
                    tempNum[i] = '1';
                    break;
                case '3':
                    tempNum[i] = '2';
                    break;
                case '4':
                    tempNum[i] = '3';
                    break;
                case '5':
                    tempNum[i] = '4';
                    break;
                case '6':
                    tempNum[i] = '5';
                    break;
                case '7':
                    tempNum[i] = '6';
                    break;
                case '8':
                    tempNum[i] = '7';
                    break;
                default://for 9
                    tempNum[i] = '8';
                    break;
            }
        }
        return new string(tempNum);
    }
}
