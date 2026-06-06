using Infrastructure;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MessageService:MainService<Message>
    {
        public MessageService(dbContext _dbContext):base(_dbContext)
        {
        }

    }
}
