using Elsa.Activities.Console;
using Elsa.Activities.ControlFlow;
using Elsa.Attributes;
using Elsa.Builders;
using P20140WhileLoopPhoneCallWorker.Services;
using Elsa.Services;
using NodaTime;
using Elsa.Activities.Temporal;

namespace P20140WhileLoopPhoneCallWorker.Activities
{
    public class MakePhoneCall : CompositeActivity
    {
        private readonly PhoneCallService _phoneCallService;

        //[ActivityProperty]
        [ActivityInput]
        public string PhoneNumber
        {
            get => GetState<string>();
            set => SetState(value);
        }

        public MakePhoneCall(PhoneCallService phoneCallService)
        {
            _phoneCallService = phoneCallService;
        }

        public override void Build(ICompositeActivityBuilder builder)
        {
            builder
                .While(() => _phoneCallService.CallStatus != PhoneCallStatus.Finished,
                    @while =>
                    {
                        @while
                            .WriteLine(() => $"Ringgggg ringgg. {PhoneNumber}")
                            .Timer(Duration.FromSeconds(2))
                            .Then(() => _phoneCallService.Progress())
                            .WriteLine(() => $"Call status: {_phoneCallService.CallStatus}");
                    });
        }
    }
}