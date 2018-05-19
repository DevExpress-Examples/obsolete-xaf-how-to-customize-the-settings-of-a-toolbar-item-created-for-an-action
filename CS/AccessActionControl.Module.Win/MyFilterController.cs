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
    public class MyFilterController : ViewController {
        private ParametrizedAction dateFilterAction;
        public MyFilterController() {
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(MyDomainObject);
            dateFilterAction = new ParametrizedAction(
                this, "MyDateFilter", PredefinedCategory.Search, typeof(DateTime));
            dateFilterAction.Execute += dateFilterAction_Execute;
        }

        void dateFilterAction_Execute(object sender, ParametrizedActionExecuteEventArgs e) {
            CriteriaOperator criterion = null;
            if (e.ParameterCurrentValue != null && e.ParameterCurrentValue.ToString() != string.Empty) {
                criterion = new BinaryOperator("CreatedOn",
                    Convert.ToDateTime(e.ParameterCurrentValue));
            }
            ((ListView)View).CollectionSource.Criteria[dateFilterAction.Id] = criterion;
        }
    }
    public class BarActionItemsCustomizationController : WindowController {
        public BarActionItemsCustomizationController() {
            TargetWindowType = WindowType.Main;
        }
        protected override void OnActivated() {
            base.OnActivated();
            BarActionItemsFactory.CustomizeActionControl += BarActionItemsFactory_CustomizeActionControl;
        }
        void BarActionItemsFactory_CustomizeActionControl(object sender, CustomizeActionControlEventArgs e) {
            if (e.Action.Id == "MyDateFilter") {
                BarEditItem barItem = (BarEditItem)e.ActionControl.Control;
                barItem.Width = 170;
                RepositoryItemDateEdit repositoryItem = (RepositoryItemDateEdit)barItem.Edit;
                repositoryItem.Mask.UseMaskAsDisplayFormat = true;
                repositoryItem.Mask.EditMask = "yyyy-MMM-dd";
                repositoryItem.NullText = "Enter date";
            }
        }
        protected override void OnDeactivated() {
            base.OnDeactivated();
            BarActionItemsFactory.CustomizeActionControl -= BarActionItemsFactory_CustomizeActionControl;
        }
    }
}
