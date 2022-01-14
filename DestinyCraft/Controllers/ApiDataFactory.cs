using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using BungieSharper.Client;
using BungieSharper.Entities;
using BungieSharper.Entities.Destiny.Definitions;
using BungieSharper.Entities.Destiny.Definitions.Artifacts;
using DestinyCraft.EventModels;
using DestinyCraft.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DestinyCraft.Controllers
{
    public class ApiDataFactory
    {
        private BungieClientConfig _apiConfig;
        private BungieApiClient _client;
        
        public event EventHandler<LoadedArgs> Loaded;
        
        public ApiDataFactory()
        {
            Console.WriteLine("Factory Created");
            _apiConfig = new BungieClientConfig();
            _apiConfig.ApiKey = "02920a8d5c614cc992a23c348e07bf9e";
            _apiConfig.OAuthClientId = 38045;
            _client = new BungieApiClient(_apiConfig);
        }

        public async void GetManifest()
        {
            var result = await _client.Api.Destiny2_GetDestinyManifest();

            var modPath = result.JsonWorldComponentContentPaths["en"]["DestinyInventoryItemDefinition"];
            var sandboxPath = result.JsonWorldComponentContentPaths["en"]["DestinySandboxPerkDefinition"];
            var artifactPath = result.JsonWorldComponentContentPaths["en"]["DestinyArtifactDefinition"];
            string modString = (new WebClient()).DownloadString("https://www.bungie.net" + modPath);
            string sandboxString = (new WebClient()).DownloadString("https://www.bungie.net" + sandboxPath);
            string artifactString = (new WebClient()).DownloadString("https://www.bungie.net" + artifactPath);
            
            
            
            var modData = JsonConvert.DeserializeObject<Dictionary<string, DestinyInventoryItemDefinition>>(modString);
            var sandboxData = JsonConvert.DeserializeObject<Dictionary<string, DestinySandboxPerkDefinition>>(sandboxString);
            var artifactData = JsonConvert.DeserializeObject<Dictionary<string, DestinyArtifactDefinition>>(artifactString);
            Console.WriteLine("Count: {0}", modData.Count);

            var seasonalMods = new List<string>();
            foreach (var destinyArtifactDefinition in artifactData)
            {
                foreach (var destinyArtifactTierDefinition in destinyArtifactDefinition.Value.Tiers)
                {
                    foreach (var destinyArtifactTierItemDefinition in destinyArtifactTierDefinition.Items)
                    {
                        seasonalMods.Add(destinyArtifactTierItemDefinition.ItemHash.ToString());
                    }
                }
            }
            
            Dictionary<string, DestinyInventoryItemDefinition> Mods = new ();
            
            foreach (var (key, destinyInventoryItemDefinition) in modData)
            {
                if (!modData.TryGetValue(key, out var item)) continue;
                try
                {
                    IEnumerable<uint>? categoryHashes = item.ItemCategoryHashes;
                    if (
                        categoryHashes.Contains(4104513227) &&
                        !item.Plug.EnergyCost.Equals(0) &&
                        !item.DisplayProperties.Description.Contains("deprecated") &&
                        !item.DisplayProperties.Name.Contains("Scavenger") &&
                        !item.DisplayProperties.Name.Contains("Unflinching") &&
                        !item.DisplayProperties.Name.Contains("Targeting") &&
                        !item.DisplayProperties.Name.Contains("Loader") &&
                        !item.DisplayProperties.Name.Contains("Reloader") &&
                        !item.DisplayProperties.Name.Contains("Finder") &&
                        !item.DisplayProperties.Name.Contains("Holster") &&
                        (!item.DisplayProperties.Name.Contains("Reserves") || item.DisplayProperties.Name == "Extra Reserves") &&
                        !item.DisplayProperties.Name.Contains("Dexterity") &&
                        !item.DisplayProperties.Name.Contains("Strength") &&
                        !item.DisplayProperties.Name.Contains("Discipline") &&
                        !item.DisplayProperties.Name.Contains("Intellect") &&
                        !item.DisplayProperties.Name.Contains("Mobility") &&
                        !item.DisplayProperties.Name.Contains("Resilience") &&
                        !item.DisplayProperties.Name.Contains("Recovery") &&
                        !item.DisplayProperties.Name.Contains("Empty Mod Socket") &&
                        !item.ItemTypeDisplayName.Contains("Last Wish Raid Mod")  &&
                        !item.ItemTypeDisplayName.Contains("Vault of Glass Armor Mod")  &&
                        !item.ItemTypeDisplayName.Contains("Deep Stone Crypt Raid Mod")  &&
                        !item.ItemTypeDisplayName.Contains("Garden of Salvation Raid Mod")  &&
                        !item.ItemTypeDisplayName.Contains("Nightmare Mod")
                        )
                    {
                        Mods.Add(key, destinyInventoryItemDefinition);
                    }
            
                }
                catch (Exception e)
                {
                    // Console.WriteLine("Faulty Key: {0}", key);
                }
            }
            
            foreach (var mod in Mods)
            {
                Console.WriteLine("Mod: {0} {1}", mod.Value.DisplayProperties.Name, mod.Value.Hash);
            }
            Console.WriteLine("Filter Count: {0}", Mods.Count);

            SandboxData returnData = new SandboxData(sandboxData, seasonalMods);
            
            
            Loaded?.Invoke(this, new LoadedArgs(Mods, returnData));
            // Loaded?.Invoke(this, EventArgs.Empty);
        }



        public void OnInitialization()
        {
            GetManifest();
        }
    }
}