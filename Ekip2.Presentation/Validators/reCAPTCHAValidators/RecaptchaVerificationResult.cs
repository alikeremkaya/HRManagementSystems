using Newtonsoft.Json;

namespace Ekip2.Presentation.Validators.reCAPTCHAValidators
{
    public class RecaptchaVerificationResult
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public string[] ErrorCodes { get; set; }
    }
}
