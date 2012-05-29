using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoossballPlayars.Commands
{
    public class RegisterPlayarCommand
    {
        public string Name { get; private set; }

        public RegisterPlayarCommand(string name)
        {
            Name = name;
        }
    }
}
