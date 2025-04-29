using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGSS.Entities;

namespace AGSS.Repositories
{
    public static class EngineerRepository
    {
        public static List<ChiefEnginner> GetDataOfEngineer()
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.ChiefEnginners.ToList();
            }
        }

        public static void SaveChanges(ChiefEnginner pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.ChiefEnginners.Update(pr);
                context.SaveChanges();
            }
        }

        public static int GetEngineer()
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.ChiefEnginners.OrderByDescending(e => e.ChiefEnginnerId).FirstOrDefault()?.ChiefEnginnerId ?? -1;
            }
        }

    }
}
