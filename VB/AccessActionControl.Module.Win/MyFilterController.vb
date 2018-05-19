Imports DevExpress.XtraBars
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.ExpressApp.Win.Templates
Imports DevExpress.ExpressApp.Win.Templates.ActionContainers

Namespace AccessActionControl.Module.Win
    Public Class MyFilterController
        Inherits ViewController

        Private dateFilterAction As ParametrizedAction
        Public Sub New()
            TargetViewType = ViewType.ListView
            TargetObjectType = GetType(MyDomainObject)
            dateFilterAction = New ParametrizedAction(Me, "MyDateFilter", PredefinedCategory.Search, GetType(Date))
            AddHandler dateFilterAction.Execute, AddressOf dateFilterAction_Execute
        End Sub

        Private Sub dateFilterAction_Execute(ByVal sender As Object, ByVal e As ParametrizedActionExecuteEventArgs)
            Dim criterion As CriteriaOperator = Nothing
            If e.ParameterCurrentValue IsNot Nothing AndAlso e.ParameterCurrentValue.ToString() <> String.Empty Then
                criterion = New BinaryOperator("CreatedOn", Convert.ToDateTime(e.ParameterCurrentValue))
            End If
            CType(View, ListView).CollectionSource.Criteria(dateFilterAction.Id) = criterion
        End Sub
    End Class
    Public Class BarActionItemsCustomizationController
        Inherits WindowController

        Public Sub New()
            TargetWindowType = WindowType.Main
        End Sub
        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            AddHandler BarActionItemsFactory.CustomizeActionControl, AddressOf BarActionItemsFactory_CustomizeActionControl
        End Sub
        Private Sub BarActionItemsFactory_CustomizeActionControl(ByVal sender As Object, ByVal e As CustomizeActionControlEventArgs)
            If e.Action.Id = "MyDateFilter" Then
                Dim barItem As BarEditItem = CType(e.ActionControl.Control, BarEditItem)
                barItem.Width = 170
                Dim repositoryItem As RepositoryItemDateEdit = CType(barItem.Edit, RepositoryItemDateEdit)
                repositoryItem.Mask.UseMaskAsDisplayFormat = True
                repositoryItem.Mask.EditMask = "yyyy-MMM-dd"
                repositoryItem.NullText = "Enter date"
            End If
        End Sub
        Protected Overrides Sub OnDeactivated()
            MyBase.OnDeactivated()
            RemoveHandler BarActionItemsFactory.CustomizeActionControl, AddressOf BarActionItemsFactory_CustomizeActionControl
        End Sub
    End Class
End Namespace
