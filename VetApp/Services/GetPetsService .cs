using Microsoft.EntityFrameworkCore;
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
            var pets = dbContext.Animal.Include(x => x.User).ToList();

            foreach (var pet in pets)
            {
                result.Add(new GetPetsResult()
                {
                    Name = pet.User.name,
                    PetId = pet.id_animal,
                 
                });
            }
            return result;
        }
    }
}
