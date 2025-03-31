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
        public static List<object> GetDataOfArea(int ProjectID, GravitySurveyOnDeleteNoAction context)
        {
            return context.Areas.Where(a => a.ProjectId == ProjectID)
                .Select(a => new {a.GeologicalInfo, a.Area1, a.ProfileCount})
                .ToList()
                .Cast<object>()
                .ToList();
        }

        public static int GetAreaIDByProjectID(int ProjectID, GravitySurveyOnDeleteNoAction context)
        {
            return context.Areas.Where(a => a.ProjectId == ProjectID).Select(a => a.AreaId).FirstOrDefault();
        }
    }
}
