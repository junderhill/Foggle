using System;
using System.Configuration;
using Foggle;

namespace FoggleConsoleTestingApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Foggle Testing Application\n");

            PrintAppSettings();

            ExecuteFeature<Feature1>();
            ExecuteFeature<Feature2>();
            ExecuteFeature<Feature3>();

            Console.ReadKey();
        }

        private static void PrintAppSettings()
        {
            Console.WriteLine("** APP SETTINGS **");
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                var value = ConfigurationManager.AppSettings[key];

                Console.WriteLine($"{key} \t {value}");
            }
            Console.WriteLine();
        }

        private static void ExecuteFeature<TFoggleFeature>()
            where TFoggleFeature : FoggleFeature
        {
            var enabled = Feature.IsEnabled<TFoggleFeature>();
            var status = enabled ? "enabled" : "disabled";

            Console.ForegroundColor = enabled ? ConsoleColor.Green : ConsoleColor.Red;

            Console.WriteLine($"Feature {typeof(TFoggleFeature).FullName} is {status}.");
            Console.ResetColor();
        }
    }

    class Feature1 : FoggleFeature
    {

    }
    class Feature2 : FoggleFeature
    {

    }
    [FoggleByHostname]
    class Feature3 : FoggleFeature
    {

    }

}
