﻿using System;

namespace Jira.Api.Models.Jira
{
    public class LoginInfo
    {
        public int FailedLoginCount { get; set; }
        public int LoginCount { get; set; }
        public DateTime LastFailedLoginTime { get; set; }
        public DateTime PreviousLoginTime { get; set; }
    }
}
