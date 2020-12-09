using System;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Skyrim;
using System.IO;
using Newtonsoft.Json;

namespace ItemWeightCustomizer
{
    public static class Program
    {

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

        // TODO : Get appropriate weight from sections, and fall-back to simplistic weight values.
        // private static float GetWeigh
        
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

            Config config;

            try
            {
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configFilePath));
            }
            catch (JsonSerializationException jsonException)
            {
                errorMessage = "Failed to Parse config.json, please review the format.";
                SynthesisLog(errorMessage);
                throw new JsonSerializationException(errorMessage, jsonException);
            }

            var weights = config.Weights;
            var bookWeight = weights.Books;
            var ingredientWeight = weights.Ingredients;
            var scrollWeight = weights.Scrolls;
            var soulGemWeight = weights.Soulgems;
            var armorWeight = weights.Armors;

            // ***** PRINT CONFIG SETTINGS ***** //
            SynthesisLog("Item Weight Configuration:", true);
            if (bookWeight >= 0) SynthesisLog($"All Books will have their weights set to {bookWeight}");
            if (ingredientWeight >= 0)
                SynthesisLog($"All Ingredients will have their weights set to {ingredientWeight}");
            if (scrollWeight >= 0) SynthesisLog($"All Scrolls will have their weights set to {scrollWeight}");
            if (soulGemWeight >= 0)
                SynthesisLog($"All Soul Gems will have their weights set to {soulGemWeight}");
            if (armorWeight >= 0) SynthesisLog($"All Armours will have their weights set to {armorWeight}");

            // START WORK ...
            SynthesisLog("Running Item Weight Customizer ...", true);
            
            // ***** BOOKS ***** //
            if (bookWeight >= 0)
            {
                foreach (IBookGetter book in state.LoadOrder.PriorityOrder.WinningOverrides<IBookGetter>())
                {
                    
                    if (Math.Abs(book.Weight - bookWeight) < float.Epsilon) continue;
                    var modifiedBook = book.DeepCopy();
                    modifiedBook.Weight = bookWeight;
                    state.PatchMod.Books.Add(modifiedBook);
                }
            }

            // ***** INGREDIENTS ***** //
            if (ingredientWeight >= 0)
            {
                foreach (IIngredientGetter ingredient in state.LoadOrder.PriorityOrder
                    .WinningOverrides<IIngredientGetter>())
                {
                    if (Math.Abs(ingredient.Weight - ingredientWeight) < float.Epsilon) continue;
                    var modifiedIngredient = ingredient.DeepCopy();
                    modifiedIngredient.Weight = ingredientWeight;
                    state.PatchMod.Ingredients.Add(modifiedIngredient);
                }
            }

            // ***** SCROLLS ***** //
            if (scrollWeight >= 0)
            {
                foreach (IScrollGetter scroll in state.LoadOrder.PriorityOrder.WinningOverrides<IScrollGetter>())
                {
                    if (Math.Abs(scroll.Weight - scrollWeight) < float.Epsilon) continue;
                    var modifiedScroll = scroll.DeepCopy();
                    modifiedScroll.Weight = scrollWeight;
                    state.PatchMod.Scrolls.Add(modifiedScroll);
                }
            }

            // ***** SOUL GEMS ***** //
            if (soulGemWeight >= 0)
            {
                foreach (ISoulGemGetter soulGem in state.LoadOrder.PriorityOrder.WinningOverrides<ISoulGemGetter>())
                {
                    if (Math.Abs(soulGem.Weight - soulGemWeight) < float.Epsilon) continue;
                    var modifiedSoulGem = soulGem.DeepCopy();
                    modifiedSoulGem.Weight = soulGemWeight;
                    state.PatchMod.SoulGems.Add(modifiedSoulGem);
                }
            }

            // ***** ARMOURS ***** //
            if (armorWeight >= 0)
            {
                foreach (IArmorGetter armor in state.LoadOrder.PriorityOrder.WinningOverrides<IArmorGetter>())
                {
                    if (Math.Abs(armor.Weight - armorWeight) < float.Epsilon) continue;
                    var modifiedArmor = armor.DeepCopy();
                    modifiedArmor.Weight = armorWeight;
                    state.PatchMod.Armors.Add(modifiedArmor);
                }
            }

            SynthesisLog("Done patching weights!", true);
        }
    }
}