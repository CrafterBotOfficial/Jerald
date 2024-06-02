#if DUMP_DATA

using GorillaNetworking;
using System;

namespace Example
{
    internal static class DumpComputerInfo
    {
        public static async void Begin(GorillaKeyboardButton __instance)
        {
            Console.WriteLine("| Key | Binding | Is Function | ");
            Console.WriteLine("|---|---|---|");
            __instance.transform.parent
                      .GetComponentsInChildren<GorillaKeyboardButton>()
                      .ForEach(key => Console.WriteLine($"| {key.characterString} | {key.Binding}={(int)key.Binding} | {key.functionKey} |"));
        }
    }
}

#endif