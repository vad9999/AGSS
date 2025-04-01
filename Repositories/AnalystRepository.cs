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
        public static List<Analyst> GetDataOfAnalyst(int ID, GravitySurveyOnDeleteNoAction context)
        {
            return context.Analysts.Where(a => a.AnalystId == ID).ToList().Cast<Analyst>().ToList();
        }

        public static void SaveChanges(Analyst pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Analysts.Update(pr);
                context.SaveChanges();
            }
        }
    }
}
