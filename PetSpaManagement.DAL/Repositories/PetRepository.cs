using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaManagement.DAL.Repositories
{
    public class PetRepository
    {
        private PetSpaManagementContext _context;
        public PetRepository()
        {
            _context = new PetSpaManagementContext();
        }
        public List<Entities.Pet> GetAllPets()
        {
            return _context.Pets.ToList();
        }
        public void AddPet(Entities.Pet pet)
        {
            _context.Pets.Add(pet);
            _context.SaveChanges();
        }
        public void UpdatePet(Entities.Pet pet)
        {
            var existingPet = _context.Pets.Find(pet.PetId);
            if (existingPet != null)
            {
                existingPet.PetName = pet.PetName;
                existingPet.Species = pet.Species;
                existingPet.Breed = pet.Breed;
                existingPet.Gender = pet.Gender;
                _context.SaveChanges();
            }
        }
        public void DeletePet(Entities.Pet pet)
        {
            var existingPet = _context.Pets.Find(pet.PetId);
            if (existingPet != null)
            {
                _context.Pets.Remove(existingPet);
                _context.SaveChanges();
            }
        }
    }
}
