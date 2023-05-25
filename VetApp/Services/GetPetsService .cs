using VetApp.Controllers.Results;
using VetOffice.Models;

namespace VetApp.Services
{
    public class GetPetsService
    {
        private readonly VetOfficeDbContext dbContext = new VetOfficeDbContext();

        public List<GetPetsResult> Get(int userId)
        {
            List<GetPetsResult> result = new List<GetPetsResult>();
            var pets = dbContext.Animal.Where(x => x.id_user == userId).ToList();

            foreach (var pet in pets)
            {
                result.Add(new GetPetsResult()
                {
                    Name = pet.name_a,
                    PetId = pet.id_animal,
                    AnimalType = pet.petType,
                    BirthDate = pet.dateofbirth

                });
            }
            return result;
        }

        public void AddPet(int userId, string name, string type, DateTime birthday)
        {
            var animal = new Animals() { id_user = userId, dateofbirth = birthday, name_a = name, petType = type };
            dbContext.Animal.Add(animal);
            dbContext.SaveChanges();
        }
    }
}
