
using API.Models;

namespace API.Contracts
{

    //membuat interface contract untuk university controller
    public interface IUniversityRepository : IGeneralRepository<University>
    {

        public University? GetUniversityNameByCode(string code);
    }
}
