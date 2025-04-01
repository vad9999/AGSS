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
        public static List<ChiefEnginner> GetDataOfEngineer(int ID, GravitySurveyOnDeleteNoAction context)
        {
            return context.ChiefEnginners.Where(e => e.ChiefEnginnerId == ID).ToList().Cast<ChiefEnginner>().ToList();
        }

        public static void SaveChanges(ChiefEnginner pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.ChiefEnginners.Update(pr);
                context.SaveChanges();
            }
        }
    }
}
