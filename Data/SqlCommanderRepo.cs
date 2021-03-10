using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext context;

        public SqlCommanderRepo(CommanderContext context)
        {
            this.context = context;
        }

        public void CreateCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            context.Commands.Add(command);
        }

        public void DeleteCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            context.Commands.Remove(command);
        }

        public IEnumerable<Command> GetAllCommands()
            => context.Commands.ToList();

        public Command GetCommandById(int id)
            => context.Commands.FirstOrDefault(x => x.Id == id);

        public bool SaveChanges()
            => context.SaveChanges() >= 0;

        public void UpdateCommand(Command command)
        {
            //Nothing - you don't really need to use 'Update' method to change the object values
            //beacuse we map updated model to database model
        }
    }
}
