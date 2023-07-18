using testNST.Dtos;
using testNST.Models;

namespace testNST
{
    public interface IPersonRepository
    {
        Task<Person?> Delete(long id);
        Task<List<Person>> Get();
        Task<Person?> Get(long id);
        Task PostPerson(Person person);
        Task PutPerson(long id, PersonDto item);
    }
}