using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUiManager : MonoBehaviour
{
    [Header("Objects")]
    public GameObject heartsContainer;
    public GameObject buildingContainer;
    public GameObject pauseButton;
    public GameObject pauseFrame;
    public Text woodText;
    public Text stoneText;
    public Text specialText;
    public Image toolIcon;
    public Image buildIcon;
    public Text woodCostText;
    public Text stoneCostText;
    public Text specialCostText;

    [Header("Resources")]
    public GameObject heartPrefab;
    public Sprite axeIcon;
    public Sprite swordIcon;
    public Sprite pickaxeIcon;
    public Sprite mazeIcon;
    public Sprite obstacleIcon;
    public Sprite towerIcon;
    public Sprite crossbowIcon;

    private List<GameObject> hearts = new List<GameObject>();
    private string maxStone;
    private string maxWood;
    private string maxSpecial;

    private void Start()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        HealthManager hm = player.GetComponent<HealthManager>();
        Inventory inv = player.GetComponent<Inventory>();

        for (int i = 1; i <= hm.MaxHealth; i++)
        {
            hearts.Add(Instantiate(heartPrefab, heartsContainer.transform));
        }

        maxStone = inv.MaxStone.ToString();
        updateResource(Resource.ResourceType.stone, inv.stone);
        maxWood = inv.MaxWood.ToString();
        updateResource(Resource.ResourceType.wood, inv.wood);
        maxSpecial = inv.MaxSpecial.ToString();
        updateResource(Resource.ResourceType.special, inv.special);

        //TODO: Hacer dinamico
        setObstacleBuild();
        woodCostText.gameObject.SetActive(false);
        stoneCostText.gameObject.SetActive(false);
        specialCostText.gameObject.SetActive(false);
    }

    public void updateHealth(int health)
    {
        for (int i = 0; i <= hearts.Count - 1; i++)
        {
            hearts[i].SetActive(i < health);
        }
    }

    public void updateToolIcon(WeaponController.ToolType type)
    {
        switch (type)
        {
            case WeaponController.ToolType.axe:
                toolIcon.sprite = axeIcon;
                break;
            case WeaponController.ToolType.hammer:
                toolIcon.sprite = mazeIcon;
                break;
            case WeaponController.ToolType.sword:
                toolIcon.sprite = swordIcon;
                break;
            case WeaponController.ToolType.pickaxe:
                toolIcon.sprite = pickaxeIcon;
                break;
        }
    }

    public void updateResource(Resource.ResourceType type, int amount)
    {
        switch (type)
        {
            case Resource.ResourceType.special:
                specialText.text = amount.ToString() + "/" + maxSpecial;
                break;
            case Resource.ResourceType.wood:
                woodText.text = amount.ToString() + "/" + maxWood;
                break;
            case Resource.ResourceType.stone:
                stoneText.text = amount.ToString() + "/" + maxStone;
                break;
        }
    }

    public void setCrossbowBuild()
    {
        buildIcon.sprite = crossbowIcon;
    }

    public void setObstacleBuild()
    {
        buildIcon.sprite = obstacleIcon;
    }

    public void setTowerBuild()
    {
        buildIcon.sprite = towerIcon;
    }

    public void setBuildingUiState(bool state)
    {
        buildingContainer.SetActive(state);
        woodCostText.gameObject.SetActive(state);
        stoneCostText.gameObject.SetActive(state);
        specialCostText.gameObject.SetActive(state);
    }

    public void updateCost(int woodCost, int stoneCost, int specialCost)
    {
        woodCostText.text = woodCost.ToString();
        stoneCostText.text = stoneCost.ToString();
        specialCostText.text = specialCost.ToString();
    }

    public void pause()
    {
        GameManager.SharedInstance.pauseGame();
        pauseButton.SetActive(false);
        pauseFrame.SetActive(true);
    }

    public void goToMainMenu()
    {
        GameManager.SharedInstance.resumeGame();
        GameManager.SharedInstance.goToMainMenu();
    }
    public void resume()
    {
        GameManager.SharedInstance.resumeGame();
        pauseButton.SetActive(true);
        pauseFrame.SetActive(false);
    }

}
