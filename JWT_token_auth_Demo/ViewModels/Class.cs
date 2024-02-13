namespace JWT_token_auth_Demo.ViewModels
{
    public class AfterSavedResponseVM
    {
        public string ref_id { get; set; }
        public bool status { get; set; }
        public string? error_msg { get; set; }
        public string? error_sms_otp { get; set; }
        public string? error_email_otp { get; set; }
        public string? online_form2_pase_link { get; set; }

    }

}
