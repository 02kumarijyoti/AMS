using System.Linq;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Specifications
{
    public class SpecificationEvaluator<TEntity> where TEntity:BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecification<TEntity> spec){
            var query= inputQuery;
            if(spec.Criteria!=null){
                query=query.Where(spec.Criteria);//p =>p.producttypeId==id;
            }
            return query;
        }
    }
}