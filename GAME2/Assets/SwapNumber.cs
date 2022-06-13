using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapNumber : MonoBehaviour
{
    public bool checkSwap = false;
    public void Swap(PlayerController player, ManagerGame managerGame)
    {

        player.touch.Touch();
        if (player.touch.hitInformation.collider != null)
        {
            for (int i = 0; i < ManagerNumber.instance.list.Count; i++)
            {
                if (Vector2.Distance(ManagerNumber.instance.list[i].transform.position, player.touch.hitInformation.transform.position) <= 0.5f)
                {
                    if (managerGame.listSwap.Count == 0)
                    {
                        managerGame.listSwap.Add(ManagerNumber.instance.list[i]);
                    }
                    if (managerGame.listSwap.Count != 0)
                    {
                        if (ManagerNumber.instance.list[i] != managerGame.listSwap[managerGame.listSwap.Count - 1])
                        {
                            managerGame.listSwap.Add(ManagerNumber.instance.list[i]);
                        }
                    }
                    break;
                }
            }          
            if (managerGame.listSwap.Count == 2)
            {
                
                var a = Instantiate(ManagerNumber.instance.listNumber[1]);
                managerGame.temp = a.GetComponent<Number>();
                managerGame.temp.spriteRender.sprite = managerGame.listSwap[0].spriteRender.sprite;
                managerGame.temp.id = managerGame.listSwap[0].id;
                managerGame.temp.ma = managerGame.listSwap[0].ma;
                managerGame.temp.transform.name = managerGame.listSwap[0].transform.name;
                managerGame.listSwap[0].spriteRender.sprite = managerGame.listSwap[1].spriteRender.sprite;
                managerGame.listSwap[0].id = managerGame.listSwap[1].id;
                managerGame.listSwap[0].ma = managerGame.listSwap[1].ma;
                managerGame.listSwap[0].transform.name = managerGame.listSwap[1].transform.name;
                managerGame.listSwap[1].spriteRender.sprite = managerGame.temp.spriteRender.sprite;
                managerGame.listSwap[1].id = managerGame.temp.id;
                managerGame.listSwap[1].ma = managerGame.temp.ma;
                managerGame.listSwap[1].transform.name = managerGame.temp.transform.name;
                Destroy(managerGame.temp);
                Destroy(a);
                managerGame.CheckAferDestroy(managerGame.listSwap,player);
                checkSwap = true;
                managerGame.listSwap.Clear();
            }
            else
            {
                checkSwap = false;
            }

        }
    }
}
