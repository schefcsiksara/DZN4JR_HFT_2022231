using DZN4JR_HFT_2022231.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Logic.Interfaces
{
    public interface IPaintService
    {
        Paint Create(Paint entity);
        Paint Read(int id);
        List<Paint> ReadAll();
        Paint Update(Paint entity);
        void Delete(int id);
    }
}
