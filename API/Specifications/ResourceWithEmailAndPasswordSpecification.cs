using API.Dtos;
using API.Entintes;

namespace API.Specifications
{
    public class ResourceWithEmailAndPasswordSpecification : BaseSpecification<Resource>
    {
        public ResourceWithEmailAndPasswordSpecification(string email,string password ) 
            : base(r => r.Email == email && r.Password == password)
        {
        }
        public ResourceWithEmailAndPasswordSpecification(string email) 
            : base(r => r.Email == email && r.IsActive == true)
        {
        }
    }
}