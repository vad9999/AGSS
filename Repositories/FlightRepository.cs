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
        public static List<Flight> GetDataOfFlight(int ProjectID)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Flights
                    .Where(f => f.ProjectId == ProjectID)
                    .ToList();
            }
        }

        public static int GetFlightIDByProjectID(int ProjectID)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Flights
                    .Where(f => f.ProjectId == ProjectID)
                    .Select(f => f.FlightId)
                    .FirstOrDefault();
            }
        }

        public static int? GetOperatorIDByFlightID(int flight)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Flights
                    .Where(f => f.FlightId == flight)
                    .Select(f => f.OperatorId)
                    .FirstOrDefault();
            }
        }


        public static void SaveChanges(Flight pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Flights.Update(pr);
                context.SaveChanges();
            }
        }

        public static void Add(Flight f)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Flights.Add(f);
                context.SaveChanges();
            }
        }

        public static bool CheckFlightId(int flight)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Flights.Any(f => f.FlightId == flight);
            }
        }
    }
}
