using Business.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.ValueObjects
{
    public class CareerLevel : Enumeration
    {
        public CareerLevel(int id, string name) : base(id, name)
        {
        }

        public static CareerLevel Intern => new CareerLevel(1, "Fresher");
        public static CareerLevel Fresher => new CareerLevel(2, "Junior");
        public static CareerLevel Staff => new CareerLevel(3, "Senior");
        public static CareerLevel Manager => new CareerLevel(4, "Manager");
        public static CareerLevel Director => new CareerLevel(5, "Director");

        public static IEnumerable<CareerLevel> SupportedLevels
        {
            get
            {
                yield return Intern;
                yield return Fresher;
                yield return Staff;
                yield return Manager;
                yield return Director;
            }
        }
    }
}
