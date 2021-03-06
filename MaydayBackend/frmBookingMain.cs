﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyClassLibrary;

//Author Mohammad Desai//

namespace MaydayBackend
{
    public partial class frmBookingMain : Form
    {
        //var to store the primary key value of the record selected
        Int32 BookRef;
   
        public frmBookingMain()
        {
            InitializeComponent();
            //on load initalize list box
            lblError.Text = DisplayBookings() + " Found";
            //write whatever the date was one year ago in the start date text box
            txtStartDate.Text = DateTime.Today.Date.AddYears(-1).ToString("dd/MM/yyyy");
            //write whatever the date was is today in the end date text box
            txtEndDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
        }

        private void btnPopulate_Click(object sender, EventArgs e)
        {
            //call the display user fumction
            lblError.Text = DisplayBookings() + " found";
        }

        Int32 DisplayBookings()
        {
            //create an instance of the booking collection
            clsBookingsCollection Bookings = new clsBookingsCollection();
            //set the data source to the list of bookings in the collection
            lstBookings.DataSource = Bookings.BookingsList;
            //set the name of the primary key
            lstBookings.ValueMember = "BookRef";
            //set the data field to display
            lstBookings.DisplayMember = "AllDetails";
            //return the count of records in the list
            return Bookings.Count;
        }

        
       

        private void btnFilter_Click(object sender, EventArgs e)
        {
            //display the number of record found after filtering has been applied
            lblError.Text = FilterBookingsRef(txtFilter.Text) + " Records";

        }

        Int32 FilterBookingsRef(string BookRef)
        {
            //create an instance of the booking collection
            clsBookingsCollection Bookings = new clsBookingsCollection();
            Bookings.FilterbyBookRef(BookRef);
            //set the data source to the list of bookings in the collection
            lstBookings.DataSource = Bookings.BookingsList;
            //set the name of the primary key
            lstBookings.ValueMember = "BookRef";
            //set the data field to display
            lstBookings.DisplayMember = "AllDetails";
            //bind the data to the list
            return Bookings.Count;
        }

        Int32 FilterBookingsDateRange(string StartDate, string EndDate)
        {
            //create an instance of the booking collection
            clsBookingsCollection Bookings = new clsBookingsCollection(" ");
            Bookings.FilterbyDateBooked(StartDate, EndDate);
            //set the data source to the list of bookings in the collection
            lstBookings.DataSource = Bookings.BookingsList;
            //set the name of the primary key
            lstBookings.ValueMember = "BookRef";
            //set the data field to display
            lstBookings.DisplayMember = "AllDetails";
            //bind the data to the list
            return Bookings.Count;
        }

        private void btnDFilter_Click(object sender, EventArgs e)
        {
                //create an instance of the booking list
                clsBookingsCollection BookingsList = new clsBookingsCollection(" ");
                //validate the data on the web form
                String Error = BookingsList.ThisBookings.ValidDateFilter(txtStartDate.Text, txtEndDate.Text);
                //if the data is OK then add it to the object
                if (Error == "")
                {
                    //display the number of record found after filtering has been applied
                    lblError.Text = FilterBookingsDateRange(txtStartDate.Text, txtEndDate.Text) + " Found";
                }
                else
                {
                    //report an error
                    lblError.Text = "There were problems with the data entered " + Error;
                }    
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            //get the primary key value of the record to edit and pass it onto edit form
            FrmAEBookings ABooking = new FrmAEBookings(Convert.ToInt32(lstBookings.SelectedValue));  
            //make the form visible 
            ABooking.Visible = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
 
            //get the primary key value of the record to delete   
             BookRef = Convert.ToInt32(lstBookings.SelectedValue);   
             //delete the record displaying a meaning full dialogue 
             lblError.Text = "Booking Reference " +  DeleteBooking() + " Canceled";   
             //refesh list box   
             DisplayBookings();     
        }


        Int32 DeleteBooking()
        {
            //function to delete the selected record
            //create a new instance of the bookings collection
            clsBookingsCollection Bookings = new clsBookingsCollection();
            //find the record to delete
            Bookings.ThisBookings.Find(BookRef);
            //delete the record
            Bookings.Delete();
            //return the primary key of the value
            return BookRef;
        }

        private void frmBookingMain_Load(object sender, EventArgs e)
        {

        }

        private void btnSurname_Click(object sender, EventArgs e)
        {
            //call the display user fumction
            lblError.Text = FilterSurnane(txtFilter.Text) + " found";
        }

        Int32 FilterSurnane(string Surnane)
        {
            //create an instance of the booking collection
            clsBookingsCollection Bookings = new clsBookingsCollection();
            Bookings.FilterSurname(Surnane);
            //set the data source to the list of bookings in the collection
            lstBookings.DataSource = Bookings.BookingsList;
            //set the name of the primary key
            lstBookings.ValueMember = "BookRef";
            //set the data field to display
            lstBookings.DisplayMember = "AllCDetails";
            //bind the data to the list
            return Bookings.Count;
        }
    }
}

