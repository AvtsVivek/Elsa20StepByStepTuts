//using Elsa.Bookmarks;
//using Elsa.Services;
//using Elsa.Services.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Elsa.CustomActivityLibrary.BookMarks
//{
//    public class CustomBookmarkProviderContext<TActivity> : BookmarkProviderContext<TActivity> where TActivity : IActivity
//    {
//        public CustomBookmarkProviderContext(ActivityExecutionContext activityExecutionContext, ActivityType activityType, BookmarkIndexingMode mode) : base(activityExecutionContext, activityType, mode)
//        {

//        }
//        //public IActivityBlueprintWrapper<TActivity> Activity => GetActivity<TActivity>();

//        //public ValueTask<T?> ReadActivityPropertyAsync<T>(Expression<Func<TActivity, T>> propertyExpression, CancellationToken cancellationToken = default) => base.ReadActivityPropertyAsync(propertyExpression, cancellationToken);

//        //public async ValueTask<T?> ReadActivityPropertyAsync(Expression<Func<TActivity, T>> propertyExpression, CancellationToken cancellationToken = default) //where TActivity : IActivity
//        //{

//        //    if (Mode == BookmarkIndexingMode.WorkflowBlueprint)
//        //    {
//        //        return await EvaluatePropertyValueAsync(propertyExpression, cancellationToken);
//        //    }
//        //    return ActivityExecutionContext.GetState(propertyExpression);
//        //}

//        private async ValueTask<T?> EvaluatePropertyValueAsync<TActivity, T>(Expression<Func<TActivity, T>> propertyExpression, CancellationToken cancellationToken = default) where TActivity : IActivity
//        {
//            var workflowBlueprint = ActivityExecutionContext.WorkflowExecutionContext.WorkflowBlueprint;
//            var activityId = ActivityExecutionContext.ActivityBlueprint.Id;

//            // Computed property setters that depend on actual workflow state might fault, since we might be using a fake activity execution context.
//            try
//            {
//                return await workflowBlueprint.GetActivityPropertyValue(activityId, propertyExpression, ActivityExecutionContext, cancellationToken);
//            }
//            catch (Exception)
//            {
//                return default;
//            }
//        }

//    }
//    //public class CustomBookmarkProviderContext : BookmarkProviderContext
//    //{
//    //    public CustomBookmarkProviderContext(ActivityExecutionContext activityExecutionContext, ActivityType activityType, BookmarkIndexingMode mode) : base(activityExecutionContext, activityType, mode)
//    //    {

//    //    }

//    //    public async ValueTask<T?> ReadActivityPropertyAsync<TActivity, T>(Expression<Func<TActivity, T>> propertyExpression, 
//    //        CancellationToken cancellationToken = default) where TActivity : IActivity
//    //    {

//    //        if (Mode == BookmarkIndexingMode.WorkflowBlueprint)
//    //        {
//    //            return await EvaluatePropertyValueAsync(propertyExpression, cancellationToken);
//    //        }
//    //        return ActivityExecutionContext.GetState(propertyExpression);
//    //    }

//    //    private async ValueTask<T?> EvaluatePropertyValueAsync<TActivity, T>(Expression<Func<TActivity, T>> propertyExpression, CancellationToken cancellationToken = default) where TActivity : IActivity
//    //    {
//    //        var workflowBlueprint = ActivityExecutionContext.WorkflowExecutionContext.WorkflowBlueprint;
//    //        var activityId = ActivityExecutionContext.ActivityBlueprint.Id;

//    //        // Computed property setters that depend on actual workflow state might fault, since we might be using a fake activity execution context.
//    //        try
//    //        {
//    //            return await workflowBlueprint.GetActivityPropertyValue(activityId, propertyExpression, ActivityExecutionContext, cancellationToken);
//    //        }
//    //        catch (Exception)
//    //        {
//    //            return default;
//    //        }
//    //    }
//    //}
//}
