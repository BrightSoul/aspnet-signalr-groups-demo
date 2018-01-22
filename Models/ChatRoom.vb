Namespace AspNetSignalRGroupsDemo.Models
    Public Class ChatRoom
        Private m_Id As String
        Public Property Id() As String
            Get
                Return m_Id
            End Get
            Set(ByVal value As String)
                m_Id = value
            End Set
        End Property

        Private m_Name As String
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set(ByVal value As String)
                m_Name = value
            End Set
        End Property
    End Class
End Namespace
