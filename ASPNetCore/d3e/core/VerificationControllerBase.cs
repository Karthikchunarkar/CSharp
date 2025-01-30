using System.Collections.Generic;
using sun.misc;
using Classes;
using list;
using models;
using store;

namespace d3e.core
{
    public abstract class VerificationControllerBase
    {
        private static String SUCCESS = "<!DOCTYPE html><html><head><link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/mini.css/3.0.1/mini-default.min.css\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><meta charset=\"utf-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><title>Verifying...</title></head><body><div class=\"container\"><div class=\"row\"><h1>Verification Successful!</h1></div><div class=\"row\"><h1><small>You may close this page.</small></h1></div></div></body></html>";
        private static String FAILED = "<!DOCTYPE html><html><head><link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/mini.css/3.0.1/mini-default.min.css\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><meta charset=\"utf-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><title>Verifying...</title></head><body><div class=\"container\"><div class=\"row\"><h1>That shouldn't have happened...</h1></div><div class=\"row\"><h1><small>Something went wrong while processing your verification. We are looking into it.</small></h1></div><div class=\"row\"><h1><small>Your link will remain active, so please retry after some time. You may close this page.</small></h1></div></div></body></html>";
        private static String INVALID = "<!DOCTYPE html><html><head><link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/mini.css/3.0.1/mini-default.min.css\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><meta charset=\"utf-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><title>Verifying...</title></head><body><div class=\"container\"><div class=\"row\"><h1>You shouldn't be here</h1></div><div class=\"row\"><h1><small>This link is invalid. If you didn't already click on this link, please reach out to Customer Support.</small></h1></div><div class=\"row\"><h1><small>You may close this page.</small></h1></div></div></body></html>";

        private VerificationDataByTokenImpl impl;

        protected string HandleVerificationClick(string token)
        {
            try
            {
                if (token == null)
                {
                    return INVALID;
                }
                VerificationDataByTokenRequest req = new VerificationDataByTokenRequest();
                req.SetToken(token);
                VerificationDataByToken dataByToken = impl.Get(req);

                List<VerificationData> items = dataByToken.GetItems();

                if (dataByToken == null || items.Count != 1)
                {
                    return INVALID;
                }

                VerificationData data = items[0];
                if (data.Processed)
                {
                    return INVALID;
                }
                data.Processed = true;
                Database.Get().Save(data);

                HandleVerificationClick(data.Method, data.Context);
                return SUCCESS;
            }
            catch (Exception e)
            {
                return FAILED;
            }
        }

        public abstract void HandleVerificationClick(string method,  string context);

    }
}
