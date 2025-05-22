using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using AGSS.Entities;

namespace AGSS.Repositories
{
    public static class ChannelsRepository
    {
        public static List<object> GetDataOfChannel1(int AreaID)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Profiles
                    .Where(profile => profile.AreaId == AreaID)
                    .Join(context.ProfileCoordinates,
                          profile => profile.ProfileId,
                          coord => coord.ProfileId,
                          (profile, coord) => new { profile.ProfileId, coord })
                    .Join(context.Channel1s,
                          coordProfile => coordProfile.coord.ProfileCoordinatesId,
                          channel => channel.ProfileCoordinatesId,
                          (coordProfile, channel) => new
                          {
                              coordProfile.ProfileId,
                              coordProfile.coord.X,
                              coordProfile.coord.Y,
                              channel.MeasurementResult
                          })
                    .Cast<object>()
                    .ToList();
            }
        }

        public static List<Channel1> GetChannel1s(int AreaID)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                var profileIds = context.Profiles
                    .Where(p => p.AreaId == AreaID)
                    .Select(p => p.ProfileId)
                    .ToList();

                var coordinates = context.ProfileCoordinates
                    .Where(c => profileIds.Contains(c.ProfileId ?? -1))
                    .Select(c => c.ProfileCoordinatesId)
                    .ToList();

                return context.Channel1s
                    .Where(c => coordinates.Contains(c.ProfileCoordinatesId ?? -1))
                    .ToList();
            }
        }

        public static List<object> GetDataOfChannel2(int AreaID)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Profiles
                    .Where(profile => profile.AreaId == AreaID)
                    .Join(context.ProfileCoordinates,
                          profile => profile.ProfileId,
                          coord => coord.ProfileId,
                          (profile, coord) => new { profile.ProfileId, coord })
                    .Join(context.Channel2s,
                          coordProfile => coordProfile.coord.ProfileCoordinatesId,
                          channel => channel.ProfileCoordinatesId,
                          (coordProfile, channel) => new
                          {
                              coordProfile.ProfileId,
                              coordProfile.coord.X,
                              coordProfile.coord.Y,
                              channel.MeasurementResult
                          })
                    .Cast<object>()
                    .ToList();
            }
        }

        public static List<Channel2> GetChannel2s(int AreaID)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                var profileIds = context.Profiles
                    .Where(p => p.AreaId == AreaID)
                    .Select(p => p.ProfileId)
                    .ToList();

                var coordinates = context.ProfileCoordinates
                    .Where(c => profileIds.Contains(c.ProfileId ?? -1))
                    .Select(c => c.ProfileCoordinatesId)
                    .ToList();

                return context.Channel2s
                    .Where(c => coordinates.Contains(c.ProfileCoordinatesId ?? -1))
                    .ToList();
            }
        }

        public static List<object> GetDataOfChannel3(int AreaID)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Profiles
                    .Where(profile => profile.AreaId == AreaID)
                    .Join(context.ProfileCoordinates,
                          profile => profile.ProfileId,
                          coord => coord.ProfileId,
                          (profile, coord) => new { profile.ProfileId, coord })
                    .Join(context.Channel3s,
                          coordProfile => coordProfile.coord.ProfileCoordinatesId,
                          channel => channel.ProfileCoordinatesId,
                          (coordProfile, channel) => new
                          {
                              coordProfile.ProfileId,
                              coordProfile.coord.X,
                              coordProfile.coord.Y,
                              channel.MeasurementResult
                          })
                    .Cast<object>()
                    .ToList();
            }
        }

        public static List<Channel3> GetChannel3s(int AreaID)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                var profileIds = context.Profiles
                    .Where(p => p.AreaId == AreaID)
                    .Select(p => p.ProfileId)
                    .ToList();

                var coordinates = context.ProfileCoordinates
                    .Where(c => profileIds.Contains(c.ProfileId ?? -1))
                    .Select(c => c.ProfileCoordinatesId)
                    .ToList();

                return context.Channel3s
                    .Where(c => coordinates.Contains(c.ProfileCoordinatesId ?? -1))
                    .ToList();
            }
        }

        public static void SaveChanges1(Channel1 pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Channel1s.Update(pr);
                context.SaveChanges();
            }
        }

        public static void SaveChanges2(Channel2 pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Channel2s.Update(pr);
                context.SaveChanges();
            }
        }

        public static void SaveChanges3(Channel3 pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Channel3s.Update(pr);
                context.SaveChanges();
            }
        }

        public static void AddChannel1(Channel1 channel)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Channel1s.Add(channel);
                context.SaveChanges();
            }
        }

        public static void AddChannel2(Channel2 channel)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Channel2s.Add(channel);
                context.SaveChanges();
            }
        }

        public static void AddChannel3(Channel3 channel)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Channel3s.Add(channel);
                context.SaveChanges();
            }
        }
    }
}
