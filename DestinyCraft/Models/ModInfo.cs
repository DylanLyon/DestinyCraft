using System.Collections.Generic;
using BungieSharper.Entities.Destiny.Definitions;

namespace DestinyCraft.Models;

public class ModInfo
{
    public List<string> PerkData { get; set; }
    // public List<DestinySandboxPerkDefinition> PerkData { get; set; }
    public DestinyInventoryItemDefinition ModData { get; set; }

    public ModInfo(DestinyInventoryItemDefinition modData, List<string> perkData)
    // public ModInfo(DestinyInventoryItemDefinition modData, List<DestinySandboxPerkDefinition> perkData)
    {
        PerkData = perkData;
        ModData = modData;
    }
}