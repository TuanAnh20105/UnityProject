using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    // Start is called before the first frame update
    string Dijkstra(int[,] A, int n, int src, int des)
    {

        int[] danhDau = new int[n + 1];
        int[] nhan = new int[n + 1];
        int[] truoc = new int[n + 1];
        int XP, min, i;
        for (i = 1; i <= n; i++)
        {
            nhan[i] = 10000;
            danhDau[i] = 0;
            truoc[i] = src;
        }
        nhan[src] = 0;
        danhDau[src] = 1;
        XP = src;
        while (XP != des)
        {

            for (int j = 1; j <= n; j++)
            {
                if (A[XP, j] > 0 && nhan[j] > A[XP, j] + nhan[XP] && danhDau[j] == 0)
                {
                    nhan[j] = A[XP, j] + nhan[XP];
                    truoc[j] = XP;
                }
            }
            min = 10000;
            for (int j = 1; j <= n; j++)
            {
                if (min > nhan[j] && danhDau[j] == 0)
                {
                    min = nhan[j];
                    XP = j;
                }
            }
            danhDau[XP] = 1;
        }

        string[] temp = new string[n + 1];
        temp[0] = des.ToString();
        temp[1] = truoc[des].ToString();
        i = truoc[des];
        int count = 2;
        while (i != src)
        {
            i = truoc[i];
            temp[count] = i.ToString();
            count++;
        }
        string res = "";
        for (i = count - 1; i >= 1; i--)
        {
            res += temp[i] + "--->";

        }
        res += temp[0] + ": " + nhan[des];
        return res;

    }
void Start()
{
    int [,] a = new int [16,9];

    Dijkstra(a,8,3,2);
}

// Update is called once per frame
void Update()
{

}
}
