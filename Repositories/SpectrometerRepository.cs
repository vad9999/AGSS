using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGSS.Entities;

namespace AGSS.Repositories
{
    public static class SpectrometerRepository
    {
        public static List<object> GetDataOfSpectrometer(int FlightID, GravitySurveyOnDeleteNoAction context)
        {
            return context.Spectrometers.Where(s => s.FlightId == FlightID).Select(s => new {s.MeasurementTime, s.PulseCount, s.TotalCount, s.EnergyWindowsCount}).ToList().Cast<object>().ToList();
        }
        public static int GetSpectrometerIDByFlightID(int FlightID, GravitySurveyOnDeleteNoAction context)
        {
            return context.Spectrometers.Where(s => s.FlightId == FlightID).Select(s => s.SpectrometerId).FirstOrDefault();
        }
    }
}
