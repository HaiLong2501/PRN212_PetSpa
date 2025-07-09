using PetSpaManagement.DAL.Entities;
using PetSpaManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaManagement.BLL.Pets
{
    public class PetController
    {
        private PetRepository _repo;
        public PetController()
        {
            _repo = new PetRepository();
        }
        public List<Pet> GetAllPets()
        {
            return _repo.GetAllPets();
        }
        public void AddPet(Pet pet)
        {
            _repo.AddPet(pet);
        }
        public void UpdatePet(Pet pet)
        {
            _repo.UpdatePet(pet);
        }
        public void DeletePet(Pet pet)
        {
            _repo.DeletePet(pet);
        }

        public List<Pet> GetPetsByCustomerId(int customerId)
        {
            var allPets = _repo.GetAllPets();
            return allPets.Where(p => p.CustomerId == customerId).ToList();
        }
    }
}
