using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGSS.Entities;

namespace AGSS.Repositories
{
    public static class ProjectRepository
    {
        public static List<string> GetProjectNames(int ID)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Projects
                    .Where(p => p.CustomerId == ID)
                    .Select(p => p.ProjectName)
                    .ToList();
            }
        }

        public static List<string> GetProjectNamesAdmin()
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Projects
                    .Select(p => p.ProjectName)
                    .ToList();
            }
        }

        public static int GetIDByProjectName(string name)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Projects
                    .Where(p => p.ProjectName == name)
                    .Select(p => p.ProjectId)
                    .FirstOrDefault();
            }
        }

        public static List<Project> GetDataOfProject(int ProjectID)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Projects
                    .Where(p => p.ProjectId == ProjectID)
                    .ToList();
            }
        }

        public static int? GetSpecialistIDByProjectID(int ProjectID)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Projects
                    .Where(p => p.ProjectId == ProjectID)
                    .Select(p => p.LeadSpecialistId)
                    .FirstOrDefault();
            }
        }

        public static int? GetEngineerIDByProjectID(int ProjectID)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Projects
                    .Where(p => p.ProjectId == ProjectID)
                    .Select(p => p.ChiefEnginnerId)
                    .FirstOrDefault();
            }
        }

        //public static int? GetAnalystIDByProjectID(int ProjectID)
        //{
        //    using (var context = new GravitySurveyOnDeleteNoAction())
        //    {
        //        return context.Projects
        //            .Where(p => p.ProjectId == ProjectID)
        //            .Select(p => p.AnalystId)
        //            .FirstOrDefault();
        //    }
        //}

        public static void SaveChanges(Project pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Projects.Update(pr);
                context.SaveChanges();
            }
        }

        public static int GetProjectBySpecialist(LeadSpecialist specialist)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Projects.OrderByDescending(e => e.LeadSpecialist == specialist).FirstOrDefault()?.ProjectId ?? -1;
            }
        }

        public static string GetProjectNameById(int ProjectID)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Projects.FirstOrDefault(p => p.ProjectId == ProjectID).ProjectName;
            }
        }
    }
}
