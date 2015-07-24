using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TC.Sample.DropAndGridCascading.Helper
{
    public static class Consts
    {
        public static class Lists
        {
            public static class County
            {
                /// <summary>
                /// Country
                /// </summary>
                public const string ListName = "Country";

                public static class Fields
                {
                    /// <summary>
                    /// Title
                    /// </summary>
                    public const string Title = "Title";

                    /// <summary>
                    /// Country
                    /// </summary>
                    public const string Country = "Country";
                }
            }

            public static class ScheduleHoliday
            {
                /// <summary>
                /// ScheduleHoliday
                /// </summary>
                public const string ListName = "ScheduleHoliday";

                public static class Fields
                {
                    /// <summary>
                    /// Title
                    /// </summary>
                    public const string Title = "Title";

                    /// <summary>
                    /// Country
                    /// </summary>
                    public const string Country = "Country";

                    /// <summary>
                    /// Holidays
                    /// </summary>
                    public const string Holidays = "Holidays";
                }
            }
        }
    }
}
