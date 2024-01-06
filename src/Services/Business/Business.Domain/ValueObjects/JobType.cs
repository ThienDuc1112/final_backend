using Business.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.ValueObjects
{
    public class JobType : Enumeration
    {
        public JobType(int id, string name) : base(id, name)
        {
        }

        public static JobType FullTime => new JobType(1, "Full-time");
        public static JobType PartTime => new JobType(2, "Part-time");
        public static JobType Contract => new JobType(3, "Contract");
        public static JobType Freelance => new JobType(4, "Freelance");
        public static JobType Remote => new JobType(5, "Remote");

        public static IEnumerable<JobType> SupportedTypes
        {
            get
            {
                yield return FullTime;
                yield return PartTime;
                yield return Contract;
                yield return Freelance;
                yield return Remote;
            }
        }
    }
}
