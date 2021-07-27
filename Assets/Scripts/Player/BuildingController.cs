using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField]
    private GameObject buildingPoint;
    [SerializeField]
    private List<GameObject> defenses;

    public bool BuildingState
    {
        get
        {
            return buildingPoint.activeSelf;
        }
    }

    private int currentDefense = 0;

    private GameUiManager uiManager;
    private Inventory inventory;

    private void Start()
    {
        desactivateAllDefenses();
        defenses[currentDefense].SetActive(true);

        uiManager = FindObjectOfType<GameUiManager>();
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (!buildingPoint.activeSelf)
            return;

        if (Input.GetButtonUp("Fire2"))
        {
            Building build = defenses[currentDefense].GetComponent<Building>();

            if (build.stoneCost > inventory.stone)
                return;

            if (build.woodCost > inventory.wood)
                return;

            if (build.specialCost > inventory.special)
                return;

            if (build.Build()) {
                inventory.removeResource(Resource.ResourceType.stone, build.stoneCost);
                inventory.removeResource(Resource.ResourceType.wood, build.woodCost);
                inventory.removeResource(Resource.ResourceType.special, build.specialCost);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") == 0f)
            return;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            currentDefense++;
            if (currentDefense >= defenses.Count)
            {
                currentDefense = 0;
            }

            desactivateAllDefenses();
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.SWITCH_OPTION);
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            currentDefense--;
            if (currentDefense < 0)
            {
                currentDefense = defenses.Count - 1;
            }

            desactivateAllDefenses();
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.SWITCH_OPTION);
        }

        defenses[currentDefense].SetActive(true);
        Building buildS = defenses[currentDefense].GetComponent<Building>();
        uiManager.updateCost(buildS.woodCost, buildS.stoneCost, buildS.specialCost);

        //TODO: Hacer dinamico.
        if (currentDefense == 0)
            uiManager.setObstacleBuild();
        if (currentDefense == 1)
            uiManager.setCrossbowBuild();
        if (currentDefense == 2)
            uiManager.setTowerBuild();
    }

    public void toggleBuilding()
    {
        buildingPoint.SetActive(!buildingPoint.activeSelf);
        uiManager.setBuildingUiState(buildingPoint.activeSelf);
    }

    void desactivateAllDefenses()
    {
        foreach (GameObject build in defenses)
        {
            build.SetActive(false);
        }
    }

}
