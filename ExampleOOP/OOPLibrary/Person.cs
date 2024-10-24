namespace OOPLibrary
{
    public class Person
    {
        #region Data Members
        private string _firstName;
        private string _lastName;
        #endregion

        #region Properties

        //Fully Implemented Properties
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (string.IsNullOrWhiteSpace((value)))
                {
                    throw new ArgumentNullException("First name is required.");
                }
                _firstName = value.Trim();
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (string.IsNullOrWhiteSpace((value)))
                {
                    throw new ArgumentNullException("Last name is required.");
                }
                _lastName = value.Trim();
            }
        }

        //Auto-Implemented Properties
        //Collection of Instance of the Employment Class
        //Set the lists to a default empty list in order to avoid null reference errors.
        public List<Employment> Positions { get; set; } = []; //[] = new List<Employment>()

        //single reference to the ResidentAddress class
        public ResidentAddress ResidentAddress { get; set; }

        //Read-Only Property
        public string FullName => $"{FirstName} {LastName}";

        #endregion

        #region Constructor
        //Default Constructor
        public Person()
        {
            FirstName = "Unknown";
            LastName = "Unknown";
        }

        public Person(string firstName, string lastName, List<Employment> positions, ResidentAddress residentAddress)
        {
            FirstName = firstName;
            LastName = lastName;
            ResidentAddress = residentAddress;
            //Check if a collection Parameter is given as null or not
            //If the user provides a null, do not set the Property and let the default empty collection be set.
            if (positions != null)
            {
                Positions = positions;
            }
        }
        #endregion

        #region Methods
        //We don't need to break down the Resident Address because it has it's own ToString override.
        public override string ToString() => $"{FirstName},{LastName},{ResidentAddress}";

        public void AddEmployment(Employment employment)
        {
            if (employment == null)
            {
                throw new ArgumentNullException("No employment data provided.");
            }
            //Business Rule: No person can hold two jobs with the save SupervisoryLevel
            if (Positions.Any(x => x.Level == employment.Level))
            {
                throw new ArgumentException("The person has already has that level of employment. Promote them!");
            }

            Positions.Add(employment);
        }
        #endregion
    }
}