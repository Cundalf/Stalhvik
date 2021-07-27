using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxStone;
    public int maxWood;
    public int maxSpecial;

    private int _stone;
    private int _wood;
    private int _special;

    public int MaxStone
    {
        get
        {
            return maxStone;
        }
    }

    public int MaxWood
    {
        get
        {
            return maxWood;
        }
    }
    public int MaxSpecial
    {
        get
        {
            return maxSpecial;
        }
    }

    public int stone
    {
        get
        {
            return _stone;
        }
        private set
        {
            if (_stone <= maxStone)
            {
                _stone = Mathf.Clamp(value, 0, maxStone);
            }
        }
    }

    public int wood
    {
        get
        {
            return _wood;
        }
        private set
        {
            if (_wood <= maxWood)
            {
                _wood = Mathf.Clamp(value, 0, maxWood);
            }
        }
    }

    public int special
    {
        get
        {
            return _special;
        }
        private set
        {
            if (_special <= maxSpecial)
            {
                _special = Mathf.Clamp(value, 0, maxSpecial);
            }
        }
    }

    private GameUiManager uiManager;
    private void Start()
    {
        uiManager = FindObjectOfType<GameUiManager>();
    }

    public void addResource(Resource.ResourceType type, int amount = 1)
    {
        if (amount < 0)
            return;

        switch(type)
        {
            case Resource.ResourceType.stone:
                stone += amount;
                uiManager.updateResource(type, stone);
                break;
            case Resource.ResourceType.wood:
                wood += amount;
                uiManager.updateResource(type, wood);
                break;
            case Resource.ResourceType.special:
                special += amount;
                uiManager.updateResource(type, special);
                break;
        }

    }

    public void removeResource(Resource.ResourceType type, int amount = 1)
    {
        switch (type)
        {
            case Resource.ResourceType.stone:
                stone -= amount;
                uiManager.updateResource(type, stone);
                break;
            case Resource.ResourceType.wood:
                wood -= amount;
                uiManager.updateResource(type, wood);
                break;
            case Resource.ResourceType.special:
                special -= amount;
                uiManager.updateResource(type, special);
                break;
        }
    }
}
