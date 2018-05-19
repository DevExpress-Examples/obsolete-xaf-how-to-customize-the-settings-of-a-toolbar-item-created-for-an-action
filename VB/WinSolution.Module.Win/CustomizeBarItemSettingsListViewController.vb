Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Templates
Imports DevExpress.ExpressApp.Win.Templates.ActionContainers
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraBars
Imports DevExpress.Data.Filtering

Namespace WinSolution.Module.Win
	Public Class CustomizeBarItemSettingsListViewController
		Inherits ViewController
		Private parametrizedAction1 As DevExpress.ExpressApp.Actions.ParametrizedAction
		Public Sub New()
			Me.TargetViewType = ViewType.ListView
			Me.TargetObjectType = GetType(DomainObject1)
			Me.parametrizedAction1 = New DevExpress.ExpressApp.Actions.ParametrizedAction(Me, "MyDateFilter", PredefinedCategory.Search, GetType(DateTime))
			Me.parametrizedAction1.Caption = "My Date Filter"
			AddHandler parametrizedAction1.Execute, AddressOf parametrizedAction1_Execute
		End Sub
		Private Sub parametrizedAction1_Execute(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.Actions.ParametrizedActionExecuteEventArgs)
			CType(View, ListView).CollectionSource.Criteria(Me.parametrizedAction1.Id) = CriteriaOperator.Parse("[CreatedOn]=?", Convert.ToDateTime(e.ParameterCurrentValue))
		End Sub
		Protected Overrides Sub OnFrameAssigned()
			MyBase.OnFrameAssigned()
			AddHandler Frame.TemplateChanged, AddressOf Frame_TemplateChanged
		End Sub
		Private Sub Frame_TemplateChanged(ByVal sender As Object, ByVal e As EventArgs)
			For Each container As IActionContainer In Frame.Template.GetContainers()
				If container.ContainerId = PredefinedCategory.Search.ToString() AndAlso TypeOf container Is ActionContainerBarItem Then
					Dim actionContainerBarItem As ActionContainerBarItem = CType(container, ActionContainerBarItem)
					For Each itemLink As BarItemLink In actionContainerBarItem.ItemLinks
						If itemLink.Caption = parametrizedAction1.Caption Then
							Dim editItemLink As BarEditItemLink = CType(itemLink, BarEditItemLink)
							Dim editor As RepositoryItemDateEdit = CType(editItemLink.Edit, RepositoryItemDateEdit)
							editor.Mask.UseMaskAsDisplayFormat = True
							editor.Mask.EditMask = "MM/dd/yyyy"
							Exit For
						End If
					Next itemLink
				End If
			Next container
		End Sub
	End Class
End Namespace