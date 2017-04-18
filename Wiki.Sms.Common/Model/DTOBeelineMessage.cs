namespace Wiki.Sms.Common.Model
{
    public class DTOBeelineStatus
    {
        public BeelineStatusModel[] messages { get; set; }
    }

    public class BeelineStatusModel
    {
        public string id { get; set; }
        public string number { get; set; }
        public string content { get; set; }
        public string date { get; set; }
        public string sms_rpt_msg_ref { get; set; }
    }

    public class DTOBeelineMessages
    {
        public BeelineMessage[] messages { get; set; }
    }

    public class BeelineMessage
    {
        public string id { get; set; }
        public string number { get; set; }
        public string content { get; set; }
        public string tag { get; set; }
        public string date { get; set; }
        public string draft_group_id { get; set; }
        public string received_all_concat_sms { get; set; }
        public string concat_sms_total { get; set; }
        public string concat_sms_received { get; set; }
        public string sms_class { get; set; }
        public string sms_mem { get; set; }
        public string sms_submit_msg_ref { get; set; }
    }
}
