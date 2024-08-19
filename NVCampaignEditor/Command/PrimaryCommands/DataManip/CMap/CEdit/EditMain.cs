namespace NVCampaignEditor.Command.PrimaryCommands.DataManip.CMap.CEdit
{
    /// <summary>
    /// This should be used to edit the map indicated by argument 0.
    /// </summary>
    internal partial class EditMain : CommandBase
    {
        public EditMain()
        {
            Aliases = ["edit", "e"];
        }

        protected override void Help(bool chain = false)
        {
            // Because this uses a custom subcommand system, we can't use listsubcommands.
            Console.WriteLine("Edit: Edits the map. All subcommands have prompt based editors." +
                "Subcommands:\n" +
                "Foes: edits foes.\n" +
                "Aliases: f\n" +
                "Usage: map edit foe <mapIndex>\n" +
                "Pickups: edits pickups.\n" +
                "Aliases: p\n" +
                "Usage: map edit pickups <mapIndex>\n" +
                "Transitions: edits transitions.\n" +
                "Aliases: t\n" +
                "Usage: map edit transitions <mapIndex>\n");
            ListAliases();
        }

        protected override void Process(string[] argArray)
        {
            // map index
            int index = int.Parse(argArray[0]);

            string[] newArgs = argArray.ToList().GetRange(2, argArray.Length - 2).ToArray();

            // Command and execution.
            switch (argArray[1])
            {
                case "foes":
                case "f": EditHelpers.Foes(index); break;
                case "pickups":
                case "p": EditHelpers.Pickups(index); break;
                case "transitions":
                case "t": EditHelpers.Transitions(index); break;
                default: throw new ArgumentException("Invalid subcommand.");
            }
        }
    }
}
