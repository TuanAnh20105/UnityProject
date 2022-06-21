using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapNumber : MonoBehaviour
{
    public bool checkSwap = false;
    public void Swap(PlayerController player, ManagerNumber managerNumber)
    {

        player.touch.Touch();
        if(Input.GetMouseButtonUp(0))
        {

        if (player.touch.hitInformation.collider != null)
        {
            for (int i = 0; i < ManagerNumber.instance.list.Count; i++)
            {
                if (Vector2.Distance(ManagerNumber.instance.list[i].transform.position, player.touch.hitInformation.transform.position) <= 0.5f)
                {
                    if (managerNumber.listSwap.Count == 0)
                    {
                        managerNumber.listSwap.Add(ManagerNumber.instance.list[i]);
                    }
                    if (managerNumber.listSwap.Count != 0)
                    {
                        if (ManagerNumber.instance.list[i] != managerNumber.listSwap[managerNumber.listSwap.Count - 1])
                        {
                            managerNumber.listSwap.Add(ManagerNumber.instance.list[i]);
                        }
                    }
                    break;
                }
            }
            if (managerNumber.listSwap.Count == 2)
            {
                    SwapTwoNumber(managerNumber, player);
            }
            else
            {
                checkSwap = false;
            }

        }
        }
    }
    public void SwapTwoNumber(ManagerNumber managerNumber,PlayerController player)
    {
        var a = Instantiate(ManagerNumber.instance.numberSample);
        managerNumber.temp = a.GetComponent<Number>();
        managerNumber.temp.spriteRenderer.sprite = managerNumber.listSwap[0].spriteRenderer.sprite;
        managerNumber.temp.id = managerNumber.listSwap[0].id;
        managerNumber.temp.ma = managerNumber.listSwap[0].ma;
        managerNumber.temp.txtNumber.text = managerNumber.listSwap[0].txtNumber.text;
        managerNumber.temp.transform.name = managerNumber.listSwap[0].transform.name;
        managerNumber.listSwap[0].spriteRenderer.sprite = managerNumber.listSwap[1].spriteRenderer.sprite;
        managerNumber.listSwap[0].id = managerNumber.listSwap[1].id;
        managerNumber.listSwap[0].ma = managerNumber.listSwap[1].ma;
        managerNumber.listSwap[0].transform.name = managerNumber.listSwap[1].transform.name;
        managerNumber.listSwap[0].txtNumber.text = managerNumber.listSwap[1].txtNumber.text;
        managerNumber.listSwap[1].spriteRenderer.sprite = managerNumber.temp.spriteRenderer.sprite;
        managerNumber.listSwap[1].id = managerNumber.temp.id;
        managerNumber.listSwap[1].ma = managerNumber.temp.ma;
        managerNumber.listSwap[1].transform.name = managerNumber.temp.transform.name;
        managerNumber.listSwap[1].txtNumber.text = managerNumber.temp.txtNumber.text;
        Destroy(managerNumber.temp);
        Destroy(a);
        managerNumber.CheckAferDestroy(managerNumber.listSwap, player);
        checkSwap = true;
        managerNumber.listSwap.Clear();
    }
}
