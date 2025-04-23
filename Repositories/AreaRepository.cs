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
        public static List<Area> GetDataOfArea(int ProjectID)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Areas.Where(a => a.ProjectId == ProjectID)
                .ToList();
            }
        }

        public static void AreaBreakCount(int project)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                Area area = context.Areas.FirstOrDefault(a => a.ProjectId == project);
                area.BreaksCount++;
                context.Areas.Update(area);
                context.SaveChanges();
            }
        }

        public static void AreaBreakCountMinus(int project)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                Area area = context.Areas.FirstOrDefault(a => a.ProjectId == project);
                area.BreaksCount--;
                context.Areas.Update(area);
                context.SaveChanges();
            }
        }

        public static void AreaProfileCount(int project)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                Area area = context.Areas.FirstOrDefault(a => a.ProjectId == project);
                area.ProfileCount++;
                context.Areas.Update(area);
                context.SaveChanges();
            }
        }

        public static void AreaProfileCountMinus(int project)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                Area area = context.Areas.FirstOrDefault(a => a.ProjectId == project);
                area.ProfileCount--;
                context.Areas.Update(area);
                context.SaveChanges();
            }
        }

        public static int GetAreaIDByProjectID(int ProjectID)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Areas
                              .Where(a => a.ProjectId == ProjectID)
                              .Select(a => a.AreaId)
                              .FirstOrDefault();
            }
        }

        public static List<AreaCoordinate> GetAreaCoordinates(int area)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.AreaCoordinates
                              .Where(a => a.AreaId == area)
                              .ToList();
            }
        }

        public static void Add(Area area)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Areas.Add(area);
                context.SaveChanges();
            }
        }

        public static void AddCoordinate(AreaCoordinate area)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.AreaCoordinates.Add(area);
                context.SaveChanges();
            }
        }

        public static void SaveChanges(Area pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Areas.Update(pr);
                context.SaveChanges();
            }
        }

        public static void SaveChangesCoord(AreaCoordinate pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.AreaCoordinates.Update(pr);
                context.SaveChanges();
            }
        }
    }
}
