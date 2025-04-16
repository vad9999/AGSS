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
        public static List<object> GetDataOfProfile(int AreaID)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
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
                    .Cast<object>()
                    .ToList();
            }
        }

        public static List<Profile> GetProfiles(int area)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Profiles
                    .Where(p => p.AreaId == area)
                    .ToList();
            }
        }

        public static List<ProfileCoordinate> GetProfileCoordinates(int area)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                var profileIds = context.Profiles
                    .Where(p => p.AreaId == area)
                    .Select(p => p.ProfileId)
                    .ToList();

                return context.ProfileCoordinates
                    .Where(c => profileIds.Contains(c.ProfileId ?? -1))
                    .ToList();
            }
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

        public static void Add(Profile profile)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Profiles.Add(profile);
                context.SaveChanges();
            }
        }

        public static void AddCoordinate(ProfileCoordinate coord)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                context.ProfileCoordinates.Add(coord);
                context.SaveChanges();
            }
        }

        public static void ProfileBreakCount(int profile)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                Profile profile1 = context.Profiles.FirstOrDefault(p => p.ProfileId == profile);
                List<ProfileCoordinate> coordinate = context.ProfileCoordinates.Where(p => p.ProfileId == profile).ToList();
                if(coordinate.Count > 2)
                {
                    profile1.BreaksCount++;
                    context.Profiles.Update(profile1);
                    context.SaveChanges();
                }
            }
        }

        public static void ProfileBreakCountMinus(int profile)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                Profile profile1 = context.Profiles.FirstOrDefault(p => p.ProfileId == profile);
                List<ProfileCoordinate> coordinate = context.ProfileCoordinates.Where(p => p.ProfileId == profile).ToList();
                if (coordinate.Count > 2)
                {
                    profile1.BreaksCount--;
                    context.Profiles.Update(profile1);
                    context.SaveChanges();
                }
            }
        }

        public static bool CheckProfileId(int profile)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Profiles.Any(p => p.ProfileId == profile);
            }        
        }

        public static bool CheckProfileCoordinatesId(int coord)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.ProfileCoordinates.Any(p => p.ProfileCoordinatesId == coord);
            }
        }

        public static List<ProfileCoordinate> GetCoordByProfile(int profile)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.ProfileCoordinates.Where(p => p.ProfileCoordinatesId == profile).ToList();
            }
        }
    }
}
