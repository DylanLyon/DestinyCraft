using System.Collections.Generic;
using BungieSharper.Entities.Destiny.Definitions;
using DestinyCraft.Models;

namespace DestinyCraft.EventModels
{
    public class LoadedArgs
    {
        public LoadedArgs(Dictionary<string, DestinyInventoryItemDefinition> mods, SandboxData sandboxData)
        {
            Mods = mods;
            SandboxData = sandboxData;
        }
        public Dictionary<string, DestinyInventoryItemDefinition> Mods { get; set; }
        public SandboxData SandboxData { get; set; }
    }
}