using Business.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.ValueObjects
{
    public class BusinessSize : Enumeration
    {
        public BusinessSize(int id, string name) : base(id, name)
        {
        }
        public static BusinessSize Small => new(1,"1-50 employees");
        public static BusinessSize Medium => new(2,"51-250 employees");
        public static BusinessSize Large => new(3,"251-1000 employees");
        public static BusinessSize Enterprise => new(4,"1001+ employees");

        public static IEnumerable<BusinessSize> SupportedSizes
        {
            get
            {
                yield return Small;
                yield return Medium;
                yield return Large;
                yield return Enterprise;
            }
        }
    }
}
