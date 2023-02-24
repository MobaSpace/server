using System;

namespace MobaSpace.Core.Utils
{
    public static class DateTimeUtils
    {
        public static string ThereIs(this TimeSpan timeSpan)
        {
            if (timeSpan.TotalSeconds < 30)
            {
                return "Il y a quelques secondes";
            }
            else if (timeSpan.TotalMinutes < 1)
            {
                return "Il y a moins d'une minute";
            }
            else if (timeSpan.TotalMinutes < 2)
            {
                return "Il y a environ 1 minute";
            }
            else if (timeSpan.TotalMinutes <= 60)
            {
                return $"Il y a {(int)timeSpan.TotalMinutes} minutes";
            }
            else if (timeSpan.TotalHours <= 24)
            {
                return $"Il y a {(int)timeSpan.TotalHours} heures";
            }
            else if (timeSpan.Days < 2)
            {
                return $"Il y a environ {(int)timeSpan.TotalDays} jour";
            }
            else
            {
                return $"Il y a {(int)timeSpan.TotalDays} jours";
            }
        }
    }
}
