using OOPLibrary;

namespace ExampleOOPWebApp.Components.Pages.Examples
{
    public partial class EmploymentReport
    {
        private string feedback = string.Empty;
        private List<string> errorMsgs = new List<string>();

        private List<Employment> employments = [];

        protected override void OnInitialized()
        {
            ReadData();
            base.OnInitialized();
        }

        private void ReadData()
        {
            feedback = string.Empty;
            errorMsgs.Clear();

            // Use our File IO to Read the data in from our file
            string fileName = @"Data/Employments.csv";
            int lineCounter = 0;
            try
            {
                string[] employmentData = File.ReadAllLines(fileName);
                foreach (var item in employmentData)
                {
                    lineCounter++;
                    /*
                    if (Employment.TryParse(item, out Employment result))
                    {
                        employments.Add(result);
                    }
                    else
                    {
                        errorMsgs.Add($"Record {lineCounter} cannot be added.");
                    }*/

                    // Directly using Parse
                    try
                    {
                        employments.Add(Employment.Parse(item));
                    }
                    catch (Exception ex)
                    {
                        errorMsgs.Add($"Record Error: {lineCounter}: {GetInnerException(ex).Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsgs.Add($"Record {lineCounter}: {GetInnerException(ex).Message}");
            }
        }
        private Exception GetInnerException(Exception ex)
        {
            // Drill down into your Exception untill there are no more inner
            // exceptions at this point you have the "real" error
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }
    }
}
