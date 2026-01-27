using UnityEngine;

public class DrinkManager : MonoBehaviour
{
    public int drinknumber;
    public GameObject glassGo;
    public GameObject coneGo;
    public Transform glassPos;
    public Transform conePos;
    public bool hasGlass = false;
    public bool hasCone = false;

    public OrderManager m_ordermanager;
    public Snap m_snapGlass;
    public Snap m_snapCone1;
    public Snap m_snapCone2;
    public Snap m_snapCone3;



    private void Start()
    {
        m_ordermanager = FindAnyObjectByType<OrderManager>();
        // m_snapGlass = FindAnyObjectByType<Snap>();
        drinknumber = 50;
    }

    public void SpawnGlass(int glass)
    {
        if (glass == 0)
        {
            if (!hasGlass)
            {
                Instantiate(glassGo, glassPos);
                hasGlass = true;
            }
        }
        else if (glass == 1)
        {
            if (!hasCone)
            {
                Instantiate(coneGo, conePos);
                hasCone = true;
            }
        }
    }

    public void GivaAss(int thisDrinky)
    {
        thisDrinky = drinknumber;
        if (thisDrinky == 0)
        {
            m_ordermanager.thisOrder[2] = 0;
            m_ordermanager.currentDrinkT.text = "Drink: Diet Cola / Dr Salt";
            Destroy(m_snapGlass.Glass);
            hasGlass= false;
        }
        if (thisDrinky == 1)
        {
            m_ordermanager.thisOrder[2] = 1;
            m_ordermanager.currentDrinkT.text = "Drink: Orange / Fancy";
            Destroy(m_snapGlass.Glass);
            hasGlass = false;

        }
        if (thisDrinky == 2)
        {
            m_ordermanager.thisOrder[2] = 2;
            m_ordermanager.currentDrinkT.text = "Drink: Jolt / Maintain Dew";
            Destroy(m_snapGlass.Glass);
            hasGlass = false;

        }
        if (thisDrinky == 3)
        {
            m_ordermanager.thisOrder[2] = 3;
            m_ordermanager.currentDrinkT.text = "Drink: Lemon / Nice Tea";
            Destroy(m_snapGlass.Glass);
            hasGlass = false;

        }
        if (thisDrinky == 4)
        {
            m_ordermanager.thisOrder[2] = 4;
            m_ordermanager.currentDrinkT.text = "Drink: Cola";
            Destroy(m_snapGlass.Glass);
            hasGlass = false;

        }
        if (thisDrinky == 5)
        {
            m_ordermanager.thisOrder[2] = 5;
            m_ordermanager.currentDrinkT.text = "Drink: Banana ice cream";
            Destroy(m_snapCone1.Glass);
            Destroy(m_snapCone2.Glass);
            Destroy(m_snapCone3.Glass);
            hasCone = false;

        }
        if (thisDrinky == 6)
        {
            m_ordermanager.thisOrder[2] = 6;
            m_ordermanager.currentDrinkT.text = "Drink: Strawberry ice cream";
            Destroy(m_snapCone1.Glass);
            Destroy(m_snapCone2.Glass);
            Destroy(m_snapCone3.Glass);
            hasCone = false;

        }
        if (thisDrinky == 7)
        {
            m_ordermanager.thisOrder[2] = 7;
            m_ordermanager.currentDrinkT.text = "Drink: Chocolate ice cream";
            Destroy(m_snapCone1.Glass);
            Destroy(m_snapCone2.Glass);
            Destroy(m_snapCone3.Glass);
            hasCone = false;

        }

    }
    public void Trash()
    {
        Destroy(m_snapGlass.Glass);
        hasGlass = false;
    }
    public void Trash2()
    {
        Destroy(m_snapCone1.Glass);
        Destroy(m_snapCone2.Glass);
        Destroy(m_snapCone3.Glass);
        hasCone = false;
    }

}
