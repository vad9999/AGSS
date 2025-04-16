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
        public static List<Spectrometer> GetDataOfSpectrometer(int FlightID)
        {
            using(var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Spectrometers.Where(s => s.FlightId == FlightID).ToList();
            }
        }
        public static int GetSpectrometerIDByFlightID(int FlightID)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Spectrometers.Where(s => s.FlightId == FlightID).Select(s => s.SpectrometerId).FirstOrDefault();
            }
        }

        public static void SaveChanges(Spectrometer pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Spectrometers.Update(pr);
                context.SaveChanges();
            }
        }

        public static void Add(Spectrometer pr)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                context.Spectrometers.Add(pr);
                context.SaveChanges();
            }
        }

        public static bool CheckSpectrometerId(int spectrometer)
        {
            using (var context = new GravitySurveyOnDeleteNoAction())
            {
                return context.Spectrometers.Any(s => s.SpectrometerId == spectrometer);
            }
        }
    }
}
