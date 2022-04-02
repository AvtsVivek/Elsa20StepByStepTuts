namespace P20140WhileLoopPhoneCallWorker.Services
{
    public class PhoneCallService //: IPhoneCallService
    {
        public PhoneCallStatus CallStatus { get; set; }

        public void Progress0() =>
            CallStatus = CallStatus switch
            {
                PhoneCallStatus.Idle => PhoneCallStatus.Dialing,
                PhoneCallStatus.Dialing => PhoneCallStatus.InProgress,
                PhoneCallStatus.InProgress => PhoneCallStatus.Finished,
                _ => CallStatus
            };

        public void Progress()
        {
            CallStatus = CallStatus switch
            {
                PhoneCallStatus.Idle => PhoneCallStatus.Dialing,
                PhoneCallStatus.Dialing => PhoneCallStatus.InProgress,
                PhoneCallStatus.InProgress => PhoneCallStatus.Finished,
                _ => CallStatus
            };
        }
    }

    //public interface IPhoneCallService
    //{
    //    public PhoneCallStatus CallStatus { get; set; }
    //    public void Progress();
    //}

    public enum PhoneCallStatus
    {
        Idle,
        Dialing,
        InProgress,
        Finished
    }
}