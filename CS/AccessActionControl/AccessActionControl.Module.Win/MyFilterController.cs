using System;
using DevExpress.XtraBars;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Actions;
using DevExpress.XtraEditors.Repository;
using DevExpress.ExpressApp.Win.Templates;
using DevExpress.ExpressApp.Win.Templates.ActionContainers;

namespace AccessActionControl.Module.Win {
    public partial class MyFilterController : ViewController {
        private ParametrizedAction parametrizedAction1;
        public MyFilterController() {
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(MyDomainObject);
            parametrizedAction1 = new ParametrizedAction(
                this, "My Date Filter", PredefinedCategory.Search, typeof(DateTime));
            parametrizedAction1.Caption = "My Date Filter";
            parametrizedAction1.Execute += parametrizedAction1_Execute;
        }

        void parametrizedAction1_Execute(object sender, ParametrizedActionExecuteEventArgs e) {
            CriteriaOperator criterion = null;
            if (e.ParameterCurrentValue != null && e.ParameterCurrentValue.ToString() != string.Empty) {
                criterion = new BinaryOperator("CreatedOn",
                    Convert.ToDateTime(e.ParameterCurrentValue));
            }
            ((ListView)View).CollectionSource.Criteria[parametrizedAction1.Id] = criterion;
        }
        protected override void OnFrameAssigned() {
            BarActionItemsFactory.CustomizeActionControl += DefaultBarActionItemsFactory_CustomizeActionControl;
        }
        protected override void OnDeactivating() {
            BarActionItemsFactory.CustomizeActionControl -= DefaultBarActionItemsFactory_CustomizeActionControl;
            base.OnDeactivating();
        }
        private void DefaultBarActionItemsFactory_CustomizeActionControl(
            object sender, CustomizeActionControlEventArgs e) {
            if (e.Action.Id == parametrizedAction1.Id) {
                BarEditItem barItem = (BarEditItem)e.ActionControl.Control;
                RepositoryItemDateEdit repositoryItem = (RepositoryItemDateEdit)barItem.Edit;
                repositoryItem.Mask.UseMaskAsDisplayFormat = true;
                repositoryItem.Mask.EditMask = "yyyy-MMM-dd";
                repositoryItem.NullText = "Enter date";
            }
        }
    }
}