using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGSS.Entities;

namespace AGSS.Repositories
{
    public static class FlightRepository
    {
        public static List<object> GetDataOfFlight(int ProjectID, GravitySurveyOnDeleteNoAction context)
        {
            return context.Flights.Where(f => f.ProjectId == ProjectID).Select(p => new {p.StartDateTime, p.EndDateTime, p.AltitudeAboveGround, p.AltitudeAboveSea, p.Speed}).ToList().Cast<object>().ToList();
        }
        public static int GetFlightIDByProjectID(int ProjectID, GravitySurveyOnDeleteNoAction context)
        {
            return context.Flights.Where(f => f.ProjectId == ProjectID).Select(f => f.FlightId).FirstOrDefault();
        }

        public static int? GetOperatorIDByFlightID(int flight, GravitySurveyOnDeleteNoAction context)
        {
            return context.Flights.Where(f => f.FlightId == flight).Select(f => f.OperatorId).FirstOrDefault();
        }
    }
}
