using Business.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.ValueObjects
{
    public class ExperienceYear : Enumeration
    {
        public ExperienceYear(int id, string name) : base(id, name)
        {
        }
        public static ExperienceYear LessThanOne => new(1, "Less than one year");
        public static ExperienceYear OneToThree => new(2, "One to three years");
        public static ExperienceYear ThreeToFive => new(3, "Three to five years");
        public static ExperienceYear FiveToTen => new(4, "Five to Ten years");
        public static ExperienceYear TenPlus => new ExperienceYear(5, "More than 10 years");

        public static IEnumerable<ExperienceYear> GetYearExperiences
        {
            get
            {
                yield return LessThanOne;
                yield return OneToThree;
                yield return ThreeToFive;
                yield return FiveToTen;
                yield return TenPlus;
            }
        }

    }
}
