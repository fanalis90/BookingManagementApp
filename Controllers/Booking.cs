﻿namespace BookingManagementApp.Controllers
{
    public class Booking : Date
    {
        public Guid Guid { get; set; }
        public DateTime StartDate { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid {  get; set; }

    }
}