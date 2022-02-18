﻿namespace Wangkanai.Universal.Models
{
    public class ConfigOption 
    {
        /// <summary>
        /// Name of the tracker object
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Anonymously identifies a browser instance. By default, this value is stored as part of the first-party analytics tracking cookie with a two-year expiration.
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// This is intended to be a known identifier for a user/visitor provided by the site owner/tracking library user. It may not itself be PII. The value should never be persisted in GA cookies or other Wangkanai.Universal provided storage.
        /// </summary>       
        public string UserId { get; set; }
        /// <summary>
        /// Specifies what percentage of users should be tracked. This defaults to 100 (no users are sampled out) but large sites may need to use a lower sample rate to stay within Google Wangkanai.Universal processing limits.
        /// </summary>
        public int? SampleRate { get; set; }
        /// <summary>
        /// This setting determines how often site speed tracking beacons will be sent. By default, 1% of visitors will be automatically be tracked.
        /// </summary>
        public int? SiteSpeedSampleRate { get; set; }
        /// <summary>
        /// By default the HTTP referrer URL, which is used to attribute traffic sources, is only sent when the hostname of the referring site differs from the hostname of the current page. Enable this setting only if you want to process other pages from your current host as referrals.
        /// </summary>
        public bool? AlwaysSendReferrer { get; set; }
        /// <summary>
        /// Name of the cookie used to store analytics data
        /// </summary>
        public string CookieDomain { get; set; }
        /// <summary>
        /// Specifies the domain used to store the analytics cookie. Setting this to 'none' sets the cookie without specifying a domain.
        /// </summary>
        public string CookieName { get; set; }
        /// <summary>
        /// Specifies the cookie expiration, in seconds.
        /// </summary>
        public int? CookieExpires { get; set; }
        /// <summary>
        /// This field is used to configure how analytics.js searches for cookies generated by earlier Google Wangkanai.Universal tracking scripts such as ga.js and urchin.js.
        /// </summary>
        public string LegacyCookieDomain { get; set; }

        private ConfigOption() { 
            
        }

        // public ConfigOption(Configuration config)
        //     : this()
        // {
        //     Name = config.Name;
        //     SampleRate = config.SampleRate;
        //     SiteSpeedSampleRate = config.SiteSpeedSampleRate;
        //     AlwaysSendReferrer = config.AlwaysSendReferrer;
        //     CookieDomain = config.CookieDomain;
        //     CookieName = config.CookieName;
        //     CookieExpires = config.CookieExpires;
        //     LegacyCookieDomain = config.LegacyCookieDomain;            
        // }
        // public ConfigOption(Configuration config, Session session)
        //     : this(config)
        // {
        //     Name = session.Name;
        //     ClientId = session.ClientId;
        //     UserId = session.UserId;
        // }
    }
}
