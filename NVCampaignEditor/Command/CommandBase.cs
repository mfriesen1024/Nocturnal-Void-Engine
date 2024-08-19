namespace NVCampaignEditor.Command
{
    public abstract class CommandBase
    {
        private string[] aliases;
        private CommandBase[] subcommands = [];

        public string[] Aliases { get => aliases; protected set => aliases = value; }
        public CommandBase[] Subcommands { get => subcommands; protected set => subcommands = value; }

        /// <summary>
        /// Used to execute the command base, first checking subcommands, then processing the command.
        /// </summary>
        /// <param name="argArray">The arguments to be used.</param>
        public virtual void Invoke(string[] argArray)
        {
            var argList = argArray.ToList();

            try
            {
                string s1 = argList[0]; // Check this against subcommand aliases.

                foreach (CommandBase subcommand in subcommands)
                {
                    if (subcommand.Aliases != null)
                    {
                        if (subcommand.Aliases.Contains(s1))
                        {
                            // In theory, this shouldnt cause issues, but be prepared to define a new var for this.
                            // Remove the subcommand from the args we pass.
                            argList.RemoveAt(0);
                            subcommand.Invoke(argList.ToArray()); return;
                        }
                    }
                }

                if (s1 == "help") { Help(); return; }
            }
            catch (Exception e) { if (e is not ArgumentOutOfRangeException) { throw; } }

            Process(argArray);
        }

        /// <summary>
        /// Called if no subcommands were invoked by Invoke().
        /// This should process the command, and be prepared to handle invalid arguments.
        /// </summary>
        /// <param name="argArray">The arguments to be used.</param>
        protected abstract void Process(string[] argArray);
        /// <summary>
        /// Called if user enters "help", after subcommands, but before running process.
        /// Should tell the user how to and why to use the command.
        /// </summary>
        /// <param name="chain">Used to determine if this was called elsewhere, so we can prevent execution chains.</param>
        protected abstract void Help(bool chain = false);

        /// <summary>
        /// Prints help messages for each subcommand. Useful for help commands in a parent command.
        /// </summary>
        protected void ListSubcommands()
        {
            foreach (CommandBase subcommand in subcommands) { subcommand.Help(true); }
        }

        /// <summary>
        /// Prints aliases. Useful for help commands.
        /// </summary>
        protected void ListAliases()
        {
            Console.Write("Aliases: ");
            foreach (string alias in aliases) { Console.Write(alias + " "); }
            Console.WriteLine();
        }
    }
}
