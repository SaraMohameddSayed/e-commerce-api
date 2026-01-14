using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Models;
namespace Managers
{
    public class notificationManager:MainManager<Notification>
    {
        public notificationManager(dbContext _dbcontext):base(_dbcontext){
        }

    }
}
