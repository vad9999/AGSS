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
                  profile.BreaksCount,
                  coord.X,
                  coord.Y
              })
        .ToList()
        .Cast<object>()
        .ToList();
        }

        public static List<Profile> GetProfiles(int area, GravitySurveyOnDeleteNoAction context)
        {
            return context.Profiles.Where(p => p.AreaId == area).ToList().Cast<Profile>().ToList();
        }

        public static List<ProfileCoordinate> GetProfileCoordinates(int area, GravitySurveyOnDeleteNoAction context)
        {
            var profileIds = context.Profiles
                .Where(p => p.AreaId == area)
                .Select(p => p.ProfileId)
                .ToList();

            return context.ProfileCoordinates
                .Where(c => profileIds.Contains(c.ProfileId ?? -1))
                .ToList();
        }


        public static void SaveChanges(Profile pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Profiles.Update(pr);
                context.SaveChanges();
            }
        }

        public static void SaveChangesCoord(ProfileCoordinate pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.ProfileCoordinates.Update(pr);
                context.SaveChanges();
            }
        }
    }
}
