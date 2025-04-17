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
        public static List<ChiefEnginner> GetDataOfEngineer(int ID)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.ChiefEnginners.Where(e => e.ChiefEnginnerId == ID).ToList().Cast<ChiefEnginner>().ToList();
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

        public static int GetEnginner()
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.ChiefEnginners.LastOrDefault().ChiefEnginnerId;
            }
        }
    }
}
