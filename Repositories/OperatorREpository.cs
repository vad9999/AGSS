using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGSS.Entities;

namespace AGSS.Repositories
{
    public static class OperatorRepository
    {
        public static List<Operator> GetDataOfOperator()
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Operators.ToList();
            }

        }

        public static Operator GetOperator(string login, string password)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Operators.FirstOrDefault(o => o.Login == login && o.Password == password);
            }
        }

        public static void SaveChanges(Operator pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Operators.Update(pr);
                context.SaveChanges();
            }
        }

        public static List<string> GetProjectsByOperator(Operator @operator)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                var flights = context.Flights.Where(f => f.OperatorId == @operator.OperatorId).Select(f => f.ProjectId).ToList();
                return context.Projects.Where(p => flights.Contains(p.ProjectId)).Select(p => p.ProjectName).ToList();
            }
        }
    }
}
