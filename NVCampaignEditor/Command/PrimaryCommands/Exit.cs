namespace NVCampaignEditor.Command.PrimaryCommands
{
    /// <summary>
    /// Close the editor.
    /// </summary>
    internal class Exit : CommandBase
    {
        public Exit()
        {
            Aliases = ["exit", "stop"];
        }

        protected override void Help(bool chain = false)
        {
            ListAliases();
            string help = "Why do you need help with the exit command?\n";
            if (!chain) { help += "Press any key to exit."; }
            Console.WriteLine(help);
            if (!chain)
            {
                Console.ReadKey();
                Process([]);
            }
        }

        protected override void Process(string[] argArray)
        {
            Environment.Exit(0);
        }
    }
}
