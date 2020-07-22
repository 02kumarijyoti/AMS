using API.Dtos;
using API.Entintes;

namespace API.Specifications
{
    public class ResourceByEmailSpecification : BaseSpecification<Resource>
    {
        public ResourceByEmailSpecification(string email,string password ) 
            : base(r => r.Email == email && r.Password == password)
        {
        }
        public ResourceByEmailSpecification(string email) 
            : base(r => r.Email == email && r.IsActive == true)
        {
        }
    }
}