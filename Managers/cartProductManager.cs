
using Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers
{
    public class cartProductManager:MainManager<CartProduct>
    {
        public cartProductManager(dbContext _dbContext) : base(_dbContext)
        {

        }
    }
}
