using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BungieSharper.Entities.Destiny.Definitions;
using DestinyCraft.EventModels;

namespace DestinyCraft.ViewModels
{
    public interface IIndexViewModel
    {
        bool Loading { get; set; }
        List<string> ArtifactMods { get; set; }
        Dictionary<string, DestinyInventoryItemDefinition> Mods { get; set; }
        Dictionary<string, DestinySandboxPerkDefinition> PerkData { get; set; }
        void OnPropertyChanged(string propertyName);

        void _Loaded(object sender, LoadedArgs e);
        // void OnInitialization();
        event PropertyChangedEventHandler? PropertyChanged;
    }
}