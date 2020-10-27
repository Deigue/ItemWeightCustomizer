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
                throw new JsonSerializationException(errorMessage , jsonException);
            }

            // ***** PRINT CONFIG SETTINGS ***** //
            SynthesisLog($"All books (BOOK) will have their weight set to {config.getBookWeight()}");


            // START WORK ...
            Console.WriteLine("Running Item Weight Customizer ...");

            // ***** BOOKS ***** //
            foreach (IBookGetter book in state.LoadOrder.PriorityOrder.WinningOverrides<IBookGetter>())
            {   
                if(book.Weight != config.getBookWeight())
                {
                    //var modifiedBook = book.
                }
            }
        }
    }
}