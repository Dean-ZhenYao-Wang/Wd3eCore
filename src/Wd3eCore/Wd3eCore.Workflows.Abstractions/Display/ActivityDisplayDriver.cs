using System.Threading.Tasks;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.DisplayManagement.ModelBinding;
using Wd3eCore.DisplayManagement.Views;
using Wd3eCore.Workflows.Activities;
using Wd3eCore.Workflows.ViewModels;

namespace Wd3eCore.Workflows.Display
{
    /// <summary>
    /// Base class for activity drivers.
    /// </summary>
    public abstract class ActivityDisplayDriver<TActivity> : DisplayDriver<IActivity, TActivity> where TActivity : class, IActivity
    {
        private static string ThumbnailshapeType = $"{typeof(TActivity).Name}_Fields_Thumbnail";
        private static string DesignShapeType = $"{typeof(TActivity).Name}_Fields_Design";

        public override IDisplayResult Display(TActivity model)
        {
            return Combine(
                Shape(ThumbnailshapeType, new ActivityViewModel<TActivity>(model)).Location("Thumbnail", "Content"),
                Shape(DesignShapeType, new ActivityViewModel<TActivity>(model)).Location("Design", "Content")
            );
        }
    }

    /// <summary>
    /// Base class for activity drivers using a strongly typed view model.
    /// </summary>
    public abstract class ActivityDisplayDriver<TActivity, TEditViewModel> : ActivityDisplayDriver<TActivity> where TActivity : class, IActivity where TEditViewModel : class, new()
    {
        private static string EditShapeType = $"{typeof(TActivity).Name}_Fields_Edit";

        public override IDisplayResult Edit(TActivity model)
        {
            return Initialize(EditShapeType, (System.Func<TEditViewModel, ValueTask>)(viewModel =>
            {
                return EditActivityAsync(model, viewModel);
            })).Location("Content");
        }

        public async override Task<IDisplayResult> UpdateAsync(TActivity model, IUpdateModel updater)
        {
            var viewModel = new TEditViewModel();
            if (await updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                await UpdateActivityAsync(viewModel, model);
            }

            return Edit(model);
        }

        /// <summary>
        /// Edit the view model before it's used in the editor.
        /// </summary>
        protected virtual ValueTask EditActivityAsync(TActivity activity, TEditViewModel model)
        {
            EditActivity(activity, model);

            return new ValueTask();
        }

        /// <summary>
        /// Edit the view model before it's used in the editor.
        /// </summary>
        protected virtual void EditActivity(TActivity activity, TEditViewModel model)
        {
        }

        /// <summary>
        /// Updates the activity when the view model is validated.
        /// </summary>
        protected virtual Task UpdateActivityAsync(TEditViewModel model, TActivity activity)
        {
            UpdateActivity(model, activity);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Updates the activity when the view model is validated.
        /// </summary>
        protected virtual void UpdateActivity(TEditViewModel model, TActivity activity)
        {
        }
    }
}
