﻿using MyClassLibrary;
using System;
using System.Collections.Generic;

namespace MyClassLibrary
{
    public class clsFlightCollection
    {
        //private data member for the list
        List<clsFlights> mFlightsList = new List<clsFlights>();
        public List<clsFlights> FlightList
        {
            get
            {
                //return the private data
                return mFlightsList;
            }
            set
            {
                //set the private data
                mFlightsList = value;
            }
        }

        //public property for count
        public int Count
        {
            get
            {
                //return the count of the list
                return mFlightsList.Count;
            }
            set
            {
                //we shall worry about this later
            }
        }

        private clsFlights mThisFlight = new clsFlights();
        public clsFlights ThisFlight
        {
            get
            {
                //return the private data
                return mThisFlight;
            }
            set
            {
                //set the private data
                mThisFlight = value;
            }
        }

        ////constructor for the class
        //public clsFlightCollection()
        //{
        //    //create the items of test data
        //    clsFlights TestItem = new clsFlights();
        //    //set its properties
        //    TestItem.Flight_No = 1;
        //    TestItem.Airline = "Air India";
        //    TestItem.ArrivalAirport = "BHX";
        //    TestItem.Arrival = DateTime.Now.Date;
        //    TestItem.Departure = DateTime.Now.Date;
        //    TestItem.DepartureAirport = "BHX";
        //    TestItem.Destination = "India";
        //    //add the item to the test list
        //    mFlightsList.Add(TestItem);
        //    //re initialise the object for some new data
        //    TestItem = new clsFlights();
        //    //set its properties
        //    TestItem.Flight_No = 2;
        //    TestItem.Airline = "Air France";
        //    TestItem.ArrivalAirport = "LHR";
        //    TestItem.Arrival = DateTime.Now.Date;
        //    TestItem.Departure = DateTime.Now.Date;
        //    TestItem.DepartureAirport = "LHR";
        //    TestItem.Destination = "Dubai";
        //    //add the item to the test list
        //    mFlightsList.Add(TestItem);
        //}

        //constructor for the class
        public clsFlightCollection()
        {
            //var for the index
            //Int32 Index = 0;
            ////var to store the record count
            //Int32 RecordCount = 0;
            //object for data connection
            clsDataConnection DB = new clsDataConnection();
            //execute the stored procedure
            DB.Execute("sproc_tblFlights_SelectAll");
            PopulateArray(DB);
            //get the count of records
            //RecordCount = DB.Count;
            ////while there are records to process
            //while (Index < RecordCount)
            //{
            //    //create a blank flight
            //    clsFlights AnFlight = new clsFlights();
            //    //read in the fields from the current record
            //    AnFlight.FlightID = Convert.ToInt32(DB.DataTable.Rows[0]["FlightID"]);
            //    AnFlight.FlightNo = Convert.ToString(DB.DataTable.Rows[0]["FlightNo"]);
            //    AnFlight.Airline = Convert.ToString(DB.DataTable.Rows[0]["Airline"]);
            //    AnFlight.Destination = Convert.ToString(DB.DataTable.Rows[0]["Destination"]);
            //    AnFlight.Arrival = Convert.ToDateTime(DB.DataTable.Rows[0]["ArrivalDate"]);
            //    AnFlight.Departure = Convert.ToDateTime(DB.DataTable.Rows[0]["DepartureDate"]);
            //    AnFlight.ArrivalAirport = Convert.ToString(DB.DataTable.Rows[0]["ArrivalAirport"]);
            //    AnFlight.DepartureAirport = Convert.ToString(DB.DataTable.Rows[0]["DepartureAirport"]);
            //    //add the record to the private data member
            //    mFlightsList.Add(AnFlight);
            //    //point at the next record
            //    Index++;
            //}
        }

