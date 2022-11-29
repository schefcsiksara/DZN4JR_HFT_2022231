using DZN4JR_HFT_2022231.Models.Entities;
using DZN4JR_HFT_2022231.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Repository.Repositories
{
    public class PaintRepository : BaseRepository<Paint, int>, IPaintRepository
    {
        public PaintRepository(DbContext Db) : base(Db)
        {
        }

        public override Paint Read(int id)
        {
            return ReadAll().SingleOrDefault(x => x.Id == id);
        }
    }
}
