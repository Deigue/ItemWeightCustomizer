using System;
using System.Collections.Generic;
using System.Linq;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Skyrim;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ItemWeightCustomizer
{
    public class Program
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

        public static void RunPatch(SynthesisState<ISkyrimMod, ISkyrimModGetter> state)
        {
            string configFilePath = Path.Combine(state.ExtraSettingsDataPath, "config.json");
            string errorMessage = "";

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

            var bookWeight = config.getBookWeight();
            var ingredientWeight = config.getIngredientWeight();
            var scrollWeight = config.getScrollWeight();
            var soulGemWeight = config.getSoulGemWeight();

            // ***** PRINT CONFIG SETTINGS ***** //
            SynthesisLog($"Item Weight Configuration:", true);
            if (bookWeight >= 0) SynthesisLog($"All books (BOOK) will have their weights set to {bookWeight}");
            if (ingredientWeight >= 0) SynthesisLog($"All ingredients (INGR) will have their weights set to {ingredientWeight}");
            if (scrollWeight >= 0) SynthesisLog($"All scrolls (SCRL) will have their weights set to {scrollWeight}");
            if (soulGemWeight >= 0) SynthesisLog($"All soul gems (SLGM) will have their weights set to {soulGemWeight}");

            // START WORK ...
            Console.WriteLine("Running Item Weight Customizer ...");

            // ***** BOOKS ***** //
            if (bookWeight >= 0)
            { 
                foreach (IBookGetter book in state.LoadOrder.PriorityOrder.WinningOverrides<IBookGetter>())
                {
                    if (book.Weight != bookWeight)
                    {
                        var modifiedBook = book.DeepCopy();
                        modifiedBook.Weight = bookWeight;
                        state.PatchMod.Books.Add(modifiedBook);
                    }
                }
            }

            // ***** INGREDIENTS ***** //
            if (ingredientWeight >= 0)
            {
                foreach (IIngredientGetter ingredient in state.LoadOrder.PriorityOrder.WinningOverrides<IIngredientGetter>())
                {
                    if (ingredient.Weight != ingredientWeight)
                    {
                        var modifiedIngredient = ingredient.DeepCopy();
                        modifiedIngredient.Weight = ingredientWeight;
                        state.PatchMod.Ingredients.Add(modifiedIngredient);
                    }
                }
            }

            // ***** SCROLLS ***** //
            if (scrollWeight >= 0)
            {
                foreach (IScrollGetter scroll in state.LoadOrder.PriorityOrder.WinningOverrides<IScrollGetter>())
                {
                    if (scroll.Weight != scrollWeight)
                    {
                        var modifiedScroll = scroll.DeepCopy();
                        modifiedScroll.Weight = scrollWeight;
                        state.PatchMod.Scrolls.Add(modifiedScroll);
                    }
                }
            }

            // ***** SOUL GEMS ***** //
            if (soulGemWeight >= 0)
            {
                foreach (ISoulGemGetter soulGem in state.LoadOrder.PriorityOrder.WinningOverrides<ISoulGemGetter>())
                {
                    if (soulGem.Weight != soulGemWeight)
                    {
                        var modifiedSoulGem = soulGem.DeepCopy();
                        modifiedSoulGem.Weight = soulGemWeight;
                        state.PatchMod.SoulGems.Add(modifiedSoulGem);
                    }
                }
            }
            
            SynthesisLog($"Done patching weights.");
        }
    }
}