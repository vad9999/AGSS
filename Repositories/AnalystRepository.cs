using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGSS.Entities;

namespace AGSS.Repositories
{
    public static class AnalystRepository
    {
        public static List<Analyst> GetDataOfAnalyst()
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Analysts.ToList();
            }
        }

        public static void SaveChanges(Analyst pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Analysts.Update(pr);
                context.SaveChanges();
            }
        }

        public static int GetAnalyst()
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Analysts.OrderByDescending(e => e.AnalystId).FirstOrDefault()?.AnalystId ?? -1;
            }
        }
    }
}
