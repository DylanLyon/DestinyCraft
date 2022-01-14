using System.Collections.Generic;
using System.Text.Json;
using BungieSharper.Entities.Destiny.Definitions;
using DestinyCraft.Models;
using DestinyCraft.Utilities;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace DestinyCraft.Components
{
    public partial class ArmorMod
    {
        public string Element { get; set; }
        [Parameter] public string View { get; set; } = "compact";
        [Parameter] public List<string> ActiveTags { get; set; }
        [Parameter] public DestinyInventoryItemDefinition Mod { get; set; }
        [Parameter] public EventCallback<(DestinyInventoryItemDefinition, string)> HandleModSelect { get; set; }
        [Parameter] public EventCallback<DestinyInventoryItemDefinition> OnHover { get; set; }
        private List<string> ModTags { get; set; }
        [Parameter] public List<string> ArtifactMods { get; set; }
        [Parameter] public ModDataModel ModDataModel { get; set; }

        protected override void OnInitialized()
        {
            GetElementFromHash getElement = new ();
            Element = getElement.GetElementFromString(Mod.Plug.EnergyCost.EnergyTypeHash.ToString());

            base.OnInitialized();
        }
    }
}