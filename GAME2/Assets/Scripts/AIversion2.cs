using System.Collections.Generic;

namespace DefaultNamespace
{
    public class AIversion2
    {
        
        public List<Number> listNumberClone = new List<Number>();
        
        public void CloneList(ManagerNumber managerNumber)
        {
            listNumberClone.Clear();
            for (int i = 0; i < managerNumber.list.Count; i++)
            {
                listNumberClone.Add(managerNumber.list[i]);
            }
            Test();
            
            
        }

        public void Test()
        {
            listNumberClone[0].transform.name = "haha";
            listNumberClone[0].SetTxtNumber("191923");
        }
        
    }
}