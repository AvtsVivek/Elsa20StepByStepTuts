using System.Threading;
using System.Threading.Tasks;
using Elsa.CustomActivityLibrary.Model;
using Elsa.Scripting.Liquid.Messages;
using Fluid;
using MediatR;

namespace Elsa.CustomActivityLibrary.Liquid
{
    public class LiquidHandler : INotificationHandler<EvaluatingLiquidExpression>
    {
        public LiquidHandler()
        {

        }
        public Task Handle(EvaluatingLiquidExpression notification, CancellationToken cancellationToken)
        {
            var context = notification.TemplateContext;
            context.Options.MemberAccessStrategy.Register<FileModel>();
            return Task.CompletedTask;
        }
    }
}
