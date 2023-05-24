using VetApp.Controllers.Results;
using VetOffice.Models;

namespace VetApp.Services
{
    public class GetPetsService
    {
        private readonly VetOfficeDbContext dbContext = new VetOfficeDbContext();

        public List<GetPetsResult> Get()
        {
            List<GetPetsResult> result = new List<GetPetsResult>();
            var pets = dbContext.Animal.ToList();

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
    }
}
