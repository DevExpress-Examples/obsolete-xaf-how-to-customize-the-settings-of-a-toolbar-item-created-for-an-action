Imports Microsoft.VisualBasic
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports System

Namespace WinSolution.Module
	<DefaultClassOptions> _
	Public Class DomainObject1
		Inherits BaseObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Private _CreatedOn As DateTime
		Public Property CreatedOn() As DateTime
			Get
				Return _CreatedOn
			End Get
			Set(ByVal value As DateTime)
				SetPropertyValue("CreatedOn", _CreatedOn, value)
			End Set
		End Property
		Private _Name As String
		Public Property Name() As String
			Get
				Return _Name
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Name", _Name, value)
			End Set
		End Property
	End Class
End Namespace
