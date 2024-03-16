using Business.Application.DTOs.Job;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Jobs.Queries.GetListJob
{
    public class GetListJobQuery : IRequest<List<GetJobDTO>>
    {
        public int? Page { get; set; }
        public string? Query { get; set; }
        public string? JobType { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public int? Career { get; set; }
        public List<string> Experience { get; set; } = new List<string>();
        public string? Date { get; set; }
        public List<string> Position { get; set; } = new List<string>();
        public List<string> Education { get; set; } = new List<string>();
    }
}
