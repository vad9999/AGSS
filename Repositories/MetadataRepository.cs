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
        public static List<object> GetDataOfMetadata(int SpecID, GravitySurveyOnDeleteNoAction context)
        {
            return context.Metadata.Where(m => m.SpectrometerId == SpecID).Select(m => new {m.EquipmentDescription, m.Notes}).ToList().Cast<object>().ToList();
        }
    }
}
