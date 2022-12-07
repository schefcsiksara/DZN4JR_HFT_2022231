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
            if (string.IsNullOrEmpty(entity.Color))
            {
                throw new ApplicationException("Color cannot be empty");
            }

            if (entity.Color.Length > 50)
            {
                throw new ApplicationException("Color cannot be more than 50 characters");
            }

            if (entity.Type.Length > 20)
            {
                throw new ApplicationException("Type cannot be more than 30 characters");
            }

            if (entity.Volume.Length > 20)
            {
                throw new ApplicationException("Volume cannot be more than 30 characters");
            }

            return repository.Create(entity);
        }

        public void Delete(int id)
        {
            var entity = Read(id);

            if (entity == null)
            {
                throw new ApplicationException("There is no brand with the given id");
            }

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
            if (string.IsNullOrEmpty(entity.Color))
            {
                throw new ApplicationException("Color cannot be empty");
            }

            if (string.IsNullOrEmpty(entity.Type))
            {
                throw new ApplicationException("Address cannot be empty");
            }

            if (string.IsNullOrEmpty(entity.Volume))
            {
                throw new ApplicationException("Email cannot be empty");
            }

            if (entity.Color.Length > 50)
            {
                throw new ApplicationException("Color cannot be more than 50 characters");
            }

            if (entity.Type.Length > 20)
            {
                throw new ApplicationException("Type cannot be more than 30 characters");
            }

            if (entity.Volume.Length > 20)
            {
                throw new ApplicationException("Volume cannot be more than 30 characters");
            }

            var oldEntity = Read(entity.Id);

            oldEntity = entity;

            return repository.Update(entity);
        }
      
    }
}
