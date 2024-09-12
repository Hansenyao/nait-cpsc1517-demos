using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPLibrary
{
    // Access Modifier - internal, public, private
    public class Employment
    {
        #region Data Members
        // private string? _title; // nullable string
        //private string _title = ""; // DON'T DO THAT
        private string _title = string.Empty;
        private double _years;
        private SupervisoryLevel _supervisoryLevel;
        private DateTime _startDate;
        #endregion

        #region Properties
        // Fully Implemented Property
        public string Title
        {
            get { return _title; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Title must be provided.");
                }
                _title = value.Trim();
            }
        }

        public double Years
        {
            get { return _years; }
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentException($"Years {value} is less than 0. Years must be positive.");
                }
                _years = value;
            }
        }
        public DateTime StartDate
        {
            get { return _startDate; }
            private set
            {
                if (value > DateTime.Today)
                {
                    throw new ArgumentException($"Time {value} is in the future.");
                }
                _startDate = value;

                // update _years
                TimeSpan days = DateTime.Today - _startDate;
                Years = Math.Round((days.Days / 365.2), 1);
            }
        }

        public SupervisoryLevel Level
        {
            get { return _supervisoryLevel; }
            private set
            {
                if (!Enum.IsDefined(typeof(SupervisoryLevel), value))
                {
                    throw new ArgumentException($"Supervisory level {value} is invalid.");
                }
                _supervisoryLevel = value;
            }
        }
        #endregion

        #region Constructors
        public Employment()
        {
            Title = "unknown";
            Level = SupervisoryLevel.TeamMember;
            StartDate = DateTime.Today;
            Years = 0.0;
        }
        // greedy constructor
        // greedy is a constructor that accepts a parameter list of value
        public Employment(string title, SupervisoryLevel level, DateTime startDate,  double years = 0.0)
        {
            // We always set to the Properties, do not set over directly to a data member(never ever ever)
            // We wrote business logic, don't ignore it by not using the property.
            Title = title;
            Level = level;
            StartDate = startDate;
            if (years == 0.0)
            {
                TimeSpan days = DateTime.Today - startDate;
                Years = Math.Round((days.Days / 365.2), 1);
            }
            else
            {
                Years = years;
            }
        }

        #endregion

        #region Methods
        public void SetEmploymentResponsibilityLevel(SupervisoryLevel level)
        {
            Level = level;
        }
        public void CorrectStartDate(DateTime startDate)
        {
            StartDate = startDate;
        }
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", 
                                _title, 
                                _supervisoryLevel.ToString(),
                                _startDate.ToString("MMM. dd yyyy"),
                                _years);
        }
        #endregion
    }
}
