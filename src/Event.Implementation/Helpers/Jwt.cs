﻿namespace Event.Implementation.Helpers;
    public class JWT
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public TimeSpan Expires { get; set; }
    }

