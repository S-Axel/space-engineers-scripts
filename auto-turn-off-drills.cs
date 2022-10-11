IMyShipController cockpit;
List<IMyTerminalBlock> drills = new List<IMyTerminalBlock>();
MyShipMass myShipMass;

public Program()
{
    // Le constructeur, appelé une seule fois par session et
    //  toujours avant qu'une autre méthode soit appelée. Utilisez-le pour
    //  initialiser votre script.
    //    
    //  Le constructeur est facultatif et peut être supprimé s'il n'est pas 
    // nécessaire.
    // 
    //  Il est recommandé de définir RuntimeInfo.UpdateFrequency
    //  ici, ce qui permettra à votre script de s'exécuter sans
    //  bloc temporisateur.


    Echo ("Program");


    cockpit = GridTerminalSystem.GetBlockWithName("Min 3 Cockpit") as IMyShipController;
    GridTerminalSystem.GetBlockGroupWithName("Min 3 Foreuses").GetBlocksOfType<IMyTerminalBlock>(drills);
}

public void Save()
{
    // Appelé lorsque le programme doit sauvegarder son état. Utilisez 
    //  cette méthode pour enregistrer votre état dans le champ Stockage
    //  ou par un autre moyen.
    // 
    //  Cette méthode est facultative et peut être supprimée si elle n'est pas
    //  nécessaire.

    Echo ("Save");
}

public void Main(string argument, UpdateType updateSource)
{
    // Le point d’entrée principal du script, appelé chaque fois
    //  qu’une des actions Exécuter du bloc programmable est appelée,
    //  ou lorsque le script se met à jour. L'argument updateSource
    //  décrit l'origine de la mise à jour. 
    // 
    // La méthode elle-même est obligatoire, mais les arguments ci-dessus
    //  peuvent être supprimés s'ils ne sont pas nécessaires.


    Echo ("Main");


    if (cockpit.IsUnderControl)
    {
        Runtime.UpdateFrequency = UpdateFrequency.Update1;
    }
    else
    {
        Runtime.UpdateFrequency = UpdateFrequency.None;
    }
    
    myShipMass = cockpit.CalculateShipMass();
    if (myShipMass.TotalMass > 2000000)
    {
        foreach (var drill in drills)
        {
            drill.ApplyAction("OnOff_Off");
        }
    }
    
}
