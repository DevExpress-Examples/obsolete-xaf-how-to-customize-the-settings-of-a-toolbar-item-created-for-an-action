using System;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Win.Templates.ActionContainers;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraBars;
using DevExpress.Data.Filtering;

namespace WinSolution.Module.Win {
    public class CustomizeBarItemSettingsListViewController : ViewController {
        private DevExpress.ExpressApp.Actions.ParametrizedAction parametrizedAction1;
        public CustomizeBarItemSettingsListViewController() {
            this.TargetViewType = ViewType.ListView;
            this.TargetObjectType = typeof(DomainObject1);
            this.parametrizedAction1 = new DevExpress.ExpressApp.Actions.ParametrizedAction(this, "MyDateFilter", PredefinedCategory.Search, typeof(DateTime));
            this.parametrizedAction1.Caption = "My Date Filter";
            this.parametrizedAction1.Execute += new DevExpress.ExpressApp.Actions.ParametrizedActionExecuteEventHandler(parametrizedAction1_Execute);
        }
        void parametrizedAction1_Execute(object sender, DevExpress.ExpressApp.Actions.ParametrizedActionExecuteEventArgs e) {
            ((ListView)View).CollectionSource.Criteria[this.parametrizedAction1.Id] = CriteriaOperator.Parse("[CreatedOn]=?", Convert.ToDateTime(e.ParameterCurrentValue));
        }
        protected override void OnFrameAssigned() {
            base.OnFrameAssigned();
            Frame.TemplateChanged += new EventHandler(Frame_TemplateChanged);
        }
        void Frame_TemplateChanged(object sender, EventArgs e) {
            foreach (IActionContainer container in Frame.Template.GetContainers()) {
                if (container.ContainerId == PredefinedCategory.Search.ToString() && container is ActionContainerBarItem) {
                    ActionContainerBarItem actionContainerBarItem = (ActionContainerBarItem)container;
                    foreach (BarItemLink itemLink in actionContainerBarItem.ItemLinks) {
                        if (itemLink.Caption == parametrizedAction1.Caption) {
                            BarEditItemLink editItemLink = (BarEditItemLink)itemLink;
                            RepositoryItemDateEdit editor = (RepositoryItemDateEdit)editItemLink.Edit;
                            editor.Mask.UseMaskAsDisplayFormat = true;
                            editor.Mask.EditMask = "MM/dd/yyyy";
                            break;
                        }
                    }
                }
            }
        }
    }
}