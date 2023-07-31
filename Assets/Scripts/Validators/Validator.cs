using System.Collections.Generic;

/*
 * Validation()
 * Nick Chase
 * All input validation from ranges, types and format.
 * Concept is that 
 */

/*
 * Abstract Validaiton class
 */
namespace Inventrack_v1.Validation
{
    public abstract class AbstractValidator<T>
    {
        public abstract List<string> Validate(T entity);
    }

}






