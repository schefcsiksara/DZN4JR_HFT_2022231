using DZN4JR_HFT_2022231.Logic.Interfaces;
using DZN4JR_HFT_2022231.Models.Entities;
using DZN4JR_HFT_2022231.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Logic.Services
{
    public class PaintService : IPaintService
    {
        private IPaintRepository repository;

        public PaintService(IPaintRepository repository)
        {
            this.repository = repository;
        }

        public Paint Create(Paint entity)
        {
            return repository.Create(entity);
        }

        public void Delete(int id)
        {
            var entity = Read(id);

            repository.Delete(entity);
        }

        public Paint Read(int id)
        {
            return repository.Read(id);
        }

        public List<Paint> ReadAll()
        {
            return repository.ReadAll().ToList();
        }

        public Paint Update(Paint entity)
        {
            var oldEntity = Read(entity.Id);

            oldEntity = entity;

            return repository.Update(entity);
        }
    }
}
