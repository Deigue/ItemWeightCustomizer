using System;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Skyrim;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Noggog;

namespace ItemWeightCustomizer
{
    public static class Program
    {
        private static Config Config { get; set; } = null!;

        public static int Main(string[] args)
        {
            return SynthesisPipeline.Instance.Patch<ISkyrimMod, ISkyrimModGetter>(
                args: args,
                patcher: RunPatch,
                new UserPreferences()
                {
                    ActionsForEmptyArgs = new RunDefaultPatcher
                    {
                        IdentifyingModKey = "ItemWeightCustomizer.esp",
                        TargetRelease = GameRelease.SkyrimSE
                    }
                }
            );
        }

        private static void SynthesisLog(string message, bool special = false)
        {
            if (special)
            {
                Console.WriteLine();
                Console.Write(">>> ");
            }

            Console.WriteLine(message);
            if (special) Console.WriteLine();
        }

        private static float? FindWeightCategory(string withType, string? editorId)
        {
            if (editorId is null) return null;
            WeightCategory? category = Config.Categories.FirstOrDefault(x => x.Types.Contains(withType));
            if (category == null || !category.EditorIds.Contains(editorId)) return null;
            SynthesisLog($"{editorId} matches the \"{category.Name}\" category, using weight {category.Weight}");
            return category.Weight;
        }

        private static bool CategoryTypeExists(string type)
        {
            return Config.Categories.Any(c => c.Types.Contains(type));
        }

        private static void RunPatch(SynthesisState<ISkyrimMod, ISkyrimModGetter> state)
        {
            string configFilePath = Path.Combine(state.ExtraSettingsDataPath, "config.json");
            string errorMessage;

            if (!File.Exists(configFilePath))
            {
                errorMessage = "Cannot find config.json for Custom Weights.";
                SynthesisLog(errorMessage);
                throw new FileNotFoundException(errorMessage, configFilePath);
            }

            try
            {
                Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configFilePath));
            }
            catch (JsonSerializationException jsonException)
            {
                errorMessage = "Failed to Parse config.json, please review the format.";
                SynthesisLog(errorMessage);
                throw new JsonSerializationException(errorMessage, jsonException);
            }

            var weights = Config.Weights;
            var bookWeight = weights.Books;
            var ingredientWeight = weights.Ingredients;
            var scrollWeight = weights.Scrolls;
            var soulGemWeight = weights.Soulgems;
            var armorWeight = weights.Armors;
            var weaponWeight = weights.Weapons;

            // ***** PRINT CONFIG SETTINGS ***** //
            SynthesisLog("Item Weight Configuration:", true);
            if (bookWeight >= 0) SynthesisLog($"BOOKS will have their weights set to {bookWeight}");
            if (ingredientWeight >= 0)
                SynthesisLog($"INGREDIENTS will have their weights set to {ingredientWeight}");
            if (scrollWeight >= 0) SynthesisLog($"SCROLLS will have their weights set to {scrollWeight}");
            if (soulGemWeight >= 0)
                SynthesisLog($"SOUL GEMS will have their weights set to {soulGemWeight}");
            if (armorWeight >= 0) SynthesisLog($"ARMOURS will have their weights set to {armorWeight}");
            if (weaponWeight >= 0) SynthesisLog($"WEAPONS will have their weights set to {weaponWeight}");
            Config.Categories.Where(c => c.Weight >= 0).ForEach(c =>
                SynthesisLog($"\"{c.Name}\" category matches will have their weights set to {c.Weight}"));

            // START WORK ...
            SynthesisLog("Running Item Weight Customizer ...", true);

            // ***** BOOKS ***** //
            if (bookWeight >= 0 || CategoryTypeExists("books"))
            {
                foreach (IBookGetter book in state.LoadOrder.PriorityOrder.WinningOverrides<IBookGetter>())
                {
                    var newWeight = FindWeightCategory("books", book.EditorID) ?? bookWeight;
                    if (newWeight < 0) continue;
                    if (Math.Abs(book.Weight - newWeight) < float.Epsilon) continue;
                    var modifiedBook = book.DeepCopy();
                    modifiedBook.Weight = newWeight;
                    state.PatchMod.Books.Add(modifiedBook);
                }
            }

