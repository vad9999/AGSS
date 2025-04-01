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
        public static List<string> GetProjectNames(int ID, GravitySurveyOnDeleteNoAction context)
        {
            return context.Projects.Where(p => p.CustomerId == ID).Select(p => p.ProjectName).ToList();
        }

        public static List<string> GetProjectNamesAdmin(GravitySurveyOnDeleteNoAction context)
        {
            return context.Projects.Select(p => p.ProjectName).ToList();
        }

        public static int GetIDByProjectName(string name, GravitySurveyOnDeleteNoAction context)
        {
            return context.Projects.Where(p => p.ProjectName == name).Select(p => p.ProjectId).FirstOrDefault();
        }

        public static List<Project> GetDataOfProject(int ProjectID, GravitySurveyOnDeleteNoAction context)
        {
            return context.Projects.Where(p => p.ProjectId == ProjectID).ToList().Cast<Project>().ToList();
        }

        public static int? GetSpecialistIDByProjectID(int ProjectID, GravitySurveyOnDeleteNoAction context)
        {
            return context.Projects.Where(p => p.ProjectId == ProjectID).Select(p => p.LeadSpecialistId).FirstOrDefault();
        }

        public static int? GetEngineerIDByProjectID(int ProjectID, GravitySurveyOnDeleteNoAction context)
        {
            return context.Projects.Where(p => p.ProjectId == ProjectID).Select(p => p.ChiefEnginnerId).FirstOrDefault();
        }

        public static int? GetAnalystIDByProjectID(int ProjectID, GravitySurveyOnDeleteNoAction context)
        {
            return context.Projects.Where(p => p.ProjectId == ProjectID).Select(p => p.AnalystId).FirstOrDefault();
        }

        public static void SaveChanges(Project pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Projects.Update(pr);
                context.SaveChanges();
            }
        }
    }
}
