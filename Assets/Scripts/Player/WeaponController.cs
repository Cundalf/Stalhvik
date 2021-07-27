using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public ToolType initialTool;
    public enum ToolType
    {
        sword,
        axe,
        pickaxe,
        hammer
    }

    private BuildingController buildingController;
    public List<Tool> tools;

    private Tool currentTool;
    private bool attackIdle;
    private GameUiManager uiManager;

    public bool isAttacking
    {
        get
        {
            return !attackIdle;
        }
    }

    private void Start()
    {
        attackIdle = true;
        buildingController = gameObject.GetComponent<BuildingController>();

        uiManager = FindObjectOfType<GameUiManager>();
        activateTool(initialTool);
        uiManager.updateToolIcon(initialTool);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            desactivateTools();
            activateTool(ToolType.sword);
            uiManager.updateToolIcon(ToolType.sword);

            if (buildingController.BuildingState)
                buildingController.toggleBuilding();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            desactivateTools();
            activateTool(ToolType.axe);
            uiManager.updateToolIcon(ToolType.axe);

            if (buildingController.BuildingState)
                buildingController.toggleBuilding();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            desactivateTools();
            activateTool(ToolType.pickaxe);
            uiManager.updateToolIcon(ToolType.pickaxe);

            if (buildingController.BuildingState)
                buildingController.toggleBuilding();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            desactivateTools();
            activateTool(ToolType.hammer);
            uiManager.updateToolIcon(ToolType.hammer);
        }

        if (Input.GetKeyDown(KeyCode.E) && currentTool != null)
        {
            if(currentTool.type == ToolType.hammer)
            {
                buildingController.toggleBuilding();
            }
        }
    }

    public void attack()
    {
        if (currentTool == null)
            return;

        if(attackIdle)
        {
            currentTool.canDamage = true;
            attackIdle = false;
        }
    }

    public void attackAnimFinished()
    {
        attackIdle = true;
    }

    private void desactivateTools()
    {
        foreach (Tool tool in tools)
        {
            tool.gameObject.SetActive(false);
        }

        currentTool = null;
    }

    private void activateTool(ToolType type)
    {
        foreach (Tool tool in tools)
        {
            if(tool.type == type)
            {
                tool.gameObject.SetActive(true);
                currentTool = tool;
                break;
            }
        }
    }
}
