using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGSS.Entities;

namespace AGSS.Repositories
{
    public static class ProfileRepository
    {
        public static List<object> GetDataOfProfile(int AreaID, GravitySurveyOnDeleteNoAction context)
        {
            return context.Profiles
        .Where(profile => profile.AreaId == AreaID)
        .Join(context.ProfileCoordinates,
              profile => profile.ProfileId,
              coord => coord.ProfileId,
              (profile, coord) => new
              {
                  profile.ProfileId,
                  coord.X,
                  coord.Y
              })
        .ToList()
        .Cast<object>()
        .ToList();
        }
    }
}
