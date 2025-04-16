using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using AGSS.Entities;

namespace AGSS.Repositories
{
    public static class MetadataRepository
    {
        public static List<Metadata> GetDataOfMetadata(int SpecID)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Metadata.Where(m => m.SpectrometerId == SpecID).ToList();
            }
        }

        public static void SaveChanges(Metadata pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Metadata.Update(pr);
                context.SaveChanges();
            }
        }

        public static void Add(Metadata pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Metadata.Add(pr);
                context.SaveChanges();
            }
        }
    }
}
