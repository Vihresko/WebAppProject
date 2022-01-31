using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkDiary.Database.Data;
using WorkDiary.Database.Data.Models;

namespace WorkDiary.Database.DataProcess
{
    public class ClientEditor
    {
        public ClientEditor()
        {

        }
        public void AddClient(Client client, WorkDiaryDbContext dbContext)
        {
            dbContext.Clients.Add(client);
            dbContext.SaveChanges();
        }
    }
}
