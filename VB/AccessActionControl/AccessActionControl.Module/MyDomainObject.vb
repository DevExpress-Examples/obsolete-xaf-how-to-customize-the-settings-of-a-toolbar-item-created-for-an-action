Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel

Imports DevExpress.Xpo

Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation

Namespace AccessActionControl.Module
	<DefaultClassOptions> _
	Public Class MyDomainObject
		Inherits BaseObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub

		Public Property CreatedOn() As DateTime
			Get
				Return GetPropertyValue(Of DateTime)("CreatedOn")
			End Get
			Set(ByVal value As DateTime)
				SetPropertyValue("CreatedOn", value)
			End Set
		End Property

		Public Property Title() As String
			Get
				Return GetPropertyValue(Of String)("Title")
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Title", value)
			End Set
		End Property
	End Class
End Namespace
