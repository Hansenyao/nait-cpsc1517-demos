﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OOPLibrary;

namespace ExampleOOPWebApp.Components.Pages.Examples
{
    public partial class DataEntry
    {
        private string feedback = string.Empty;
        private List<string> errorMsgs = [];

        //Creating variables to store the form information
        //Match what is in the class for datatypes
        private string empTitle = string.Empty;
        private DateTime empStartDate = DateTime.Today;
        private SupervisoryLevel empLevel;
        private double empYears = 0.0;
        private Employment employment = null;

        // injected service into your application
        // injected services they need to be coded as preperties, typically auto-implemented.
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        //Inject a service to navigate between pages
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        private void OnCollect()
        {
            feedback = string.Empty;
            errorMsgs.Clear();

            // primitive validation
                // presence
                // datatype/pattern
                // range of value
            // Business Rules
                // Title must present, must have at least character
                // start date cannot be in the future
                // years cannot be less than zero
            if (string.IsNullOrWhiteSpace(empTitle))
            {
                errorMsgs.Add("Title is required.");
            }
            if (empStartDate > DateTime.Today)
            {
                errorMsgs.Add("Start Date cannot be in the future.");
            }

            if (empYears < 0.0)
            {
                errorMsgs.Add("Years must be 0 or greater.");
            }

            if (errorMsgs.Count == 0)
            {
                // we can now process the data

                // if you are using a class to collect and hold your data
                //   You need to be concerned with how the class coding handled any errors
                // this normally means some type of try/catch processing
                try
                {
                    //Create an instance of the Employment class
                    //  we will write this to a data file
                    //remember that is an error occurs while creating your instance
                    //the instance will throw an exception!

                    employment = new Employment(empTitle, empLevel, empStartDate, empYears);

                    //Write the class data as a csv string to a csv text file
                    //Required:
                    //  a) know the file location
                    //  b) have a technique to write to a file
                    //      1) SteamReading/StreamWriter
                    //      2) use System.IO.File methods
                    string fileName = @"Data/Employments.csv";
                    string line = $"{employment}\n";
                    File.AppendAllText(fileName, line);

                    // at this point the data is acceptable based off the form validation
                    feedback = $"Entered data: {empTitle}, {empStartDate.ToString("MMM. dd yyyy")}, {empLevel}, {empYears}\n\n Data saved to file.";

                }
                catch (ArgumentNullException ex) 
                {
                    errorMsgs.Add($"Missing Data: {ex.Message}");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    errorMsgs.Add($"Data Range: {GetInnerException(ex).Message}");
                }
                catch (ArgumentException ex)
                {
                    errorMsgs.Add($"Data Value: {GetInnerException(ex).Message}");
                }
                catch (FormatException ex)
                {
                    errorMsgs.Add($"Data Format: {GetInnerException(ex).Message}");
                }
                catch (Exception ex)
                {
                    errorMsgs.Add($"System error: {GetInnerException(ex).Message}");
                }
            }
        }
        private void GoToReport()
        {
            NavigationManager.NavigateTo("employmentreport");
        }
        private async void ClearForm()
        {
            // issue as SimpleConsoleFormatterOptions prompt dialogue to
            // the user to confirm the form clearing
            object[] messageLine = new object[] { "Clearing with tlose all unsaved data. Are you sure you want to clear the form?" };
            if (await JSRuntime.InvokeAsync<bool>("confirm", messageLine))
            {
                feedback = "";
                errorMsgs.Clear();
                empTitle = string.Empty;
                empStartDate = DateTime.Today;
                empLevel = SupervisoryLevel.Entry;
                empYears = 0.0;

                // Re-render page with new datas (in an async mode)
                await InvokeAsync(StateHasChanged);
                // If not in an async function use:
                //StateHasChanged();
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
