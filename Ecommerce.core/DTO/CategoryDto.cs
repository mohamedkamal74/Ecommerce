using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.core.DTO
{
    public record CategoryDto(string name, string description);
    public record UpdateCategoryDto(int id,string name, string description);
}
