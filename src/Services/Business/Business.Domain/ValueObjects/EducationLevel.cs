using Business.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.ValueObjects
{
    public class EducationLevel : Enumeration
    {
        public EducationLevel(int id, string name) : base(id, name)
        {
        }

        public static EducationLevel HighSchool => new EducationLevel(1, "High School");
        public static EducationLevel Associate => new EducationLevel(2, "Associate Degree");
        public static EducationLevel Bachelor => new EducationLevel(3, "Bachelor's Degree");
        public static EducationLevel Master => new EducationLevel(4, "Master's Degree");
        public static EducationLevel Doctorate => new EducationLevel(5, "Doctorate Degree");

        public static IEnumerable<EducationLevel> GetEducationLevels
        {
            get
            {
                yield return Associate;
                yield return HighSchool;
                yield return Bachelor;
                yield return Master;
                yield return Doctorate;
            }
        }
    }
}
