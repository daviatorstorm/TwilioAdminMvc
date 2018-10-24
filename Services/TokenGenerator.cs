using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.Extensions.Options;
using TestTwilio.Models;
using Twilio;
using Twilio.Jwt.AccessToken;

namespace TestTwilio.Services
{
    public class TokenGenerator
    {
        const string APP_NAME = "TwilioChat";
        private readonly TwilioOptions twilioOptions;

        public TokenGenerator(IOptions<TwilioOptions> opts)
        {
            twilioOptions = opts.Value;
        }

        public string Generate(string identity)
        {
            var endpointName = $"{APP_NAME}:{identity}:browser";
            var chatGrand = new ChatGrant
            {
                EndpointId = endpointName,
                ServiceSid = twilioOptions.IpmServiceSID
            };

            var accessToken = new Twilio.Jwt.AccessToken.Token(
                twilioOptions.AccountSID,
                twilioOptions.ApiKey,
                twilioOptions.ApiSecret,
                identity,
                null,
                null,
                new HashSet<IGrant> { chatGrand });

            return accessToken.ToJwt();
        }
    }
}