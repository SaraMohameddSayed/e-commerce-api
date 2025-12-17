using Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers
{
    public class governorateManager:MainManager<Governorate>
    {
        public governorateManager(dbContext _context) : base(_context)
        {

        }
    }
}
