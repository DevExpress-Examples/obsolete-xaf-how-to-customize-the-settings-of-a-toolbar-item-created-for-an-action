Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraBars
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.ExpressApp.Win.Templates
Imports DevExpress.ExpressApp.Win.Templates.ActionContainers

Namespace AccessActionControl.Module.Win
	Partial Public Class MyFilterController
		Inherits ViewController
		Private parametrizedAction1 As ParametrizedAction
		Public Sub New()
			TargetViewType = ViewType.ListView
			TargetObjectType = GetType(MyDomainObject)
			parametrizedAction1 = New ParametrizedAction(Me, "My Date Filter", PredefinedCategory.Search, GetType(DateTime))
			parametrizedAction1.Caption = "My Date Filter"
			AddHandler parametrizedAction1.Execute, AddressOf parametrizedAction1_Execute
		End Sub

		Private Sub parametrizedAction1_Execute(ByVal sender As Object, ByVal e As ParametrizedActionExecuteEventArgs)
			Dim criterion As CriteriaOperator = Nothing
			If e.ParameterCurrentValue IsNot Nothing AndAlso e.ParameterCurrentValue.ToString() <> String.Empty Then
				criterion = New BinaryOperator("CreatedOn", Convert.ToDateTime(e.ParameterCurrentValue))
			End If
			CType(View, ListView).CollectionSource.Criteria(parametrizedAction1.Id) = criterion
		End Sub
		Protected Overrides Overloads Sub OnFrameAssigned()
			AddHandler BarActionItemsFactory.CustomizeActionControl, AddressOf DefaultBarActionItemsFactory_CustomizeActionControl
		End Sub
		Protected Overrides Overloads Sub OnDeactivated()
			RemoveHandler BarActionItemsFactory.CustomizeActionControl, AddressOf DefaultBarActionItemsFactory_CustomizeActionControl
			MyBase.OnDeactivated()
		End Sub
		Private Sub DefaultBarActionItemsFactory_CustomizeActionControl(ByVal sender As Object, ByVal e As CustomizeActionControlEventArgs)
			If e.Action.Id = parametrizedAction1.Id Then
				Dim barItem As BarEditItem = CType(e.ActionControl.Control, BarEditItem)
				Dim repositoryItem As RepositoryItemDateEdit = CType(barItem.Edit, RepositoryItemDateEdit)
				repositoryItem.Mask.UseMaskAsDisplayFormat = True
				repositoryItem.Mask.EditMask = "yyyy-MMM-dd"
				repositoryItem.NullText = "Enter date"
			End If
		End Sub
	End Class
End Namespace