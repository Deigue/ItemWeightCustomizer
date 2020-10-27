using System;
using System.Collections.Generic;
using System.Linq;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Skyrim;
using System.IO;

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
        }

        public static void RunPatch(SynthesisState<ISkyrimMod, ISkyrimModGetter> state)
        {
            string configFilePath = Path.Combine(state.ExtraSettingsDataPath, "config.json");
            if (!File.Exists(configFilePath))
            {
                var errorMessage = "Cannot find config.json for Custom Weights.";
                var fileException = new FileNotFoundException(errorMessage, configFilePath);
                SynthesisLog(errorMessage, true);
                throw fileException;
            }


            Console.WriteLine("Running Item Weight Customizer ...");

            foreach (var item in state.LoadOrder.PriorityOrder)
            {
                // iterate some items.
            }
        }
    }
}