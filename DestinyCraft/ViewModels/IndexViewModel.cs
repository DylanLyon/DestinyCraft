using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using BungieSharper.Entities.Destiny.Definitions;
// using DestinyCraft.Annotations;
using DestinyCraft.Controllers;
using DestinyCraft.EventModels;



namespace DestinyCraft.ViewModels
{
    public class IndexViewModel : IIndexViewModel, INotifyPropertyChanged
    {
        // private ApiDataFactory _factory = new ();
        private List<String> _elementDisplayOrderHashes = new (new [] { "1198124803", "591714140", "1819698290", "4069572561", "728351493"});

        public event PropertyChangedEventHandler? PropertyChanged;
        
        public Dictionary<string, DestinySandboxPerkDefinition> PerkData { get; set; }

        // [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public List<string> ArtifactMods { get; set; }
        
        private Dictionary<string, DestinyInventoryItemDefinition> _mods = new ();
        public Dictionary<string, DestinyInventoryItemDefinition> Mods
        {
            get => _mods; 
            set
            {
                _mods = value;
                OnPropertyChanged();
            }
        }
        private bool _loading =  true;
        public bool Loading
        {
            get => _loading; 
            set
            {
                _loading = value; OnPropertyChanged();
            }
        }

        // private void _Loaded(object sender, LoadedArgs e)
        public void _Loaded(object sender, LoadedArgs e)
        {
            _loading = false;
            // _mods = e.Mods;
            var list = e.Mods.ToList();
            PerkData = e.SandboxData.PerkData;
            ArtifactMods = e.SandboxData.SeasonalMods;

            var tempList = new List<KeyValuePair<string, DestinyInventoryItemDefinition>>();
            foreach (var item in list.ToList())
            {
                if (!ArtifactMods.Contains(item.Value.Hash.ToString())) continue;
                tempList.Add(item);
                list.Remove(item);
            }
            
            
            list.Sort((mod1, mod2) =>
                    _elementDisplayOrderHashes.IndexOf(mod1.Value.Plug.EnergyCost.EnergyTypeHash.ToString()).CompareTo(_elementDisplayOrderHashes.IndexOf(mod2.Value.Plug.EnergyCost.EnergyTypeHash.ToString()))
                // mod1.Value.Plug.EnergyCost.EnergyTypeHash.CompareTo(mod2.Value.Plug.EnergyCost.EnergyTypeHash)
                );

            foreach (var seasonalMod in tempList)
            {
                list.Add(seasonalMod);
            }
            
            
            // list.Sort((mod1, mod2) => ArtifactMods.Contains(mod1.Value.Hash.ToString()).CompareTo(ArtifactMods.Contains(mod2.Value.Hash.ToString())));
            _mods = list.ToDictionary(mod => mod.Key, mod => mod.Value);

            
            
            Console.WriteLine("Loaded! {0}", _mods.Count);
            OnPropertyChanged();
        }
        
        // public void OnInitialization()
        // {
        //     _factory.Loaded += _Loaded;
        //     _factory.OnInitialization();
        // }
    }
}