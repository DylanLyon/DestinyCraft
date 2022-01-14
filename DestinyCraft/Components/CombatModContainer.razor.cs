using BungieSharper.Entities.Destiny.Definitions;
using DestinyCraft.Utilities;
using Microsoft.AspNetCore.Components;

namespace DestinyCraft.Components
{
    public partial class CombatModContainer
    {

        public string Element { get; set; }
        [Parameter] public DestinyInventoryItemDefinition Mod { get; set; }
        // [Parameter] public EventCallback<DestinyInventoryItemDefinition> HandleModSelect { get; set; }

        protected override void OnInitialized()
        {
            GetElementFromHash getElement = new();
            Element = getElement.GetElementFromString(Mod.Plug.EnergyCost.EnergyTypeHash.ToString());
            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            GetElementFromHash getElement = new();
            Element = getElement.GetElementFromString(Mod.Plug.EnergyCost.EnergyTypeHash.ToString());
            StateHasChanged();
        }

    }
}