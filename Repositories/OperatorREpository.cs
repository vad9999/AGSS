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

        public static void SaveChanges(Operator pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Operators.Update(pr);
                context.SaveChanges();
            }
        }
    }
}
