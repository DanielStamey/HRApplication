﻿using System;

namespace EmployeeManager
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string PropertyName { get; set; }
        public string PrimaryKeyValue { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime DateChanged { get; set; }
    }
}