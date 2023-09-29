using BookingManagementApp.Models;

namespace BookingManagementApp.Contracts
{

    //membuat interface contract untuk university controller
    public interface IUniversityRepository
    {
        //membuat interface method getall
        IEnumerable<University> GetAll();

        //membuat interface method getbyGuid
        University? GetByGuid(Guid guid);

        //membuat interface method Create
        University? Create(University university);

        //membuat interface method Update
        bool Update(University university);

        //membuat interface method Delete
        bool Delete(University university);

    }
}
