// Configuration
string cockpitName = "Min 3 Cockpit";
string drillsGroupName = "Min 3 Foreuses";
float maxMass = 2000000f;

// Global Properties
IMyShipController cockpit;
List<IMyTerminalBlock> drills = new List<IMyTerminalBlock>();
MyShipMass myShipMass;

private void ApplyActionToBlocks(List<IMyTerminalBlock> blocks, string action) {
    foreach (var block in blocks) {
        block.ApplyAction(action);
    }
}

public Program()
{
    Echo ("Program");

    cockpit = GridTerminalSystem.GetBlockWithName(cockpitName) as IMyShipController;
    GridTerminalSystem.GetBlockGroupWithName(drillsGroupName).GetBlocksOfType<IMyTerminalBlock>(drills);
}

public void Main(string argument, UpdateType updateSource)
{
    Echo ("Main");

    if (cockpit.IsUnderControl) {
        Runtime.UpdateFrequency = UpdateFrequency.Update1;
    }
    else {
        Runtime.UpdateFrequency = UpdateFrequency.None;
    }
    
    myShipMass = cockpit.CalculateShipMass();
    if (myShipMass.TotalMass > maxMass) {
        ApplyActionToBlocks(drills, "OnOff_Off");
    }
}
