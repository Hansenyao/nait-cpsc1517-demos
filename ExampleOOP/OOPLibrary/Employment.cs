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
        public DateTime StartDate { get; set; }

        // Auto-Implemented Property, no additional values needed!
        public SupervisoryLevel Level { get; set; }
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
            Years = years;
        }

        #endregion

        #region Methods

        #endregion
    }
}
