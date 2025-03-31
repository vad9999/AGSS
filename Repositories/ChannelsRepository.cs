using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGSS.Entities;

namespace AGSS.Repositories
{
    public static class ChannelsRepository
    {
        public static List<object> GetDataOfChannel1(int AreaID, GravitySurveyOnDeleteNoAction context)
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
        .ToList()
        .Cast<object>()
        .ToList();
        }

        public static List<object> GetDataOfChannel2(int AreaID, GravitySurveyOnDeleteNoAction context)
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
        .ToList()
        .Cast<object>()
        .ToList();
        }

        public static List<object> GetDataOfChannel3(int AreaID, GravitySurveyOnDeleteNoAction context)
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
        .ToList()
        .Cast<object>()
        .ToList();
        }
    }
}
