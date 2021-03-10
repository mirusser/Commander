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

        public IEnumerable<Command> GetAllCommands()
            => context.Commands.ToList();

        public Command GetCommandById(int id)
            => context.Commands.FirstOrDefault(x => x.Id == id);
    }
}