        public int Add()
        {
            //adds a new record to the database based on the values of mThisFlights
            //connect to the database
            clsDataConnection DB = new clsDataConnection();
            //set the parameters for the stored procedure
            DB.AddParameter("@FlightNo", mThisFlight.FlightNo);
            DB.AddParameter("@Airline", mThisFlight.Airline);
            DB.AddParameter("@ArrivalAirport", mThisFlight.ArrivalAirport);
            DB.AddParameter("@Arrival", mThisFlight.Arrival);
            DB.AddParameter("@Departure", mThisFlight.Departure);
            DB.AddParameter("@DepartureAirport", mThisFlight.DepartureAirport);
            DB.AddParameter("@Destination", mThisFlight.Destination);
            //execute the query returning the primary key value
            return DB.Execute("sproc_tblFlights_Insert");

        }

        public void Delete()
        {
            //deletes the record pointed to by thisFlights
            //connect to the database
            clsDataConnection DB = new clsDataConnection();
            //set the parameters for the stored procedure
            DB.AddParameter("@FlightID", mThisFlight.FlightID);
            //execute the stored procedure
            DB.Execute("sproc_tblFLights_Delete");
        }

        public void Update()
        {
            //update an existing record based on the values of thisFlights
            //connect to the database
            clsDataConnection DB = new clsDataConnection();
            //set the parameters for the stored procedure
            DB.AddParameter("@FlightID", mThisFlight.FlightID);
            DB.AddParameter("@FlightNo", mThisFlight.FlightNo);
            DB.AddParameter("@Airline", mThisFlight.Airline);
            DB.AddParameter("@ArrivalAirport", mThisFlight.ArrivalAirport);
            DB.AddParameter("@Arrival", mThisFlight.Arrival);
            DB.AddParameter("@Departure", mThisFlight.Departure);
            DB.AddParameter("@DepartureAirport", mThisFlight.DepartureAirport);
            DB.AddParameter("@Destination", mThisFlight.Destination);
            //execute the stored procedure
            DB.Execute("sproc_tblFlights_Update");

        }

        public void FilterByDestination(string Destination)
        {
            //filters the records based on a fill or partial airline
            //connect to the database
            clsDataConnection DB = new clsDataConnection();
            //send the Destination parameter to the database
            DB.AddParameter("@Destination", Destination);
            //execute the stored procedure
            DB.Execute("sproc_tblFlights_FilterByDestination");
            PopulateArray(DB);
        }

        void PopulateArray(clsDataConnection DB)
        {
            //populates the array list based on the data table in the parameter DB
            //var for the index
            Int32 Index = 0;
            //var to store the record count
            Int32 RecordCount;
            //get the count of records
            RecordCount = DB.Count;
            //clear the private array list
            mFlightsList = new List<clsFlights>();
            //while there are records to process
            while (Index < RecordCount)
            {
                //create a blank flight
                clsFlights AnFlight = new clsFlights();
                //read in the fields from the current record
                AnFlight.FlightID = Convert.ToInt32(DB.DataTable.Rows[Index]["FlightID"]);
                AnFlight.FlightNo = Convert.ToString(DB.DataTable.Rows[Index]["FlightNo"]);
                AnFlight.Airline = Convert.ToString(DB.DataTable.Rows[Index]["Airline"]);
                AnFlight.ArrivalAirport = Convert.ToString(DB.DataTable.Rows[Index]["ArrivalAirport"]);
                AnFlight.DepartureAirport = Convert.ToString(DB.DataTable.Rows[Index]["DepartureAirport"]);
                AnFlight.Arrival = Convert.ToDateTime(DB.DataTable.Rows[Index]["ArrivalDate"]);
                AnFlight.Departure = Convert.ToDateTime(DB.DataTable.Rows[Index]["DepartureDate"]);
                AnFlight.Destination = Convert.ToString(DB.DataTable.Rows[Index]["Destination"]);
                //add the record to the private data member
                mFlightsList.Add(AnFlight);
                //point at the next record
                Index++;

            }
        }
    }
}