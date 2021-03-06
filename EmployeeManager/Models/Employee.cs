﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManager
{
    public class Employee
    {
        public Employee()
        {
            TeamMemberPhoto = null;
        }

        public int Id { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Email")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [DisplayName("Preferred Contact #")]
        public string PreferredContactPhoneNumber { get; set; }

        public int? PositionId { get; set; }

        public virtual Position Position { get; set; }

        public int? DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        [DisplayName("Start Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]

        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? EndDate { get; set; }

        public int? EmploymentStatusId { get; set; }

        [DisplayName("Employment Status")]
        public virtual EmploymentStatus EmploymentStatus { get; set; }

        public int? ShiftId { get; set; }

        public virtual Shift Shift { get; set; }

        public int? ManagerId { get; set; }

        public virtual Employee Manager { get; set; }

        [DisplayName("Employee Photo")]
        public byte[] TeamMemberPhoto { get; set; }

        public virtual string TeamMemberPhotoString
        {
            get
            {
                string imageString = string.Empty;
                if (TeamMemberPhoto != null)
                {
                    imageString = $"data:image;base64,{System.Convert.ToBase64String(TeamMemberPhoto)}";
                }
                
                return imageString;
            }
        }

        [DisplayName("Favorite Color")]
        public string FavoriteColor { get; set; }

        public ICollection<EmployeePermission> EmployeePermissions { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}