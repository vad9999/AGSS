using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGSS.Entities;

namespace AGSS.Repositories
{
    public static class AreaRepository
    {
        public static List<Area> GetDataOfArea(int ProjectID, GravitySurveyOnDeleteNoAction context)
        {
            return context.Areas.Where(a => a.ProjectId == ProjectID)
                .ToList()
                .Cast<Area>()
                .ToList();
        }

        public static int GetAreaIDByProjectID(int ProjectID, GravitySurveyOnDeleteNoAction context)
        {
            return context.Areas.Where(a => a.ProjectId == ProjectID).Select(a => a.AreaId).FirstOrDefault();
        }

        public static void SaveChanges(Area pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Areas.Update(pr);
                context.SaveChanges();
            }
        }
    }
}
