using System.Collections.Generic;
using BungieSharper.Entities.Destiny.Definitions;

namespace DestinyCraft.Models;

public class SandboxData
{
    public Dictionary<string, DestinySandboxPerkDefinition> PerkData { get; set; }
    public List<string> SeasonalMods { get; set; }

    public SandboxData(Dictionary<string, DestinySandboxPerkDefinition> sandbox, List<string> seasonalMods)
    {
        PerkData = sandbox;
        SeasonalMods = seasonalMods;
    }
}