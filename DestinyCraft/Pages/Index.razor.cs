// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Net;
// using System.Threading.Tasks;
// using BungieSharper.Client;
// using BungieSharper.Entities.Destiny.Definitions;
// using BungieSharper.Entities.Destiny.Definitions.Artifacts;
// using DestinyCraft.EventModels;
// using DestinyCraft.Models;
// using DestinyCraft.Utilities;
// using Newtonsoft.Json;
//
// namespace DestinyCraft.Pages
// {
//     public partial class Index
//     {
//         public List<DestinyInventoryItemDefinition> SelectedCombatMods { get; set; } = new ();
//         public List<DestinyInventoryItemDefinition> SelectedHelmetMods { get; set; } = new ();
//         public List<DestinyInventoryItemDefinition> SelectedArmMods { get; set; } = new ();
//         public List<DestinyInventoryItemDefinition> SelectedChestMods { get; set; } = new ();
//         public List<DestinyInventoryItemDefinition> SelectedLegMods { get; set; } = new ();
//         public List<DestinyInventoryItemDefinition> SelectedClassMods { get; set; } = new ();
//         public string View { get; set; } = "compact";
//         public List<string> CoreTypes = new List<string>();
//         public List<string> ActiveTags = new List<string>();
//         private string[] _elementDisplayOrderHashes = { "1198124803", "591714140", "1819698290", "4069572561", "728351493"};
//         public Dictionary<string, ModDataModel>? ModDataModels = new Dictionary<string, ModDataModel>();
//         
//         
//         
//         private BungieClientConfig _apiConfig;
//
//         
//         
//
//         public ModInfo? HoveredMod { get; set; } = null;
//
//         public void OnHandleHover(DestinyInventoryItemDefinition mod)
//         {
//             Console.WriteLine("Mod: {0}", mod.DisplayProperties.Name);
//             var perks = new List<string>();
//             foreach (var perk in mod.Perks)
//             {
//                 var info = _viewModel.PerkData[perk.PerkHash.ToString()];
//                 if (info.DisplayProperties.Description is not null && !perks.Contains(info.DisplayProperties.Description))
//                 {
//                     perks.Add(info.DisplayProperties.Description);
//                     // Console.WriteLine("Perk: {0}", info);
//                 }
//             }
//             
//             HoveredMod = new ModInfo(mod, perks);
//             StateHasChanged();
//         }
//
//         public void HandleModSelect(DestinyInventoryItemDefinition? mod, string type)
//         {
//             GetElementFromHash getElement = new();
//             if (type == "primary")
//             {
//                 if (SelectedCombatMods.Count < 5)
//                 {
//                     SelectedCombatMods.Add(mod);
//                     CoreTypes.Add(getElement.GetElementFromString(mod.Plug.EnergyCost.EnergyTypeHash.ToString()));
//                 }
//                 else
//                 {
//                     SelectedCombatMods.RemoveAt(4);
//                     SelectedCombatMods.Insert(0, mod);
//                     CoreTypes.RemoveAt(4);
//                     CoreTypes.Insert(0, getElement.GetElementFromString(mod.Plug.EnergyCost.EnergyTypeHash.ToString()));
//                 }
//             }
//             else if (type == "secondary")
//             {
//
//                 if (mod.ItemCategoryHashes.Contains<uint>(1362265421)) // Helm
//                 {
//                     if (SelectedHelmetMods.Count < 2)
//                         SelectedHelmetMods.Add(mod);
//                     else
//                     {
//                         SelectedHelmetMods.RemoveAt(1);
//                         SelectedHelmetMods.Insert(0, mod);
//                     }
//                 }
//                 else if (mod.ItemCategoryHashes.Contains(3872696960)) // Arms
//                 {
//                     if (SelectedArmMods.Count < 2)
//                         SelectedArmMods.Add(mod);
//                     else
//                     {
//                         SelectedArmMods.RemoveAt(1);
//                         SelectedArmMods.Insert(0, mod);
//                     }
//                 }
//                 else if (mod.ItemCategoryHashes.Contains(3723676689)) // Chest
//                 {
//                     if (SelectedChestMods.Count < 2)
//                         SelectedChestMods.Add(mod);
//                     else
//                     {
//                         SelectedChestMods.RemoveAt(1);
//                         SelectedChestMods.Insert(0, mod);
//                     }
//                 }
//                 else if (mod.ItemCategoryHashes.Contains(3607371986)) // Legs
//                 {
//                     if (SelectedLegMods.Count < 2)
//                         SelectedLegMods.Add(mod);
//                     else
//                     {
//                         SelectedLegMods.RemoveAt(1);
//                         SelectedLegMods.Insert(0, mod);
//                     }
//                 }
//                 else if (mod.ItemCategoryHashes.Contains(3196106184)) // Class
//                 {
//                     if (SelectedClassMods.Count < 2)
//                         SelectedClassMods.Add(mod);
//                     else
//                     {
//                         SelectedClassMods.RemoveAt(1);
//                         SelectedClassMods.Insert(0, mod);
//                     }
//                 }
//             }
//             else if (type == "emptyPrimary")
//             {
//                 if (SelectedCombatMods.Count >= 1)
//                 {
//                     SelectedCombatMods.RemoveAt(SelectedCombatMods.Count - 1);
//                     CoreTypes.RemoveAt(CoreTypes.Count - 1);
//                     // CoreTypes.Insert(0, getElement.GetElementFromString(mod.Plug.EnergyCost.EnergyTypeHash.ToString()));
//                 }
//             }
//             else if (type == "emptyHelm")
//             {
//                 if (SelectedHelmetMods.Count >= 1)
//                     SelectedHelmetMods.RemoveAt(SelectedHelmetMods.Count - 1);
//             }
//             else if (type == "emptyArm")
//             {
//                 if (SelectedArmMods.Count >= 1)
//                     SelectedArmMods.RemoveAt(SelectedArmMods.Count - 1);
//             }
//             else if (type == "emptyChest")
//             {
//                 if (SelectedChestMods.Count >= 1)
//                     SelectedChestMods.RemoveAt(SelectedChestMods.Count - 1);
//             }
//             else if (type == "emptyLeg")
//             {
//                 if (SelectedLegMods.Count >= 1)
//                     SelectedLegMods.RemoveAt(SelectedLegMods.Count - 1);
//             }
//             else if (type == "emptyClass")
//             {
//                 if (SelectedClassMods.Count >= 1)
//                     SelectedClassMods.RemoveAt(SelectedClassMods.Count - 1);
//             }
//         }
//
//         public void ToggleView()
//         {
//             if (View == "compact")
//             {
//                 View = "detailed";
//             }
//             else
//             {
//                 View = "compact";
//             }
//             StateHasChanged();
//         }
//
//         public void ToggleTag(string tag)
//         {
//             if (ActiveTags.Contains(tag))
//                 ActiveTags.Remove(ActiveTags.Single(t => t == tag));
//             else
//                 ActiveTags.Add(tag);
//             
//             StateHasChanged();
//         }
//         
//         protected override Task OnInitializedAsync()
//         {
//             string jsonString = File.ReadAllText("wwwroot/data/ModData.json");
//             ModDataModels = JsonConvert.DeserializeObject<Dictionary<string, ModDataModel>>(jsonString);
//             
//             Console.WriteLine("Factory Created");
//             _apiConfig = new BungieClientConfig();
//             _apiConfig.ApiKey = "02920a8d5c614cc992a23c348e07bf9e";
//             _apiConfig.OAuthClientId = 38045;
//             _client = new BungieApiClient(_apiConfig);
//
//             GetManifest();
//             
//             _viewModel.PropertyChanged += async (sender, e) => { 
//                 await InvokeAsync(StateHasChanged);
//             };
//             // _viewModel.OnInitialization();
//             
//             
//             
//             return base.OnInitializedAsync();
//         }
//         
//         public async void GetManifest()
//         {
//             var result = await _client.Api.Destiny2_GetDestinyManifest();
//
//             var modPath = result.JsonWorldComponentContentPaths["en"]["DestinyInventoryItemDefinition"];
//             var sandboxPath = result.JsonWorldComponentContentPaths["en"]["DestinySandboxPerkDefinition"];
//             var artifactPath = result.JsonWorldComponentContentPaths["en"]["DestinyArtifactDefinition"];
//             string modString = (new WebClient()).DownloadString("https://www.bungie.net" + modPath);
//             string sandboxString = (new WebClient()).DownloadString("https://www.bungie.net" + sandboxPath);
//             string artifactString = (new WebClient()).DownloadString("https://www.bungie.net" + artifactPath);
//             
//             
//             
//             var modData = JsonConvert.DeserializeObject<Dictionary<string, DestinyInventoryItemDefinition>>(modString);
//             var sandboxData = JsonConvert.DeserializeObject<Dictionary<string, DestinySandboxPerkDefinition>>(sandboxString);
//             var artifactData = JsonConvert.DeserializeObject<Dictionary<string, DestinyArtifactDefinition>>(artifactString);
//             Console.WriteLine("Count: {0}", modData.Count);
//
//             var seasonalMods = new List<string>();
//             foreach (var destinyArtifactDefinition in artifactData)
//             {
//                 foreach (var destinyArtifactTierDefinition in destinyArtifactDefinition.Value.Tiers)
//                 {
//                     foreach (var destinyArtifactTierItemDefinition in destinyArtifactTierDefinition.Items)
//                     {
//                         seasonalMods.Add(destinyArtifactTierItemDefinition.ItemHash.ToString());
//                     }
//                 }
//             }
//             
//             Dictionary<string, DestinyInventoryItemDefinition> Mods = new ();
//             
//             foreach (var (key, destinyInventoryItemDefinition) in modData)
//             {
//                 if (!modData.TryGetValue(key, out var item)) continue;
//                 try
//                 {
//                     IEnumerable<uint>? categoryHashes = item.ItemCategoryHashes;
//                     if (
//                         categoryHashes.Contains(4104513227) &&
//                         !item.Plug.EnergyCost.Equals(0) &&
//                         !item.DisplayProperties.Description.Contains("deprecated") &&
//                         !item.DisplayProperties.Name.Contains("Scavenger") &&
//                         !item.DisplayProperties.Name.Contains("Unflinching") &&
//                         !item.DisplayProperties.Name.Contains("Targeting") &&
//                         !item.DisplayProperties.Name.Contains("Loader") &&
//                         !item.DisplayProperties.Name.Contains("Reloader") &&
//                         !item.DisplayProperties.Name.Contains("Finder") &&
//                         !item.DisplayProperties.Name.Contains("Holster") &&
//                         (!item.DisplayProperties.Name.Contains("Reserves") || item.DisplayProperties.Name == "Extra Reserves") &&
//                         !item.DisplayProperties.Name.Contains("Dexterity") &&
//                         !item.DisplayProperties.Name.Contains("Strength") &&
//                         !item.DisplayProperties.Name.Contains("Discipline") &&
//                         !item.DisplayProperties.Name.Contains("Intellect") &&
//                         !item.DisplayProperties.Name.Contains("Mobility") &&
//                         !item.DisplayProperties.Name.Contains("Resilience") &&
//                         !item.DisplayProperties.Name.Contains("Recovery") &&
//                         !item.DisplayProperties.Name.Contains("Empty Mod Socket") &&
//                         !item.ItemTypeDisplayName.Contains("Last Wish Raid Mod")  &&
//                         !item.ItemTypeDisplayName.Contains("Vault of Glass Armor Mod")  &&
//                         !item.ItemTypeDisplayName.Contains("Deep Stone Crypt Raid Mod")  &&
//                         !item.ItemTypeDisplayName.Contains("Garden of Salvation Raid Mod")  &&
//                         !item.ItemTypeDisplayName.Contains("Nightmare Mod")
//                         )
//                     {
//                         Mods.Add(key, destinyInventoryItemDefinition);
//                     }
//             
//                 }
//                 catch (Exception e)
//                 {
//                     // Console.WriteLine("Faulty Key: {0}", key);
//                 }
//             }
//             
//             foreach (var mod in Mods)
//             {
//                 Console.WriteLine("Mod: {0} {1}", mod.Value.DisplayProperties.Name, mod.Value.Hash);
//             }
//             Console.WriteLine("Filter Count: {0}", Mods.Count);
//
//             SandboxData returnData = new SandboxData(sandboxData, seasonalMods);
//
//             _viewModel._Loaded(this, new LoadedArgs(Mods, returnData));
//             // Loaded?.Invoke(this, new LoadedArgs(Mods, returnData));
//             // Loaded?.Invoke(this, EventArgs.Empty);
//         }
//     }
// }