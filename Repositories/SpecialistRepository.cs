using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using AGSS.Entities;

namespace AGSS.Repositories
{
    public static class SpecialistRepository
    {
        public static List<LeadSpecialist> GetDataOfSpecialist()
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.LeadSpecialists.ToList();
            }
        }

        public static void SaveChanges(LeadSpecialist pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.LeadSpecialists.Update(pr);
                context.SaveChanges();
            }
        }

        public static List<LeadSpecialist> GetFreeSpecialists()
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.LeadSpecialists.ToList();
            }
        }

        public static LeadSpecialist GetSpecialist(string login, string password)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.LeadSpecialists.FirstOrDefault(c => c.Login == login && c.Password == password);
            }
        }
    }
}
