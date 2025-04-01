using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGSS.Entities;

namespace AGSS.Repositories
{
    public static class SpecialistRepository
    {
        public static List<LeadSpecialist> GetDataOfSpecialist(int SpecialistID, GravitySurveyOnDeleteNoAction context)
        {
            return context.LeadSpecialists.Where(l => l.LeadSpecialistId == SpecialistID).ToList().Cast<LeadSpecialist>().ToList();
        }

        public static void SaveChanges(LeadSpecialist pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.LeadSpecialists.Update(pr);
                context.SaveChanges();
            }
        }
    }
}