            // ***** INGREDIENTS ***** //
            if (ingredientWeight >= 0 || CategoryTypeExists("ingredients"))
            {
                foreach (IIngredientGetter ingredient in state.LoadOrder.PriorityOrder
                    .WinningOverrides<IIngredientGetter>())
                {
                    var newWeight = FindWeightCategory("ingredients", ingredient.EditorID) ?? ingredientWeight;
                    if (newWeight < 0) continue;
                    if (Math.Abs(ingredient.Weight - newWeight) < float.Epsilon) continue;
                    var modifiedIngredient = ingredient.DeepCopy();
                    modifiedIngredient.Weight = newWeight;
                    state.PatchMod.Ingredients.Add(modifiedIngredient);
                }
            }

            // ***** SCROLLS ***** //
            if (scrollWeight >= 0 || CategoryTypeExists("scrolls"))
            {
                foreach (IScrollGetter scroll in state.LoadOrder.PriorityOrder.WinningOverrides<IScrollGetter>())
                {
                    var newWeight = FindWeightCategory("scrolls", scroll.EditorID) ?? scrollWeight;
                    if (newWeight < 0) continue;
                    if (Math.Abs(scroll.Weight - newWeight) < float.Epsilon) continue;
                    var modifiedScroll = scroll.DeepCopy();
                    modifiedScroll.Weight = newWeight;
                    state.PatchMod.Scrolls.Add(modifiedScroll);
                }
            }

            // ***** SOUL GEMS ***** //
            if (soulGemWeight >= 0 || CategoryTypeExists("soulgems"))
            {
                foreach (ISoulGemGetter soulGem in state.LoadOrder.PriorityOrder.WinningOverrides<ISoulGemGetter>())
                {
                    var newWeight = FindWeightCategory("soulgems", soulGem.EditorID) ?? soulGemWeight;
                    if (newWeight < 0) continue;
                    if (Math.Abs(soulGem.Weight - newWeight) < float.Epsilon) continue;
                    var modifiedSoulGem = soulGem.DeepCopy();
                    modifiedSoulGem.Weight = newWeight;
                    state.PatchMod.SoulGems.Add(modifiedSoulGem);
                }
            }

            // ***** ARMOURS ***** //
            if (armorWeight >= 0 || CategoryTypeExists("armors"))
            {
                foreach (IArmorGetter armor in state.LoadOrder.PriorityOrder.WinningOverrides<IArmorGetter>())
                {
                    var newWeight = FindWeightCategory("armors", armor.EditorID) ?? armorWeight;
                    if (Math.Abs(armor.Weight - newWeight) < float.Epsilon) continue;
                    var modifiedArmor = armor.DeepCopy();
                    modifiedArmor.Weight = newWeight;
                    state.PatchMod.Armors.Add(modifiedArmor);
                }
            }

            // ***** WEAPONS ***** //
            if (weaponWeight >= 0 || CategoryTypeExists("weapons"))
            {
                foreach (IWeaponGetter weapon in state.LoadOrder.PriorityOrder.WinningOverrides<IWeaponGetter>())
                {
                    var newWeight = FindWeightCategory("weapons", weapon.EditorID) ?? weaponWeight;
                    if (newWeight < 0) continue;
                    if (weapon.BasicStats != null &&
                        Math.Abs(weapon.BasicStats.Weight - newWeight) < float.Epsilon) continue;
                    var modifiedWeapon = weapon.DeepCopy();
                    modifiedWeapon.BasicStats!.Weight = newWeight;
                    state.PatchMod.Weapons.Add(modifiedWeapon);
                }
            }

            SynthesisLog("Done patching weights!", true);
        }
    }
}